using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens
{
    public class MainMenuScript : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            
            Button button = root.Q<Button>("Play");
            button.clicked += () =>
            {
                AudioManager.instance.PlaySound("StartGame");
                UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
            };
            
            Button instructionsButton = root.Q<Button>("Instructions");
            instructionsButton.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
            };
            
            Button creditsButton = root.Q<Button>("Credits");
            creditsButton.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
            };
            
            Button quitButton = root.Q<Button>("Quit");
            quitButton.clicked += () =>
            {
                Application.Quit();
            };
            
            
        }
    }
}