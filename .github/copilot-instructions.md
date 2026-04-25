# Copilot Workspace Instructions

## Mandatory Development Checklist

Before finalizing any change:

- [ ] `dotnet build SocOps/SocOps.csproj` succeeds
- [ ] `dotnet test` is run when tests exist (or when test projects are added)
- [ ] No obvious runtime regressions in the main game flow (`Home` -> start game -> mark squares)
- [ ] Keep changes scoped; avoid refactoring unrelated files

## Project Context

**Soc Ops** is a Blazor WebAssembly social bingo app built on .NET 10.
The core user flow is on `SocOps/Pages/Home.razor`, which switches between start and game screens.

## Architecture Map

- `SocOps/Components/`: UI components (`StartScreen`, `GameScreen`, `BingoBoard`, `BingoSquare`, `BingoModal`)
- `SocOps/Services/`: game logic and state (`BingoLogicService`, `BingoGameService`)
- `SocOps/Models/`: domain models (`GameState`, `BingoLine`, `BingoSquareData`)
- `SocOps/Data/Questions.cs`: question source data
- `SocOps/wwwroot/css/app.css`: utility-first CSS classes used across components

## Preferred Commands

```bash
dotnet build SocOps/SocOps.csproj
dotnet run --project SocOps/SocOps.csproj
```

## Coding & Styling Expectations

- Follow existing Blazor and C# conventions in this repo
- Preserve event-driven state updates from `BingoGameService`
- Prefer existing CSS utility classes from `wwwroot/css/app.css`
- Add comments only when logic is non-obvious
- Avoid introducing new dependencies unless clearly justified
