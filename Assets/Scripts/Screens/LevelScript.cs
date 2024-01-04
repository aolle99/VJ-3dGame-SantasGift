using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens
{
    public class LevelScript : MonoBehaviour
    {
        private VisualElement root;
        private GiftStateManager _giftStateManager;
        private float deltaTime = 0.0f;
        
        private void Start()
        {
            _giftStateManager = GiftStateManager.Instance;
            root = GetComponent<UIDocument>().rootVisualElement;
            VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
            pauseMenu.style.display = DisplayStyle.None;
            
            VisualElement ammunition1 = root.Q<VisualElement>("Ammunition1");
            ammunition1.style.color = new Color(74f, 101f, 132f, 255f);
            
            VisualElement ammunition2 = root.Q<VisualElement>("Ammunition2");
            ammunition2.style.color = new Color(141f, 85f, 88f, 80f);
            
            ProgressBar progressBar = root.Q<ProgressBar>("Bar");
            progressBar.value = _giftStateManager.GetTotalGifts();
            progressBar.highValue = _giftStateManager.GetMaxBlueGifts() + _giftStateManager.GetMaxRedGifts();
        }

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                VisualElement pauseMenu = root.Q<VisualElement>("PauseMenu");
                pauseMenu.style.display = DisplayStyle.Flex;
                Time.timeScale = 0;
            }
            
            
            VisualElement ammunition1 = root.Q<VisualElement>("Ammunition1");
            VisualElement ammunition2 = root.Q<VisualElement>("Ammunition2");

            if (_giftStateManager.GetAmmunitionSelected() == GiftType.Blue)
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
            
            // Update ammunition numbers
            Label numAmmunition1 = root.Q<Label>("NumAmmunition1");
            numAmmunition1.text = _giftStateManager.GetBlueGifts().ToString();
            
            Label numAmmunition2 = root.Q<Label>("NumAmmunition2");
            numAmmunition2.text = _giftStateManager.GetRedGifts().ToString();
            
            ProgressBar progressBar = root.Q<ProgressBar>("Bar");
            progressBar.value = _giftStateManager.GetTotalGifts();
            Label label = root.Q<Label>("GiftsLeft");
            label.text = progressBar.value.ToString();
            
            ManageMainMenu();
            
            DisplayFPS();
        }
        
        void DisplayFPS()
        {
            int fps = Mathf.RoundToInt(1.0f / deltaTime);
            string text = $"FPS: {fps}";
            
            Label label = root.Q<Label>("FPS");
            label.text = text;
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