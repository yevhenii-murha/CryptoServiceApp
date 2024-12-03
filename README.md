# CryptoService

CryptoService is a WPF desktop application that allows users to view and filter cryptocurrencies using the CoinCap API. It provides functionality to load the top cryptocurrencies, view detailed information about them, and filter the list by name or symbol.

## Features

- Load and display top cryptocurrencies.
- View detailed cryptocurrency information and their markets.
- Filter cryptocurrencies by name or symbol.
- Log critical operations and errors to the console.

## Technologies

- **.NET Core** - Cross-platform application development.
- **WPF** - User interface framework.
- **CoinCap API** - Source of cryptocurrency data.
- **MVVM pattern** - Architecture for managing the UI and data.

## API Setup

Configure the CoinCap API in the `ApiConfig.cs` file:

```csharp
public static class ApiConfig
{
    public static string BaseAddress = "https://api.coincap.io/v2";
    public static string AssetsEndpoint = "/assets";
}
```

## Pages

### 1. Main Window

The main page of the application, which displays a list of the top cryptocurrencies.

![image](https://github.com/user-attachments/assets/0faf9ea3-394f-4d6d-b505-2b9c41e605f4)

### 2. Show All Cryptos Page

A page that displays all available cryptocurrencies with search functionality. It allows users to filter cryptocurrencies by name or symbol.

![image](https://github.com/user-attachments/assets/cbfde91d-519a-4161-ad4a-79a4f2d1d56b)

### 3. Crypto Details Page

Displays detailed information about a selected cryptocurrency, including its markets and other relevant data.

![image](https://github.com/user-attachments/assets/86de4d36-6d65-4e03-91cd-bea48bf3c51a)


