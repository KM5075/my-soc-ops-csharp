using SocOps.Models;
using System.Text.Json;
using Microsoft.JSInterop;

namespace SocOps.Services;

public class BingoGameService
{
    private const string STORAGE_KEY = "bingo-game-state";
    private const int STORAGE_VERSION = 1;

    private readonly IJSRuntime _jsRuntime;

    public GameState CurrentGameState { get; private set; } = GameState.Start;
    public PlayMode CurrentMode { get; private set; } = PlayMode.Bingo;
    public List<BingoSquareData> Board { get; private set; } = new();
    public BingoLine? WinningLine { get; private set; }
    public HashSet<int> WinningSquareIds => BingoLogicService.GetWinningSquareIds(WinningLine);
    public bool ShowBingoModal { get; private set; }
    public int MarkedCount => Board.Count(s => s.IsMarked);
    public int TotalCount => Board.Count;
    public int ProgressPercent => TotalCount == 0 ? 0 : (int)Math.Round((double)MarkedCount / TotalCount * 100);

    public event Action? OnStateChanged;

    public BingoGameService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        await LoadGameStateAsync();
    }

    public void StartGame()
    {
        CurrentMode = PlayMode.Bingo;
        Board = BingoLogicService.GenerateBoard();
        WinningLine = null;
        CurrentGameState = GameState.Playing;
        ShowBingoModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void StartScavengerGame()
    {
        CurrentMode = PlayMode.ScavengerHunt;
        Board = BingoLogicService.GenerateBoard();
        WinningLine = null;
        CurrentGameState = GameState.ScavengerPlaying;
        ShowBingoModal = false;
        NotifyStateChanged();
    }

    public void HandleSquareClick(int squareId)
    {
        Board = BingoLogicService.ToggleSquare(Board, squareId);

        // Check for bingo after toggling
        if (WinningLine == null)
        {
            var bingo = BingoLogicService.CheckBingo(Board);
            if (bingo != null)
            {
                WinningLine = bingo;
                if (CurrentMode == PlayMode.ScavengerHunt)
                {
                    CurrentGameState = GameState.ScavengerBingo;
                }
                else
                {
                    CurrentGameState = GameState.Bingo;
                    ShowBingoModal = true;
                }
            }
        }

        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void ResetGame()
    {
        CurrentGameState = GameState.Start;
        CurrentMode = PlayMode.Bingo;
        Board = new();
        WinningLine = null;
        ShowBingoModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void DismissModal()
    {
        ShowBingoModal = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();

    private async Task LoadGameStateAsync()
    {
        try
        {
            var saved = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", STORAGE_KEY);
            if (!string.IsNullOrEmpty(saved))
            {
                var data = JsonSerializer.Deserialize<StoredGameData>(saved);
                if (data != null && data.Version == STORAGE_VERSION)
                {
                    CurrentGameState = data.GameState;
                    Board = data.Board;
                    WinningLine = data.WinningLine;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load game state: {ex.Message}");
        }
    }

    private async Task SaveGameStateAsync()
    {
        try
        {
            if (CurrentMode == PlayMode.ScavengerHunt)
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", STORAGE_KEY);
                return;
            }

            var data = new StoredGameData
            {
                Version = STORAGE_VERSION,
                GameState = CurrentGameState,
                Board = Board,
                WinningLine = WinningLine
            };
            var json = JsonSerializer.Serialize(data);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save game state: {ex.Message}");
        }
    }

    private class StoredGameData
    {
        public int Version { get; set; }
        public GameState GameState { get; set; }
        public PlayMode Mode { get; set; } = PlayMode.Bingo;
        public List<BingoSquareData> Board { get; set; } = new();
        public BingoLine? WinningLine { get; set; }
    }
}
