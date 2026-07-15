<div align="center">

# 🌍 Europe Domination

**A real-time grand strategy game set in 18th-century Europe, built with Godot 4 and C#.**

[![Godot Engine](https://img.shields.io/badge/Godot-4.6%20(.NET)-478CBF?style=for-the-badge&logo=godotengine&logoColor=white)]()
[![C#](https://img.shields.io/badge/C%23-.NET%208-512BD4?style=for-the-badge&logo=csharp&logoColor=white)]()
[![Steam](https://img.shields.io/badge/Steam-Multiplayer-1b2838?style=for-the-badge&logo=steam&logoColor=white)]()
[![Tests](https://img.shields.io/badge/Tests-NUnit-22a5a0?style=for-the-badge&logo=nunit&logoColor=white)]()

![demo](./demo.gif)

</div>

---

## 📌 About

**Europe Domination** is a Paradox-inspired grand strategy game. You take control of a country on a historical map of Europe and play in **real time with a pausable, multi-speed clock** — managing an economy, researching technology, waging war on land and sea, and conducting diplomacy against AI or other players.

The project is a Godot 4 / C# game with a data-driven scenario system (the bundled scenario is **Europe 1700**), a colour-coded province map rendered with custom shaders, and a clean event-driven engine ⇄ UI architecture. This repository is the public **demo** build.

> **Genre note:** this is *real-time with pause* (à la Europa Universalis), not turn-based — time advances on a day tick you can pause and speed up/slow down.

---

## ✨ Features

**World & map**
- Historical Europe map with hundreds of provinces (land, sea, coastal, colonized, uncolonized, wasteland).
- Multiple **map modes**: Political, Terrain, Goods, Trade, Development, Factories, and transportation overlays.
- **Fog of war**, minimap, and zoom-based level-of-detail (province labels curve to fit the map).

**Economy**
- Goods production and consumption: harvested goods, consumable goods, and crafted weapons/equipment.
- **Factories with recipes**, stockpiles & trade, province development, and land/sea **transportation routes**.
- Special buildings: Factory, Dockyard, Military Training Camp, Stock & Trade.

**Military**
- **Land regiments** — infantry, cavalry, artillery — and **naval regiments** — light / medium / heavy / transport ships.
- Generals and admirals, unit templates, army movement with pathfinding, and a battle-resolution system.

**Strategy layer**
- **Technology tree** with tiered levels.
- **Diplomacy** — wars and trade agreements.
- **Events** system for scripted occurrences.
- **AI** opponents driven by behavioural patterns.

**Meta**
- **Steam multiplayer** via lobbies (Facepunch.Steamworks).
- **Save / load** with custom JSON serialization.
- Multiple game modes: Random Spawn, Selection Spawn, Full-Map Scenario.

---

## 🎮 Controls

| Input | Action |
|-------|--------|
| Arrow keys / middle-mouse drag | Pan the camera |
| Mouse wheel | Zoom in / out |
| `P` | Pause / unpause time |
| `` ` `` (backtick) | Toggle debug console |
| `Esc` | Escape / settings menu |
| `Tab` | Toggle overlay |
| Left-click | Select province / unit |

Time speed is adjustable in-game via the time controls.

---

## 🛠️ Tech stack

- **Engine:** [Godot Engine 4.6](https://godotengine.org/) (.NET / Mono build), Forward+ renderer
- **Language:** C# targeting **.NET 8** (`Godot.NET.Sdk 4.6.1`)
- **Multiplayer/Platform:** [Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks) 2.3.4
- **Testing:** NUnit 4 + `Microsoft.NET.Test.Sdk`
- **Shaders:** custom GDShaders for the province map, occupied-territory overlay, minimap, fog of war, and curved map text

---

## 🧠 Architecture

The game is organized around a central engine node with an **event-driven split between simulation and UI**:

- **[`GlobalStrategyEngine`](Scripts/GlobalStrategyEngine.cs)** (`Node2D`) — the root engine. It owns the day-tick `Timer`, the camera, and coordinates the gameplay **handlers** through a `CallMulticaster`.
- **Handlers** ([`Scripts/Handlers/`](Scripts/Handlers)) — `MapHandler`, `ArmyHandler`, `AiHandler`, `SelectorBoxHandler`, `BattleHandler`, `MultiplayerHandler`, each owning one slice of gameplay.
- **Event buses** ([`Scripts/UI/Events/`](Scripts/UI/Events)) — three one-way channels keep the engine and GUI decoupled:
  - `GUI*` events: player input from the UI → engine
  - `ToEngine*` events: requests routed into the simulation
  - `ToGUI*` events: state updates pushed to the UI
- **Province picking** — the map is a colour-coded texture; a pixel's colour maps to a province ID via [`GameMath`](Scripts/Utils/Math/GameMath.cs), the classic grand-strategy approach.
- **Global state** ([`Scripts/GlobalStates/`](Scripts/GlobalStates)) — `EngineState`, `GlobalResources`, `SteamState`, `GameSettings`, `MultiplayerState`.
- **Scenario model** ([`Scripts/Scenarios/`](Scripts/Scenarios)) — plain C# data classes (countries, provinces, goods, army, technology, diplomacy, events) that define a playable world; scenarios can be custom-built and are loaded from the `Scenarios/` data.
- **Utilities** ([`Scripts/Utils/`](Scripts/Utils)) — geometry & **pathfinding** (`PathFinder`, `Polygon`, `Sector`, `Dsu`), map-label **text solvers**, and JSON converters for save/load.

---

## 🚀 Getting started

### Prerequisites
- [**Godot 4.6 — .NET/Mono version**](https://godotengine.org/download) (the C# build; the standard build cannot compile C#)
- **.NET 8 SDK**
- A running Steam client (the game initializes Steam via the SDK's test App ID `480` for development)

### Run the game
```sh
git clone https://github.com/Pazzann/EuropeDomination.git
cd EuropeDomination
```
1. Open **Godot 4.6 (.NET)** and **Import** the `project.godot` file.
2. Click **Build** (top-right of the editor) to compile the C# assemblies.
3. Press **F5** / **Play** to launch (main scene is [`main.tscn`](main.tscn)).

The `Europe 1700` scenario data ships in [`Scenarios/`](Scenarios) and is copied to the build output automatically.

### Run the tests
The solution includes a separate NUnit test project ([`Tests/Tests.csproj`](Tests/Tests.csproj)):
```sh
dotnet test
```

---

## 🗂️ Project structure

```
├── project.godot            # Godot project config (input maps, display, autoloads)
├── main.tscn                # Entry scene
├── EuropeDominationDemo.sln # Solution (game + tests)
├── Scripts/
│   ├── GlobalStrategyEngine.cs  # Central engine node
│   ├── Handlers/                # Map, Army, AI, Battle, Multiplayer, Selection handlers
│   ├── GlobalStates/            # Engine/game/Steam/multiplayer global state
│   ├── Scenarios/               # Data model: countries, provinces, army, goods, tech, diplomacy, events
│   ├── UI/                      # GUI, GUI handlers, and the event buses (GUI / ToEngine / ToGUI)
│   ├── Units/                   # Army units & path handling
│   ├── Enums/                   # Game modes, map types, province types, unit states, ...
│   └── Utils/                   # Math & pathfinding, text solvers, JSON converters, save/load
├── Scenes/                  # GameScene, LobbyScene, Tests scenes
├── Prefabs/                 # Reusable scene prefabs
├── Shaders/                 # Map, minimap, fog-of-war, curved-text shaders
├── Sprites/ · Fonts/        # Art & typography assets
├── Scenarios/               # Packaged scenario data (Europe 1700)
└── Tests/                   # NUnit test project
```

---

<div align="center">
<i>Developed by Anton Matiash</i>

<a href="https://github.com/Pazzann">GitHub</a>
</div>
</content>
