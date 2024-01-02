using UnityEngine;

public class GameStateManager
{
    [SerializeField] private int _maxGifts;
    [SerializeField] private int _redGifts;
    [SerializeField] private int _blueGifts;
    private static GameStateManager _instance;

    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameStateManager();
            }

            return _instance;
        }
    }
    
    public GameState CurrentGameState { get; private set; }
    
    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChange;

    private GameStateManager()
    {
        
    }
    
    public void SetGameState(GameState newGameState)
    {
        if(newGameState == CurrentGameState) return;
        
        CurrentGameState = newGameState;
        OnGameStateChange?.Invoke(newGameState);
    }
    
    public void AddRedGift()
    {
        _redGifts++;
    }
    
    public void AddBlueGift()
    {
        _blueGifts++;
    }
    
    public void RemoveRedGift()
    {
        _redGifts--;
    }
    
    public void RemoveBlueGift()
    {
        _blueGifts--;
    }
    
    public int GetRedGifts()
    {
        return _redGifts;
    }
    
    public int GetBlueGifts()
    {
        return _blueGifts;
    }
}