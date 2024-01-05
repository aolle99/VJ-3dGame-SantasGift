using System;
using UnityEngine;


namespace Environment.InteractionSystem
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt;
        [SerializeField] private bool randomGifts = true;
        [SerializeField] private int redGifts = 0;
        [SerializeField] private int blueGifts = 0;
        
        
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
            AudioManager.instance.PlaySound("GetGifts");
            if (randomGifts)
            {
                _giftStateManager.AddRandomGifts();
            }
            else
            {
                _giftStateManager.AddRedGift(redGifts);
                _giftStateManager.AddBlueGift(blueGifts);
            }
            
            Destroy(_interactionPromptUI);
            Destroy(gameObject);
            return true;
        }
    }
}