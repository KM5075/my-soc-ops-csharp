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

## Design Guide

- Build intentional themes, not default-looking UI; each redesign should have a clear visual concept.
- Define and use CSS variables in `SocOps/wwwroot/css/app.css` for palette, contrast, and reusable theme tokens.
- Prefer expressive typography (avoid generic default stacks) while keeping readability strong on mobile.
- Use layered backgrounds (gradients, subtle patterns, depth) instead of flat single-color screens.
- Add meaningful motion at key moments (screen entry, bingo celebration, state change) and keep it lightweight.
- Preserve gameplay clarity first: square text must remain legible, marked/winning/free states must be visually distinct.
- Respect existing component boundaries in `SocOps/Components/` and avoid coupling visual changes to game logic.
- Keep responsive behavior explicit for small screens; verify board fit, tap targets, and text wrapping.
