const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5050/datahub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start().then(() => {
    console.log("Connected to SignalR Hub");
}).catch(err => console.error(err.toString()));

connection.on("ReceiveData", (esp32Id, voltage, current) => {
    console.log(`🔹 ESP32: ${esp32Id} | Voltage: ${voltage}V | Current: ${current}A`);
    document.getElementById("voltage").innerText = voltage + "V";
    document.getElementById("current").innerText = current + "A";
});
