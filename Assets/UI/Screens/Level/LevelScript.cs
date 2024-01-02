using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class LevelScript : MonoBehaviour
    {
        private VisualElement root;
        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
            pauseMenu.style.display = DisplayStyle.None;
        }

        private void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            ProgressBar progressBar = root.Q<ProgressBar>("Bar");
            progressBar.value = 50;
            //progressBar.style.backgroundColor = Color.green;
            
            Label label = root.Q<Label>("GiftsLeft");
            label.text = "3";
            
            Button button = root.Q<Button>("Quit");
            button.clicked += () =>
            {
                print("clicked");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            };
            
            Button resumeButton = root.Q<Button>("Resume");
            resumeButton.clicked += () =>
            {
                VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
                pauseMenu.style.display = DisplayStyle.None;
            };
            
            Button restartButton = root.Q<Button>("Restart");
            restartButton.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
            };
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
                pauseMenu.style.display = DisplayStyle.Flex;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
                pauseMenu.style.display = DisplayStyle.None;
            }
        }
    }
}