using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class CreditsScript : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            
            Button button = root.Q<Button>("RMainMenu");
            //when button clicked load main menu scene
            button.clicked += () =>
            {
                print("clicked");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            };
        }
    }
}