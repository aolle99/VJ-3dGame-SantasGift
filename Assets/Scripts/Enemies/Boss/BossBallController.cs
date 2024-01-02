using UnityEngine;


namespace Enemies.Boss
{
    public class BossBallController : MonoBehaviour
    {
        public int direction = 1;
        public float speed = 0.2f;
        float _angle = 0f;
        public float ballDuration = 1f;
        float _lifeTime = 0f;
        
        void Start()
        {
            _angle = Mathf.Atan2(transform.position.z, transform.position.x);
        }

        void FixedUpdate()
        {
            // move bullet in a circle
            _angle += speed * Time.deltaTime * direction;

            float radius = 70f;

            float x = Mathf.Cos(_angle) * radius;
            float z = Mathf.Sin(_angle) * radius;

            transform.position = new Vector3(x, transform.position.y, z);

            _lifeTime += Time.deltaTime;

            if (_lifeTime > ballDuration)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Santa"))
            {
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
