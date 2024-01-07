using UnityEngine;
using UnityEngine.UIElements;

namespace Screens
{
    public class InstructionsScript : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            
            Button button = root.Q<Button>("RMainMenu");
            button.clicked += () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            };
        }
    }
}

