using UnityEngine;

namespace Environment.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField]
        private Transform _interactionPoint;
        
        [SerializeField]
        private float _interactionRadius = 1f;
        
        [SerializeField]
        private LayerMask _interactableMask;
        
        private readonly Collider[] _colliders = new Collider[3];
    }
}