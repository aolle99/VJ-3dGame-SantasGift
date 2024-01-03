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
        [SerializeField] private int maxRedGifts = 10;
        [SerializeField] private int maxBlueGifts = 10;
        [SerializeField] private int redGifts = 10;
        [SerializeField] private int blueGifts = 10;
        [SerializeField] private bool ammunitionSelected = true;
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
        
        public void AddRedGift(int numGifts)
        {
            redGifts += numGifts;
        }
        
        public void AddBlueGift(int numGifts)
        {
            blueGifts += numGifts;
        }
        
        public void RemoveRedGift()
        {
            redGifts--;
        }
        
        public void RemoveBlueGift()
        {
            blueGifts--;
        }
        
        public int GetRedGifts()
        {
            return redGifts;
        }
        
        public int GetBlueGifts()
        {
            return blueGifts;
        }
        
        public int GetMaxRedGifts()
        {
            return maxRedGifts;
        }
        
        public int GetMaxBlueGifts()
        {
            return maxBlueGifts;
        }
        
        public int GetTotalGifts()
        {
            return redGifts + blueGifts;
        }
        
        public bool GetAmmunitionSelected()
        {
            return ammunitionSelected;
        }
    
        public void ChangeAmmunition()
        {
            ammunitionSelected = !ammunitionSelected;
        }
    }
}
