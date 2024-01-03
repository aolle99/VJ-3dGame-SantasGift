using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class LevelScript : MonoBehaviour
    {
        private VisualElement root;
        private Color _blueSelected = new Color(74f, 101f, 132f, 255f);
        private Color _redSelected = new Color(141f, 85f, 88f, 255f);
        private Color _blueNotSelected = new Color(74f, 101f, 132f, 80f);
        private Color _redNotSelected = new Color(141f, 85f, 88f, 80f);
        
        private void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
            pauseMenu.style.display = DisplayStyle.None;
            
            VisualElement ammunition1 = root.Q<VisualElement>("Ammunition1");
            ammunition1.style.color = _blueSelected;
            
            VisualElement ammunition2 = root.Q<VisualElement>("Ammunition2");
            ammunition2.style.color = _redNotSelected;
            
            ProgressBar progressBar = root.Q<ProgressBar>("Bar");
            progressBar.value = GameStateManager.Instance.GetTotalGifts();
            progressBar.highValue = GameStateManager.Instance.GetMaxBlueGifts() + GameStateManager.Instance.GetMaxRedGifts();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
                pauseMenu.style.display = DisplayStyle.Flex;
                Time.timeScale = 0;
            }
            
            if(Input.GetKeyDown(KeyCode.X))
            {
                GameStateManager.Instance.ChangeAmmunition();
                Boolean isBlue = GameStateManager.Instance.GetAmmunitionSelected();
                VisualElement ammunition1 = root.Q<VisualElement>("Ammunition1");
                VisualElement ammunition2 = root.Q<VisualElement>("Ammunition2");

                if (isBlue)
                {
                    Color blue, lightRed;
                    ColorUtility.TryParseHtmlString("#8D555850", out lightRed);
                    ColorUtility.TryParseHtmlString("#4A6584", out blue);
                    ammunition1.style.backgroundColor = new StyleColor(blue);
                    ammunition2.style.backgroundColor = new StyleColor(lightRed);
                } 
                else
                {
                    Color lightBlue, red;
                    ColorUtility.TryParseHtmlString("#8D5558", out red);
                    ColorUtility.TryParseHtmlString("#4A658450", out lightBlue);
                    ammunition1.style.backgroundColor = new StyleColor(lightBlue);
                    ammunition2.style.backgroundColor = new StyleColor(red);
                }
            }
            
            // Update ammunition numbers
            Label numAmmunition1 = root.Q<Label>("NumAmmunition1");
            numAmmunition1.text = GameStateManager.Instance.GetBlueGifts().ToString();
            
            Label numAmmunition2 = root.Q<Label>("NumAmmunition2");
            numAmmunition2.text = GameStateManager.Instance.GetRedGifts().ToString();
            
            ProgressBar progressBar = root.Q<ProgressBar>("Bar");
            progressBar.value = GameStateManager.Instance.GetTotalGifts();
            Label label = root.Q<Label>("GiftsLeft");
            label.text = progressBar.value.ToString();
            
            ManageMainMenu();
        }

        private void ManageMainMenu()
        {
            Button button = root.Q<Button>("Quit");
            button.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1;
            };
            
            Button resumeButton = root.Q<Button>("Resume");
            resumeButton.clicked += () =>
            {
                VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
                pauseMenu.style.display = DisplayStyle.None;
                Time.timeScale = 1;
            };
            
            Button restartButton = root.Q<Button>("Restart");
            restartButton.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
                Time.timeScale = 1;
            };
        }

        public void UpdateNumGifts()
        {
            
        }
    }
}