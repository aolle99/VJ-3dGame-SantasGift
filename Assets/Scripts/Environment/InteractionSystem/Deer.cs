using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.InteractionSystem
{
    public class Deer : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt;
        
        public string InteractionPrompt => prompt;
        
        [SerializeField] private InteractionPromptUI _interactionPromptUI;
        private float timer;
        
        private bool _isInteracted;
        
        private MapManager _mapManager;
        
        private void Start()
        {
            timer = 0;
            
        }
        
        private void Update()
        {
            if (_isInteracted)
            {
                timer += Time.deltaTime;
                if (timer > 3)
                {
                    _isInteracted = false;
                    timer = 0;
                }
            }
        }
        
        public InteractionPromptUI InteractionPromptUI
        {
            get => _interactionPromptUI;
            set => _interactionPromptUI = value;
        }

        public bool Interact(Interactor interactor)
        {
            _mapManager = MapManager.instance;
            _mapManager.NextPhase();
            Destroy(_interactionPromptUI);
            return true;
        }
    }
}