using System;
using Player;
using UnityEngine;

namespace Enemies.Snowman
{
    public class SnowmanOrientation : MonoBehaviour
    {
        private Vector2 centerPosition;
        public Transform centerPoint; // Center point for the circular raycast
        public LayerMask layerMask; // Layers to include in the raycast
        public int numberOfRays = 36; // Number of rays to cast
        public float maxRayLength = 20f;
        private Boolean _viewDirection = false;
        private float _timeChangeDirection = 0f;
        
        public void Start()
        {
            Orientate();
        }

        public void Update()
        {
            Orientate();
            _timeChangeDirection += Time.deltaTime;
        }

        public void Orientate()
        {
            // Obtén la dirección desde la posición actual hasta la posición del padre
            Vector3 directionToParent = transform.parent.position - transform.position;

            // Proyecta la dirección sobre el plano horizontal (X-Z)
            Vector3 horizontalDirection = new Vector3(directionToParent.x, 0.0f, directionToParent.z);

            // Asegúrate de que la dirección no sea cero para evitar problemas con LookAt
            if (horizontalDirection.sqrMagnitude > 0.001f)
            {
                // Utiliza LookAt solo en las coordenadas X y Z
                transform.LookAt(transform.position + horizontalDirection);
        
                // Gira el objeto 90 grados alrededor del eje Y
                transform.Rotate(Vector3.up, 90.0f);

            }
            // Invierte la dirección si es necesario
            if (_timeChangeDirection > 5f)
            {
                _timeChangeDirection = 0f;
                _viewDirection = !_viewDirection;
                
            }

            if (!_viewDirection)
            {
                transform.Rotate(Vector3.up, 180.0f);
            }
        }
        
        public int GetDirection()
        {
            if(_viewDirection) return 1;
            return -1;
        }
    }
}