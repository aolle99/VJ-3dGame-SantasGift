using UnityEngine;

namespace Enemies.Snowball
{
    public class SnowballMovement : MonoBehaviour
    {
        public float rotationSpeed, movementSpeed;
        private float angle = 0f;
        private bool left = false;
        Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ManageMovement();
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("House"))
            {
                print("House Collision");
                left = !left;
            }
        }

        private void ManageMovement()
        {
            Vector3 position = transform.position;
            Vector3 direction = position - transform.parent.position;
            angle = movementSpeed * Time.deltaTime;
            if (left) angle *= -1;
            
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 target = transform.parent.position + rotation * direction;
            
            float rotationY = Mathf.Atan2(target.z, target.x) * Mathf.Rad2Deg;
            float rotationX = Mathf.Atan2(target.z, target.x) * Mathf.Rad2Deg * rotationSpeed;
            print("rotationSpeed: " + rotationSpeed);

            // Aplicar la rotación en ambos ejes
            rotation = Quaternion.Euler(rotationX, -rotationY, 0f);
            
            rb.Move(target, rotation);
            
        }
    }
}
