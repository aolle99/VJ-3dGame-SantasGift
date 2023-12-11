using System;
using UnityEngine;

namespace Player.GiftBullet
{
    public class GiftController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private float speed = 5.0f;
        
        [SerializeField] private float bulletDuration = 1f;
        
        private float _angle;

        private float _lifeTimeTimer;

        private Rigidbody _rb;

        public int Direction { private get; set; }

        void Start()
        {
            var position = transform.position;
            _angle = Mathf.Atan2(position.z, position.x);

            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // move bullet in a circle
            _angle += speed * Time.deltaTime * Direction;

            var radius = MapManager.instance.Radius;

            var x = Mathf.Cos(_angle) * radius;
            var z = Mathf.Sin(_angle) * radius;

            var newPosition = new Vector3(x, transform.position.y, z);
            var rotation = new Quaternion();
            
            _rb.Move(newPosition,rotation);

            _lifeTimeTimer += Time.deltaTime;

            if (_lifeTimeTimer > bulletDuration)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}