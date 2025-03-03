#define RELAY_PIN 4   // GPIO connected to relay
#define BUTTON_PIN 5  // GPIO connected to button

bool relayState = false;  // Tracks relay state
bool lastButtonState = HIGH;  // Stores last button state

void setup() {
  pinMode(RELAY_PIN, OUTPUT);
  digitalWrite(RELAY_PIN, HIGH);  // Relay OFF initially (NC active - LED2 ON)

  pinMode(BUTTON_PIN, INPUT_PULLUP);  // Internal pull-up for button
  Serial.begin(115200);
}

void loop() {
  bool buttonState = digitalRead(BUTTON_PIN);

  // Detect button press (transition from HIGH to LOW)
  if (buttonState == LOW && lastButtonState == HIGH) {
    relayState = !relayState;  // Toggle relay state
    digitalWrite(RELAY_PIN, relayState ? LOW : HIGH);  // Activate relay

    Serial.println(relayState ? "Relay ON (NO active - LED1 ON)" : "Relay OFF (NC active - LED2 ON)");
    delay(300);  // Debounce delay
  }

  lastButtonState = buttonState;  // Store last button state
}
