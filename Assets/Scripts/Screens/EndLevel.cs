using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens
{
    public class EndLevel : MonoBehaviour
    {
        VisualElement root;
        private Boolean _isLoaded = false;
        
        private void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            
            Button playAgain = root.Q<Button>("PlayAgain");
            playAgain.clicked += () =>
            {
                print("clicked");
                UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
            };
            
            Button mainMenu = root.Q<Button>("MainMenu");
            mainMenu.clicked += () =>
            {
                print("clicked");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            };

        }
    }
}