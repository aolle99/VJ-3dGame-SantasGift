using System;
using UnityEngine;

namespace Player.GiftBullet
{
    enum LifeTimeType
    {
        Time,
        Distance
    }
    public class GiftController : MonoBehaviour
    {
        // Start is called before the first frame update
        
        [SerializeField]private float speed = 5.0f;

        [SerializeField] private LifeTimeType lifeTimeType = LifeTimeType.Time;
        [SerializeField] private float bulletDuration = 1f;
        [SerializeField] private int bulletAngleDistance = 180;
        [SerializeField] private float damage = 1f;
        
        private float lifeTime = 0f;
        private float _startAngle;
        private float direction = 1f;
        private float angle = 0f;
        private float acumulatedAngle = 0f;
        private Vector2 startPosition;
        private Vector2 centerPosition;
        private float startAngle;
        
        Rigidbody rb;
        void Start()
        {
            var position = transform.position;
            angle = Mathf.Atan2(position.z, position.x);
            rb = GetComponent<Rigidbody>();
            _startAngle = angle * Mathf.Rad2Deg;
            
            startPosition = new Vector2(position.x, position.z);
            
            startAngle = calculateAngle();
            
            
        }

        private void Update()
        {
            if (lifeTimeType == LifeTimeType.Distance)
            {
                float newAngle = calculateAngle();
                acumulatedAngle += Mathf.Abs(newAngle - startAngle) * Mathf.Rad2Deg;
                startAngle = newAngle;
                
                // Verificar si el ángulo supera el umbral
                if (acumulatedAngle  > bulletAngleDistance)
                {
                    Destroy(gameObject);
                }
            }

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

            if (lifeTimeType == LifeTimeType.Time)
            {
                lifeTime += Time.deltaTime;

                if (lifeTime > bulletDuration)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Call the enemy hit method
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Call the enemy hit method
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
        
        public void SetDirection(float d)
        {
            direction = d;
        }

        private float calculateAngle()
        {
            // Calcular el ángulo acumulado
            var currentPosition = new Vector2(transform.position.x, transform.position.z);
            float ladoC = Vector2.Distance(startPosition, currentPosition);
            float ladoA = Vector2.Distance(startPosition, centerPosition);
            float ladoB = Vector2.Distance(currentPosition, centerPosition);
                
            return Mathf.Acos((Mathf.Pow(ladoA, 2) + Mathf.Pow(ladoB, 2) - Mathf.Pow(ladoC, 2)) / (2 * ladoA * ladoB));
        }
    }
}