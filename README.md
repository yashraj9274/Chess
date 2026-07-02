# Playable Offline Chess Game

A fully playable 2-player offline Chess game built with C# and WPF in .NET 8.

## Features

- **Interactive Board**: Click pieces to view their legal moves highlighted directly on the board.
- **Move Validation**: Full validation to prevent illegal moves, including preventing moves that leave the King in check.
- **Check and Checkmate Detection**: Automatically detects Checks, Checkmates, and Stalemates.
- **Visual Warning**: The King glows red when placed in check.
- **Game Over Handling**: Clear game over screen with a "Restart" button to easily play again.
- **Modular Codebase**: Chess game logic is separated into a `ChessLogic` class library, making it highly portable and decoupled from the UI.

*(Note: Advanced special moves like En Passant, Castling, and Pawn Promotion are omitted in this standard-play release).*

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or later installed on your machine.
- Windows OS (WPF is a Windows-only framework).
- An IDE like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) (optional).

## How to Run

### Using the Standalone Executable

If you just want to play the game without installing developer tools, you can run the standalone pre-built executable:

1. Navigate to the release directory: `AppRelease/`
2. Double-click the `Chess UI.exe` application to start playing immediately.

*(This standalone executable has the .NET runtime bundled within it, meaning it will run perfectly on any Windows machine even if the user hasn't installed .NET!)*

### Using the Command Line (.NET CLI)

1. Open a terminal (Command Prompt or PowerShell).
2. Navigate to the root directory of this project where the `.sln` file is located:
   ```powershell
   cd path\to\Chess
   ```
3. Build and run the application via the UI project:
   ```powershell
   dotnet run --project "Chess UI"
   ```

### Using Visual Studio

1. Double-click the `Chess.sln` file to open the solution in Visual Studio.
2. In the Solution Explorer, ensure that `Chess UI` is set as the **Startup Project** (right-click `Chess UI` -> Set as Startup Project).
3. Press `F5` or click the **Start** button at the top to build and launch the game.

## Architecture

This project is divided into two main components:
- **`ChessLogic`**: A class library defining the core rules, piece classes, movement bounds, board state, and turn mechanisms.
- **`Chess UI`**: A Windows Presentation Foundation (WPF) application that consumes the logic library and manages graphics, interactions, and visuals.