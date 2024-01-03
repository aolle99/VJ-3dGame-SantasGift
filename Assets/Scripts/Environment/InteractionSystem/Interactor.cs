using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField]
        private Transform _interactionPoint;
        
        [SerializeField]
        private float _interactionRadius = 1f;
        
        [SerializeField]
        private LayerMask interactableMask;
        
        private readonly Collider[] _colliders = new Collider[3];

        [SerializeField] private int numFound;
        
        
        private IInteractable _interactable = null;

        private void Update()
        {
            numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionRadius, _colliders, interactableMask);

            if (numFound > 0)
            {
                _interactable = _colliders[0].GetComponent<IInteractable>();
                
                if (_interactable != null)
                {
                    var interactionPromptUI = _interactable.InteractionPromptUI;
                    if(!interactionPromptUI.IsDisplayed)
                        interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (_interactable.Interact(this))
                        {
                            _interactable = null;
                        }
                    }
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionRadius);
        }
    }
}