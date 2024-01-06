using System;
using Player;
using UnityEngine;

namespace Enemies.Snowball
{
    public class SnowballMovement : MonoBehaviour
    {
        public float rotationSpeed, movementSpeed;
        private float _angle = 0f;
        private bool _left = false;
        private Rigidbody _rb;
        private float initialSnowballRadius;
        [SerializeField] private PlayerController playerController;
        [SerializeField]private float damageCaused = 5f;

        // Start is called before the first frame update
        void Start()
        {
            var position = transform.position;
            initialSnowballRadius = Mathf.Sqrt(position.z * position.z + position.x * position.x);
            print(initialSnowballRadius);
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ManageMovement();
            ValidateRadius();
        }

        void ValidateRadius()
        {
            var snowballPos = transform.position;
            var snowballRadius = Mathf.Sqrt(snowballPos.z * snowballPos.z + snowballPos.x * snowballPos.x);

            if (Math.Abs(initialSnowballRadius - snowballRadius) > 0.05)
            {
                var newX = snowballPos.x * initialSnowballRadius / snowballRadius;
                var newZ = snowballPos.z * initialSnowballRadius / snowballRadius;
                
                var targetPos = new Vector3(newX, snowballPos.y, newZ);
                transform.position = targetPos;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Obstacle"))
            {
                _left = !_left;
            }
            if(collision.gameObject.CompareTag("Santa"))
            {
                _left = !_left;
                playerController.damagePlayer(damageCaused);
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
