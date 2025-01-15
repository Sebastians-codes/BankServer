BankServer

This repository contains a simple client-server application developed in C#. The project demonstrates the implementation of a TCP protocol for a banking system with both server and client components.
Project Structure

    TcpClientServer: Contains the client-side implementation.
    TcpServer: Contains the server-side implementation.

Features

    Establishes a TCP connection between the client and the server.
    Allows users to log in and perform basic banking operations.

TcpServer/Program.cs

The server is initialized with an IP address, port, and a secret key. It starts listening for incoming client connections and handles them accordingly.
TcpClientServer/Program.cs

The client is initialized similarly and attempts to connect to the server. It provides a simple console interface for user login and deposit operations.
License

This project is licensed under the MIT License.
