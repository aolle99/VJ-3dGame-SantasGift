using System;
using UnityEngine;

namespace Player.GiftBullet
{
    public class GiftController : MonoBehaviour
    {
        // Start is called before the first frame update
        public int direction = 1;
        public float speed = 5.0f;
        float angle = 0f;
        public float bulletDuration = 1f;
        float lifeTime = 0f;
        
        Rigidbody rb;
        void Start()
        {
            var position = transform.position;
            angle = Mathf.Atan2(position.z, position.x);
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // move bullet in a circle
            angle += speed * Time.deltaTime * direction;

            float radius = MapManager.instance.Radius;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            rb.position = new Vector3(x, transform.position.y, z) ;

            lifeTime += Time.deltaTime;

            if (lifeTime > bulletDuration)
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