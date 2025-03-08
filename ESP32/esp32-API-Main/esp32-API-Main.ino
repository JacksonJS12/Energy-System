#include <WiFi.h>
#include <WiFiClient.h>
#include <WebServer.h>
#include <HTTPClient.h>

#define RELAY1_PIN 4   // AC Control (Normally Closed)
#define RELAY2_PIN 6   // Battery Charging Control (Normally Closed)
#define BUTTON_PIN 5   // Button to toggle AC state

const char* ssid = "ne_pipai"; 
const char* password = "12345678"; 

const char* apiUrl = "https://192.168.138.92:5001/api/esp32/button";  
const char* toggleRelayApiUrl = "https://192.168.138.92:5001/api/esp32/toggle";  
const char* batteryRelayApiUrl = "https://192.168.138.92:5001/api/esp32/charge-battery";  

bool acState = false;  // Tracks AC state
bool batteryCharging = false;  // Tracks Battery Charging state
bool lastButtonState = HIGH; 
unsigned long lastDebounceTime = 0;
const unsigned long debounceDelay = 300;
bool relayState = false;
WebServer server(80);

void setup() {
    Serial.begin(115200);
    
    pinMode(RELAY1_PIN, OUTPUT);
    pinMode(RELAY2_PIN, OUTPUT);
    digitalWrite(RELAY1_PIN, LOW);  // NC default state
    digitalWrite(RELAY2_PIN, LOW);  // NC default state

    pinMode(BUTTON_PIN, INPUT_PULLUP);

    // Connect to Wi-Fi
    WiFi.begin(ssid, password);
    Serial.print("Connecting to Wi-Fi");

    while (WiFi.status() != WL_CONNECTED) {
        delay(1000);
        Serial.print(".");
    }
    Serial.println("\n✅ Connected to Wi-Fi!");
    Serial.print("ESP32 IP Address: ");
    Serial.println(WiFi.localIP());

    // Define API endpoint for toggling relay via local web server
    server.on("/toggle-relay", HTTP_GET, []() {
        toggleRelay();
        server.send(200, "application/json", relayState ? "{\"relay\":\"on\"}" : "{\"relay\":\"off\"}");
    });

    server.on("/charge-battery", HTTP_GET, []() {
        toggleBatteryCharging();
        server.send(200, "application/json", batteryCharging ? "{\"battery\":\"charging\"}" : "{\"battery\":\"not charging\"}");
    });

    server.begin();
}

void loop() {
    server.handleClient();  // Handle local API requests

    bool buttonState = digitalRead(BUTTON_PIN);

    if (buttonState == LOW && lastButtonState == HIGH && (millis() - lastDebounceTime) > debounceDelay) {
        acState = !acState;
        toggleRelay();

        String message = acState ? "Button is pressed. AC is now ON." : "Button is pressed. AC is now OFF.";
        sendDataToApi(apiUrl, message);

        Serial.println(message);
        lastDebounceTime = millis();
    }

    lastButtonState = buttonState;

    delay(500);
}

void toggleRelay() {
    acState = !acState;
    digitalWrite(RELAY1_PIN, acState ? LOW : HIGH);  // Toggle NC relay
    sendDataToApi(toggleRelayApiUrl, acState ? "Relay ON" : "Relay OFF");
}

void toggleBatteryCharging() {
    batteryCharging = !batteryCharging;
    digitalWrite(RELAY2_PIN, batteryCharging ? LOW : HIGH);  // Toggle NC relay
    sendDataToApi(batteryRelayApiUrl, batteryCharging ? "Battery Charging ON" : "Battery Charging OFF");
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
