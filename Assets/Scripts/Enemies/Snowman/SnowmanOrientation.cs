using System;
using Player;
using UnityEngine;

namespace Enemies.Snowman
{
    public class SnowmanOrientation : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        private Vector3 _initialPosition;
        
        public void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            _initialPosition = transform.position;
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
            Vector3 positionA = _initialPosition;
            print("positionA: " + positionA);
            Vector3 positionB = playerController.transform.position;
            print("positionB: " + positionB);
            var radius = Mathf.Sqrt(positionA.x * positionA.x + positionA.z * positionA.z);
            
            float angleClockwise = Mathf.Atan2(positionB.z - positionA.z, positionB.x - positionA.x) * Mathf.Rad2Deg;
            if (angleClockwise < 0)
                angleClockwise += 360f;

            float angleAntiClockwise = 360f - angleClockwise;

            // Calculate arc lengths
            float arcLengthClockwise = (angleClockwise / 360f) * 2 * Mathf.PI * radius;
            float arcLengthAntiClockwise = (angleAntiClockwise / 360f) * 2 * Mathf.PI * radius;

            print("Distance Clockwise: " + arcLengthClockwise);
            print("Distance AntiClockwise: " + arcLengthAntiClockwise);
            return true;
            /*
            // Calculate the angle between the objects
            Vector3 directionToPlayer = positionA - positionB;
            //print("angle: " + angle);
            float angle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);
            angle = (angle + 360f) % 360f;

            // Calculate the distances in both directions
            float distanceClockwise = CalculateDistance(angle, radius);
            float distanceAntiClockwise = CalculateDistance(360f - angle, radius);
            print("distanceClockwise: " + distanceClockwise + ", distanceAntiClockwise: " + distanceAntiClockwise);
            return distanceAntiClockwise > distanceClockwise;*/
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