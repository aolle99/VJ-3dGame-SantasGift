using System;
using Player;
using UnityEngine;

namespace Enemies.Snowman
{
    public class SnowmanOrientation : MonoBehaviour
    {
        private Vector2 centerPosition;
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
            Vector3 directionToParent = transform.parent.position - transform.position;

            Vector3 horizontalDirection = new Vector3(directionToParent.x, 0.0f, directionToParent.z);

            if (horizontalDirection.sqrMagnitude > 0.001f)
            {
                transform.LookAt(transform.position + horizontalDirection);
        
                transform.Rotate(Vector3.up, 90.0f);

            }
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