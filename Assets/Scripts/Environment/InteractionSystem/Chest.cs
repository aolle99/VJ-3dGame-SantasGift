using System;
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
        
        private GiftStateManager _giftStateManager;
        
        public void Start()
        {
            _giftStateManager = GiftStateManager.Instance;
        }

        public bool Interact(Interactor interactor)
        {
            Debug.Log("Chest opened");
            _giftStateManager.AddRandomGifts();
            Destroy(_interactionPromptUI);
            Destroy(gameObject);
            return true;
        }
    }
}