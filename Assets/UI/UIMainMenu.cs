using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class UIMainMenu : MonoBehaviour
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