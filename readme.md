# The Random Fact app
This example app uses a random fact app API to display random facts to the user. 

## 🛠️ Technology Stack

- **Framework**: .NET 9 MAUI (Multi-platform App UI)
- **IDE**: Developed in **Visual Studio**

## 👨‍💻 Development Prerequisites

- **Visual Studio 2022+** (with .NET MAUI workload installed)
- **.NET 9 SDK**

## 👨‍🏭 Architecture

- Uses the HttpClientFactory (`Microsoft.Extensions.Http`) to create a(testable) API client using Dependency Injection. This client is configured in the `Program.cs`
- ViewModels with compiled bindings to seperate UI logic from Domain logic and the UI itself (MVVM pattern)
