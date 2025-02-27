#include <WiFi.h>
#include <HTTPClient.h>

const char* ssid = "H3601P_1D8F"; // wifi name

const char* password = "bku2E9b7GK"; // wifi password
const char* serverUrl = "https://192.168.1.4:5001/api/esp32/send-data";  // Replace with your API URL

void setup() {
    Serial.begin(115200);
    WiFi.begin(ssid, password);

    Serial.print("Connecting to Wi-Fi");
    while (WiFi.status() != WL_CONNECTED) {
        delay(1000);
        Serial.print(".");
    }
    Serial.println("\n✅ Connected to Wi-Fi!");
}

void loop() {
    if (WiFi.status() == WL_CONNECTED) {
        HTTPClient http;
        http.begin(serverUrl);
        http.addHeader("Content-Type", "application/json");

        // Simulated sensor readings
        float voltage = 12.5;  
        float current = 2.3;   

        // JSON Data
        String jsonData = "{\"DeviceId\":\"ESP32_1\", \"Voltage\":" + String(voltage) + ", \"Current\":" + String(current) + "}";

        // Send POST request
        int httpResponseCode = http.POST(jsonData);

        Serial.print("HTTP Response code: ");
        Serial.println(httpResponseCode);
        http.end();
    }

    delay(5000);  // Send data every 5 seconds
}
