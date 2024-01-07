using System;
using Player;
using UnityEngine;

namespace Enemies.Snowman
{
    public class SnowmanOrientation : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        public void Start()
        {
            Orientate();
        }

        public void Update()
        {
            Orientate();
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

                // Invierte la dirección si es necesario
                if (CheckPositionPlayer())
                {
                    transform.Rotate(Vector3.up, 180.0f);
                }
            }
        }

        private Boolean CheckPositionPlayer()
        {
            Vector3 positionA = transform.position;
            Vector3 positionB = playerController.transform.position;
            var radius = Mathf.Sqrt(positionA.x * positionA.x + positionA.z * positionA.z);
            
            // Calculate the angle between the objects
            Vector3 directionToPlayer = positionA - positionB;
            //print("angle: " + angle);
            float angle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);
            angle = (angle + 360f) % 360f;

            // Calculate the distances in both directions
            float distanceClockwise = CalculateDistance(angle, radius) % 80;
            float distanceAntiClockwise = CalculateDistance(360f - angle, radius) % 80;
            return distanceAntiClockwise > distanceClockwise;
        }
        
        private float CalculateDistance(float angle, float radius)
        {
            // Convert angle to radians
            float radians = angle * Mathf.Deg2Rad;

            // Calculate distance along the circumference
            float distance = radians * radius;

            return distance;
        }

        public int GetDirection()
        {
            if(CheckPositionPlayer()) return -1;
            return 1;
        }
    }
}