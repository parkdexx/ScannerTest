# TCP/IP Fixed Scanner Test Tool (C# WinForm)

A robust and efficient C# WinForm utility designed to test and monitor **TCP/IP-connected fixed scanners**. This tool features an automatic reconnection mechanism and asynchronous data processing to ensure stable communication in industrial environments.

---

## 🚀 Key Technical Features

* **Automatic Reconnection Loop**: A background monitoring thread continuously checks the socket status and attempts to reconnect every 1 second if the connection is lost.
* **Asynchronous I/O**: Implements `Task`-based asynchronous reading (`ReadAsync`) to ensure the UI remains responsive during data transmission.
* **Smart Packet Parsing**: Automatically identifies data boundaries by parsing Carriage Return (`\r`) or Line Feed (`\n`) delimiters, preventing data fragmentation.
* **Graceful Thread Management**: Uses status flags and `Thread.Join()` for safe and clean thread termination, preventing resource leaks.
* **Event-Driven Architecture**: Scanned data is decoupled from the logic and delivered via the `_scannerEvent` (Action<string>).

---

## 🛠 Implementation Details

* **Connectivity Check**: Utilizes the `Socket.Poll` method to detect physical connection drops that `TcpClient.Connected` might miss.
* **Buffer Handling**: Uses a 4KB buffer and `StringBuilder` for efficient memory management during high-speed scanning.
* **Error Handling**: Comprehensive `try-catch-finally` blocks ensure that network streams and clients are properly disposed of even during unexpected disconnects.

---

## 📖 How to Use

1.  **Initialize**: Provide the Scanner's IP address and Port number to the `ScannerManager`.
2.  **Start Monitoring**: Call `StartScannerThread()` to begin the auto-connect and monitoring process.
3.  **Receive Data**: Subscribe to the `_scannerEvent` to handle incoming barcode strings.
    ```csharp
    scannerManager._scannerEvent += (data) => {
        Console.WriteLine($"Scanned Data: {data}");
    };
    ```
4.  **Stop**: Call `StopScannerThread()` to safely close all connections and stop the background thread.

---

## 🗺 Future Roadmap

* **Protocol Extension**: Support for STX (Start of Text) and ETX (End of Text) framed protocols for enhanced data integrity.
* **Configurable Delimiters**: Allow users to define custom termination characters beyond CR/LF.
* **Logging System**: Implement a logging feature to track connection history and error frequency for diagnostic purposes.
* **Multi-Scanner Support**: Update the manager to handle multiple TCP/IP scanner connections simultaneously.

---

## 💻 Environment
* **Language**: C#
* **Framework**: .NET Framework 4.7.2+ / .NET 6.0+
* **Key Libraries**: `System.Net.Sockets`, `System.Threading.Tasks`
