using UnityEngine;

namespace Enemies.Snowball
{
    public class SnowballMovement : MonoBehaviour
    {
        public float rotationSpeed, movementSpeed;
        private float _angle = 0f;
        private bool _left = false;
        private Rigidbody _rb;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
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
                _left = !_left;
            }
        }

        private void ManageMovement()
        {
            Vector3 position = transform.position;
            Vector3 direction = position - transform.parent.position;
            _angle = movementSpeed * Time.deltaTime;
            if (_left) _angle *= -1;
            
            Quaternion rotation = Quaternion.AngleAxis(_angle, Vector3.up);
            Vector3 target = transform.parent.position + rotation * direction;
            
            float rotationY = Mathf.Atan2(target.z, target.x) * Mathf.Rad2Deg;
            float rotationX = Mathf.Atan2(target.z, target.x) * Mathf.Rad2Deg * rotationSpeed;

            // Aplicar la rotación en ambos ejes
            rotation = Quaternion.Euler(rotationX, -rotationY, 0f);
            
            _rb.Move(target, rotation);
            
        }
    }
}
