using System;
using UnityEngine;

namespace Player
{
    public class PlayerOrientation : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        public void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
            Orientate();
        }

        public void Update()
        {
            Orientate();
            
        }

        public void Orientate()
        {
            Vector3 directionToParent = transform.parent.position - transform.position;

            Vector3 horizontalDirection = new Vector3(directionToParent.x, 0.0f, directionToParent.z);

            if (horizontalDirection.sqrMagnitude > 0.001f)
            {
                transform.LookAt(transform.position + horizontalDirection);
        
                transform.Rotate(Vector3.up, 90.0f);

                if (!playerMovement.ViewDirection)
                    transform.Rotate(Vector3.up, 180.0f);
            }
        }
    }
}