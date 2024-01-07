using Camera;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment.InteractionSystem
{
    public class Sled : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt;
        [SerializeField] private Scene nextScene;
        
        [SerializeField ] private CameraTransition cameraTransition;
        
        public string InteractionPrompt => prompt;
        
        [SerializeField] private InteractionPromptUI _interactionPromptUI;
        
        public InteractionPromptUI InteractionPromptUI
        {
            get => _interactionPromptUI;
            set => _interactionPromptUI = value;
        }

        public bool Interact(Interactor interactor)
        {
            if (cameraTransition)
            {
                cameraTransition.StartFadeIn();
            }
            Invoke(nameof(LoadNextScene), 1.5f);
            
            return true;
        }
        
        public void LoadNextScene()
        {
            SceneManager.LoadScene(nextScene.name);
        }
    }
}