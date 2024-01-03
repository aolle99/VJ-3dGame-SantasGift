using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.InteractionSystem
{
    public class Bridge : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt;
        
        private MapManager _mapManager;
        
        public string InteractionPrompt => prompt;
        
        [SerializeField] private InteractionPromptUI _interactionPromptUI;
        
        public InteractionPromptUI InteractionPromptUI
        {
            get => _interactionPromptUI;
            set => _interactionPromptUI = value;
        }

        public bool Interact(Interactor interactor)
        {
            _mapManager = MapManager.instance;
            _mapManager.ChangeMapZone();
            Destroy(_interactionPromptUI);
            return true;
        }
    }
}