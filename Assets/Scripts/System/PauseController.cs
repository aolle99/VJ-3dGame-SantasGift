using UnityEngine;

namespace System
{
    public class PauseController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.instance.PlaySound("StartGame");
                GameState currentGameState = GameStateManager.Instance.CurrentGameState;
                GameState newGameState = currentGameState == GameState.Gameplay ? GameState.Paused : GameState.Gameplay;
            
                GameStateManager.Instance.SetGameState(newGameState);
            }
        }
    }
}
