# Copilot Workspace Instructions

## Mandatory Development Checklist

Before finalizing any change:

- [ ] Lint passes (run the repo lint task/command when available)
- [ ] `dotnet build SocOps/SocOps.csproj` succeeds
- [ ] `dotnet test` is run when tests exist (or when test projects are added)

## Project Context

Soc Ops is a Blazor WebAssembly social bingo app on .NET 10.
Primary flow: `Home` -> start game -> mark squares.

## Architecture Map

- `SocOps/Components/`: UI (`StartScreen`, `GameScreen`, `BingoBoard`, `BingoSquare`, `BingoModal`)
- `SocOps/Services/`: state + logic (`BingoGameService`, `BingoLogicService`)
- `SocOps/Models/`: domain models
- `SocOps/Data/Questions.cs`: question data
- `SocOps/wwwroot/css/app.css`: shared utility CSS

## Preferred Commands

```bash
dotnet build SocOps/SocOps.csproj
dotnet run --project SocOps/SocOps.csproj
```

## Coding & Styling Expectations

- Follow existing Blazor/C# conventions
- Preserve event-driven updates from `BingoGameService`
- Reuse utility classes in `wwwroot/css/app.css`
- Keep changes scoped; avoid unrelated refactors/dependencies
