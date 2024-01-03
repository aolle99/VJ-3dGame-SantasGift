using UnityEngine;
using UnityEngine.Serialization;

namespace System
{
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        Victory
    }

    public class GameStateManager : MonoBehaviour
    {
        private static GameStateManager _instance;
    
        public static GameStateManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = FindObjectOfType<GameStateManager>();
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
    }
}
