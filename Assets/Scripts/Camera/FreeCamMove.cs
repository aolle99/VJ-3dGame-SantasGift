using UnityEngine;

namespace Camera
{
    public class FreeCamMove : MonoBehaviour
    {
        public Transform target;
        public float rotationSpeed = 5f;
        public float translationSpeed = 2f;
        
        void Update()
        {
            // Rotación Horizontal
            float horizontalInput = Input.GetAxis("Horizontal")*-1;
            transform.RotateAround(target.position, Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);


            // Movimiento vertical
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * (verticalInput * translationSpeed * Time.deltaTime));
        }
    }
}