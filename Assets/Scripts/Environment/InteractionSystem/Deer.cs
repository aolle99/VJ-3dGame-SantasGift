using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.InteractionSystem
{
    public class Deer : MonoBehaviour, IInteractable
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
            Debug.Log("Interacted with deer");
            return true;
        }
    }
}