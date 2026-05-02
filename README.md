# ChessGame
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET 8](https://img.shields.io/badge/.NET%208.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![WPF](https://img.shields.io/badge/WPF-0C54C2?style=for-the-badge&logo=windows&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
[![MVVM](https://img.shields.io/badge/Architecture-MVVM-0078D4?style=for-the-badge)](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm)
[![LAN Multiplayer](https://img.shields.io/badge/Multiplayer-LAN-2EA043?style=for-the-badge&logo=cisco&logoColor=white)](#)

A desktop multiplayer chess game that allows two players to play against each other over a Local Area Network (LAN). Built with C# and WPF, featuring professional architecture with SOLID principles, design patterns, and clean code practices.

---
## Features



---

## Programming Principles

### **S - Single Responsibility Principle**
Each class has a single reason to change, promoting maintainability:
- `GameService` - manages game state and player turns
- `ChessValidator` - validates chess rules and legal moves
- `TcpNetworkService` - manages network communication
- `NavigationService` - controls UI navigationRules
NavigationServiceIMove

**Reference:**
- [GameService.cs](ChessApplication/Services/Game/GameService.cs)
- [TcpNetworkService.cs](ChessInfrastructure/Network/TcpNetworkService.cs)
- [ChessValidator.cs](ChessLibrary/Rules/Validation/ChessValidator.cs)
- [NavigationService.cs](ChessGame/Utils/NavigationService.cs)

### **O - Open/Closed Principle**
Code is open for extension but closed for modification:
- New move strategies can be added without modifying existing pieces
- New end-game rules can be registered without changing the pipeline
- New message handlers can be added without touching existing ones

**Reference:** 
- [Moves/Strategies](ChessLibrary/Moves/Strategies)
- [Rules](ChessLibrary/Rules)
- [Handlers](ChessInfrastructure/DTO/Handlers)

### **L - Liskov Substitution Principle**
All piece subclasses can be substituted for the base Piece class:
- `Bishop`, `Knight`, `Rook`, `Queen`, `King`, `Pawn` inherit from `Piece`
- Each piece can be used interchangeably through the base interfaceHand
- No special casting or type checking required Piece

**Reference:** [Piece.cs](ChessLibrary/Pieces/Piece.cs)

#### **I - Interface Segregation Principle**
Clients depend only on interfaces they need:
- `ISubPieceFactory` - minimal interface for individual piece creation
- `IPieceFactory` - interface for factory composition
- `IMoveStrategy` - specific interface for movement algorithms
- `IEndGameRule` - specific interface for end-game conditions

**Reference:** 
- [ISubPieceFactory.cs](ChessLibrary/Factories/PieceFactories/ISubPieceFactory.cs)
- [IPieceFactory.cs](ChessLibrary/Factories/IPieceFactory.cs)

**Usage:**
- [PieceFactory.cs](ChessLibrary/Factories/PieceFactory.cs)

### **D - Dependency Inversion Principle**
High-level modules depend on abstractions, not concrete implementations:
- Dependency Injection through Microsoft.Extensions.DependencyInjection
- All dependencies passed through constructors
- Configuration centralized in App.xaml.cs

**Reference:** [App.xaml.cs](ChessGame/App.xaml.cs)

**Usage:**
- [PieceFactory.cs](ChessLibrary/Factories/PieceFactory.cs)

### **DRY (Don't Repeat Yourself)**
- Reusable NotifyPropertyChanged() in `BaseViewModel.cs`
- Common factory infrastructure shared across all piece factories
- Shared validation and rule checking utilities

**Reference:** [BaseViewModel.cs](ChessGame/ViewModel/Base/BaseViewModel.cs)

---
