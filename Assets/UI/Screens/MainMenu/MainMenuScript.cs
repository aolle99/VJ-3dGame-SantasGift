using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuScript : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            
            Button button = root.Q<Button>("Play");
            //when button clicked load firstlevel sce
            button.clicked += () =>
            {
                print("clicked");
                UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
            };
            
            Button instructionsButton = root.Q<Button>("Instructions");
            //when button clicked quit
            instructionsButton.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Instructions");
            };
            
            Button creditsButton = root.Q<Button>("Credits");
            //when button clicked quit
            creditsButton.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
            };
            
            Button quitButton = root.Q<Button>("Quit");
            //when button clicked quit
            quitButton.clicked += () =>
            {
                print("quit");
                Application.Quit();
            };
            
            
        }
    }
}