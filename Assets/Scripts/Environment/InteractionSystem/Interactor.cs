using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Environment.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField]
        private Transform interactionPoint;
        
        [SerializeField]
        private float interactionRadius = 1f;
        
        [SerializeField]
        private LayerMask interactableMask;
        
        private readonly Collider[] _colliders = new Collider[3];

        private int numFound;
        
        
        private IInteractable _interactable = null;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (_interactable != null)
                {
                    if (_interactable.Interact(this))
                    {
                        _interactable = null;
                    }
                }
            }
        }

        private void Update()
        {
            numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, _colliders, interactableMask);

            if (numFound > 0)
            {
                _interactable = _colliders[0].GetComponent<IInteractable>();
                
                if (_interactable != null)
                {
                    var interactionPromptUI = _interactable.InteractionPromptUI;
                    if(!interactionPromptUI.IsDisplayed)
                        interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }
            }
            else
            {
                if (_interactable != null)
                {
                    var interactionPromptUI = _interactable.InteractionPromptUI;
                    if (interactionPromptUI.IsDisplayed)
                    {
                        interactionPromptUI.Close();
                    }
                }
            }
        }
    }
}