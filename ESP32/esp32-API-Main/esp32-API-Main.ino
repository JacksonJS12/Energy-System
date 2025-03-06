#include <WiFi.h>
#include <WiFiClient.h>
#include <WebServer.h>
#include <HTTPClient.h>

#define RELAY_PIN 4   // GPIO connected to relay (if needed)
#define BUTTON_PIN 5  // GPIO connected to button

const char* ssid = "H3601P_1D8F"; 
const char* password = "bku2E9b7GK"; 

const char* apiUrl = "https://192.168.1.4:5001/api/esp32/button";  // Replace with actual API IP

bool acState = false;  // Tracks AC simulation state
bool lastButtonState = HIGH; 
unsigned long lastDebounceTime = 0;  // Tracks last button press time
const unsigned long debounceDelay = 300;  // Debounce time to avoid double presses

WebServer server(80);
bool relayState = false;  // Tracks relay state

void setup() {
    Serial.begin(115200);
    pinMode(RELAY_PIN, OUTPUT);
    digitalWrite(RELAY_PIN, HIGH);  // Default relay OFF

    pinMode(BUTTON_PIN, INPUT_PULLUP); // Internal pull-up resistor for button

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

    // Define API endpoint for toggling relay
    server.on("/toggle-relay", HTTP_GET, []() {
        relayState = !relayState;
        digitalWrite(RELAY_PIN, relayState ? LOW : HIGH);  // Toggle relay
        Serial.println(relayState ? "Relay ON" : "Relay OFF");

        server.send(200, "application/json", relayState ? "{\"relay\":\"on\"}" : "{\"relay\":\"off\"}");
    });

    server.begin();
}

void loop() {
    server.handleClient();  // Handle API requests

    bool buttonState = digitalRead(BUTTON_PIN);

    // Check if button was pressed and released (debounced)
    if (buttonState == LOW && lastButtonState == HIGH && (millis() - lastDebounceTime) > debounceDelay) {
        acState = !acState;  // Toggle AC state

        // Determine message to send
        String message = acState ? "Button is pressed. AC is now ON." : "Button is pressed. AC is now OFF.";

        // Send message to API
        sendButtonPressToApi(message);

        Serial.println(message);  // Debugging output

        lastDebounceTime = millis();  // Update debounce time
    }

    lastButtonState = buttonState; // Store last button state
}

void sendButtonPressToApi(String message) {
    if (WiFi.status() == WL_CONNECTED) {
        HTTPClient http;
        http.begin(apiUrl);
        http.addHeader("Content-Type", "application/json");

        String jsonPayload = "{\"message\":\"" + message + "\"}";
        Serial.print("Sending JSON: ");
        Serial.println(jsonPayload);

        int httpResponseCode = http.POST(jsonPayload);

        Serial.print("HTTP Response code: ");
        Serial.println(httpResponseCode);

        if (httpResponseCode > 0) {
            String response = http.getString();
            Serial.print("API Response: ");
            Serial.println(response);
        } else {
            Serial.println("Failed to send data to API. Possible network issue.");
        }

        http.end();
    } else {
        Serial.println("Wi-Fi Disconnected. Cannot send API request.");
    }
}
