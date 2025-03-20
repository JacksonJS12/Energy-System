#include <WiFi.h>
#include <WiFiClient.h>
#include <WebServer.h>
#include <HTTPClient.h>

#define RELAY1_PIN D4   // Power Grid Control (NC)
#define RELAY2_PIN D6   // Battery Charging Control (NC)
#define BUTTON_PIN D5   // Button to toggle AC state
#define VOLTAGE_PIN A1  // Analog input pin connected to voltage sensor module

const char* ssid = "ne_pipai"; 
const char* password = "12345678"; 

const char* apiUrl = "https://192.168.56.1:5001/api/esp32";  
const char* toggleRelayApiUrl = "https://192.168.56.1:5001/api/esp32/toggle";  
const char* batteryRelayApiUrl = "https://192.168.56.1:5001/api/esp32/charge-battery";  
const char* voltageApiUrl = "http://192.168.56.1:5001/api/esp32/voltage"; 

float R1 = 30000.0;
float R2 = 7500.0;

bool acState = false;  // Tracks AC state
bool batteryCharging = false;  // Tracks Battery Charging state
bool lastButtonState = HIGH; 
unsigned long lastDebounceTime = 0;
const unsigned long debounceDelay = 300;

WebServer server(80);

void setup() {
  Serial.begin(115200);
  delay(3000); // Time to open Serial Monitor

  pinMode(VOLTAGE_PIN, INPUT);
  pinMode(RELAY1_PIN, OUTPUT);
  pinMode(RELAY2_PIN, OUTPUT);
  digitalWrite(RELAY1_PIN, LOW);  // NC default (relay off)
  digitalWrite(RELAY2_PIN, LOW);  // NC default (relay off)

  pinMode(BUTTON_PIN, INPUT_PULLUP);

  WiFi.begin(ssid, password);
  Serial.print("Connecting to Wi-Fi");

  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }

  Serial.println("\n✅ Connected to Wi-Fi!");
  Serial.print("ESP32 IP Address: ");
  Serial.println(WiFi.localIP());

  server.on("/toggle-relay", HTTP_GET, []() {
    togglePowerGrid();
    server.send(200, "application/json", "{\"relay\":\"toggled\"}");
});

 server.on("/charge-battery", HTTP_GET, []() {
    toggleBatteryCharging();
    server.send(200, "application/json", batteryCharging ? "{\"battery\":\"charging\"}" : "{\"battery\":\"not charging\"}");
});

  server.begin();
}

void loop() {
  server.handleClient();

  bool buttonState = digitalRead(BUTTON_PIN);

  if (buttonState == HIGH && lastButtonState == LOW && (millis() - lastDebounceTime) > debounceDelay) {
    togglePowerGrid();
    lastDebounceTime = millis();
  }

  lastButtonState = buttonState;

  sendVoltageReading();

  delay(5000);
}

void sendVoltageReading() {
  int adcValue = analogRead(VOLTAGE_PIN);
  float vout = (adcValue * 3.3) / 4095.0;
  float vin = vout / (R2 / (R1 + R2));

  Serial.print("Battery Voltage: ");
  Serial.print(vin, 2);
  Serial.println("V");

  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(voltageApiUrl);
    http.addHeader("Content-Type", "application/json");

    String payload = "{\"voltage\":" + String(vin, 2) + "}";
    Serial.print("Sending voltage JSON: ");
    Serial.println(payload);

    int httpResponseCode = http.POST(payload);
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
    String response = http.getString();
    Serial.print("Response: ");
    Serial.println(response);
    
    http.end();
  } else {
    Serial.println("Wi-Fi Disconnected. Cannot send voltage reading.");
  }
}


void togglePowerGrid() {
  acState = !acState;
  digitalWrite(RELAY1_PIN, acState ? HIGH : LOW);  // Toggle NC relay
  String message = acState ? "Power Grid ON" : "Power Grid OFF";
  Serial.println(message);
  sendDataToApi(toggleRelayApiUrl, message);
}

void toggleBatteryCharging() {
  batteryCharging = !batteryCharging;
  digitalWrite(RELAY2_PIN, batteryCharging ? HIGH : LOW);  // Toggle NC relay
  String message = batteryCharging ? "Battery Charging ON" : "Battery Charging OFF";
  Serial.println(message);
  sendDataToApi(batteryRelayApiUrl, message);
}

void sendDataToApi(const char* url, String message) {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(url);
    http.addHeader("Content-Type", "application/json");

    String jsonPayload = "{\"message\":\"" + message + "\"}";
    Serial.print("Sending JSON: ");
    Serial.println(jsonPayload);

    int httpResponseCode = http.POST(jsonPayload);
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);

    http.end();
  } else {
    Serial.println("Wi-Fi Disconnected. Cannot send API request.");
  }
}
