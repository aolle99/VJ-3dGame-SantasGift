using UnityEngine;

namespace Environment.InteractionSystem
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt;
        
        public string InteractionPrompt => prompt;
        
        [SerializeField] private InteractionPromptUI _interactionPromptUI;
        
        public InteractionPromptUI InteractionPromptUI
        {
            get => _interactionPromptUI;
            set => _interactionPromptUI = value;
        }

        public bool Interact(Interactor interactor)
        {
            Debug.Log("Interacted with chest");
            return true;
        }
    }
}