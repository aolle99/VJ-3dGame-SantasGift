using System.Collections.Generic;
using System.Linq;
using Environment;
using UnityEditor;
using UnityEngine;

namespace Camera
{
    public class ObjectFaderTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        
        private List<ObjectFader> _objectsFader = new List<ObjectFader>();

        private void Update()
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 objectPosition = transform.position;
            Vector3 direction = playerPosition - objectPosition;
            
            //Use raycast to check if the player is behind the object and fade object
            if (Physics.Raycast(objectPosition, direction, out RaycastHit hit))
            {
                if (hit.collider == null) return;
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    foreach (var objectFader in _objectsFader)
                    {
                        objectFader.setFade(false);
                        //remove object from array
                        _objectsFader.Remove(objectFader);
                        
                    }
                }
                else
                {
                    var objectFader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (objectFader != null)
                    {
                        _objectsFader.Add(objectFader);
                        objectFader.setFade(true);
                    }
                        
                }
            }
            
            
        }
    }
}