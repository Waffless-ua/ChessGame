### **S - Single Responsibility Principle**

Each class has a single reason to change, promoting maintainability:

* `GameService` - manages game state and player turns
* `ChessValidator` - validates chess rules and legal moves
* `TcpNetworkService` - manages network communication
* `NavigationService` - controls UI navigation

**Reference:**

* [GameService.cs](ChessApplication/Services/Game/GameService.cs)
* [TcpNetworkService.cs](ChessInfrastructure/Network/TcpNetworkService.cs)
* [ChessValidator.cs](ChessLibrary/Rules/Validation/ChessValidator.cs)
* [NavigationService.cs](ChessGame/Utils/NavigationService.cs)

---

### **O - Open/Closed Principle**

Code is open for extension but closed for modification:

* New move strategies can be added without modifying existing pieces
* New end-game rules can be registered without changing the pipeline
* New message handlers can be added without touching existing ones

**Reference:**

* [Moves/Strategies](ChessLibrary/Moves/Strategies)
* [Rules](ChessLibrary/Rules)
* [Handlers](ChessInfrastructure/DTO/Handlers)

---

### **L - Liskov Substitution Principle**

All piece subclasses can be substituted for the base `Piece` class:

* `Bishop`, `Knight`, `Rook`, `Queen`, `King`, `Pawn` inherit from `Piece`
* Each piece can be used interchangeably through the base interface
* No special casting or type checking is required

**Reference:**

* [Piece.cs](ChessLibrary/Pieces/Piece.cs)

---

### **I - Interface Segregation Principle**

Clients depend only on interfaces they need:

* `ISubPieceFactory` - minimal interface for individual piece creation
* `IPieceFactory` - interface for factory composition
* `IMoveStrategy` - specific interface for movement algorithms
* `IEndGameRule` - specific interface for end-game conditions

**Reference:**

* [ISubPieceFactory.cs](ChessLibrary/Factories/PieceFactories/ISubPieceFactory.cs)
* [IPieceFactory.cs](ChessLibrary/Factories/IPieceFactory.cs)

**Usage:**

* [PieceFactory.cs](ChessLibrary/Factories/PieceFactory.cs)

---

### **D - Dependency Inversion Principle**

High-level modules depend on abstractions, not concrete implementations:

* Dependency Injection through Microsoft.Extensions.DependencyInjection
* All dependencies passed through constructors
* Configuration centralized in `App.xaml.cs`

**Reference:**

* [App.xaml.cs](ChessGame/App.xaml.cs)

**Usage:**

* [PieceFactory.cs](ChessLibrary/Factories/PieceFactory.cs)

---

### **DRY (Don't Repeat Yourself)**

* Reusable `NotifyPropertyChanged()` in `BaseViewModel.cs`
* Common factory infrastructure shared across all piece factories
* Shared validation and rule checking utilities

**Reference:**

* [BaseViewModel.cs](ChessGame/ViewModel/Base/BaseViewModel.cs)
