# The Random Fact app
This example app uses a random fact app API to display random facts to the user and has some other basic geolocation functionality.

## 🛠️ Technology Stack

- **Framework**: .NET 8 MAUI (Multi-platform App UI)
- **IDE**: Developed in **Visual Studio**

## 👨‍💻 Development Prerequisites

- **Visual Studio 2022+** (with .NET MAUI workload installed)
- **.NET 8 SDK**

## 👨‍🏭 Architecture

- Uses the HttpClientFactory (`Microsoft.Extensions.Http`) to create a(testable) API client using Dependency Injection. This client is configured in the `Program.cs`
- ViewModels with compiled bindings to seperate UI logic from Domain logic and the UI itself (MVVM pattern)
- Has a custom map wrapper that allows MVVM binding to the Map control (credits to: https://dev.to/symbiogenesis/use-net-maui-map-control-with-mvvm-dfl)
- A simple (and fairly useless) websocket implementation
