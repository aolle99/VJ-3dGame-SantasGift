using System;
using System.Collections.Generic;
using System.Linq;
using Environment;
using UnityEngine;

namespace Camera
{
    public class ObjectFaderTrigger : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        [SerializeField] private float detectionDistance;


        private void Update()
        {
            if (MapManager.instance.GetCurrentFaseRadius() < 70)
            {
                RaycastHit[] hits;

                Vector3 playerPosition = playerTransform.position;
                Vector3 camPosition = transform.position;
                Vector3 direction = (playerPosition - camPosition).normalized;

                hits = Physics.RaycastAll(camPosition, direction, detectionDistance);

                foreach (RaycastHit hit in hits)
                {
                    ObjectFader objectFader = hit.collider.GetComponent<ObjectFader>();
                    if (objectFader == null) continue;
                    objectFader.SetFade();
                }
            }
        }
    }
}