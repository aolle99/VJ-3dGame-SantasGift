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
        
        [SerializeField]private float speed = 5.0f;

        [SerializeField] private LifeTimeType lifeTimeType = LifeTimeType.Time;
        [SerializeField] private float bulletDuration = 1f;
        [SerializeField] private int bulletAngleDistance = 180;
        [SerializeField] private float damage = 1f;
        [SerializeField] private GameObject hitEffect;
        
        private float lifeTime = 0f;
        private float direction = 1f;
        private float angle = 0f;
        private float acumulatedAngle = 0f;
        private Vector2 startPosition;
        private Vector2 centerPosition;
        private float startAngle;
        private float radius;
        
        Rigidbody rb;
        void Start()
        {
            var position = transform.position;
            angle = Mathf.Atan2(position.z, position.x);
            rb = GetComponent<Rigidbody>();
            
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
                
                if (acumulatedAngle  > bulletAngleDistance)
                {
                    callParticleExplosion();
                    Destroy(gameObject);
                }
            }

        }

        void FixedUpdate()
        {
            angle += speed * Time.deltaTime * direction;
            radius = MapManager.instance.GetCurrentFaseRadius();
            
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            rb.position = new Vector3(x, transform.position.y, z);
            
            rb.angularVelocity = new Vector3(0, 5f, 0);

            if (lifeTimeType == LifeTimeType.Time)
            {
                lifeTime += Time.deltaTime;

                if (lifeTime > bulletDuration)
                {
                    callParticleExplosion();
                    Destroy(gameObject);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
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
                callParticleExplosion();
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Obstacle"))
            {
                callParticleExplosion();
                Destroy(gameObject);
            }
        }
        
        public void SetDirection(float d)
        {
            direction = d;
        }

        private float calculateAngle()
        {
            var currentPosition = new Vector2(transform.position.x, transform.position.z);
            float ladoC = Vector2.Distance(startPosition, currentPosition);
            float ladoA = Vector2.Distance(startPosition, centerPosition);
            float ladoB = Vector2.Distance(currentPosition, centerPosition);
                
            return Mathf.Acos((Mathf.Pow(ladoA, 2) + Mathf.Pow(ladoB, 2) - Mathf.Pow(ladoC, 2)) / (2 * ladoA * ladoB));
        }

        private void callParticleExplosion()
        {
            var particle = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(particle, 1f);
        }
    }
}