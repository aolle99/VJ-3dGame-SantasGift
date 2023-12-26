using System.Collections.Generic;
using System.Linq;
using Environment;
using UnityEditor;
using UnityEngine;

namespace Camera
{
    public class ObjectFaderTrigger : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        
        [SerializeField] private float detectionDistance;
        

        private void Update()
        {
            RaycastHit[] hits;
            
            Vector3 playerPosition = playerTransform.position;
            Vector3 camPosition = transform.position;
            Vector3 direction = playerPosition - camPosition;
            
            hits = Physics.RaycastAll(camPosition, (playerPosition - camPosition).normalized, detectionDistance);

            foreach (RaycastHit hit in hits)
            {
                ObjectFader objectFader = hit.collider.GetComponent<ObjectFader>();
                if (objectFader == null) continue;
                objectFader.SetFade();
            }
            
            
            
        }
    }
}