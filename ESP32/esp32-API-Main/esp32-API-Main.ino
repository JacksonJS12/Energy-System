#include <WiFi.h>
#include <WiFiClient.h>
#include <WebServer.h>

#define RELAY_PIN 4   // GPIO connected to relay

const char* ssid = "H3601P_1D8F"; 
const char* password = "bku2E9b7GK"; 

WebServer server(80);

bool relayState = false;  // Tracks relay state

void setup() {
    Serial.begin(115200);
    pinMode(RELAY_PIN, OUTPUT);
    digitalWrite(RELAY_PIN, HIGH);  

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
    server.handleClient();  // Handle incoming API requests
}
