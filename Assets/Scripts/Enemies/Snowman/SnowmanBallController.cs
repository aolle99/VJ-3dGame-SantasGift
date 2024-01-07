using Player;
using UnityEngine;

namespace Enemies.Snowman
{
    enum LifeTimeType
    {
        Time,
        Distance
    }

    public class SnowmanBallController : MonoBehaviour
    {
        [SerializeField] private float speed = 20.0f;
        [SerializeField] private LifeTimeType lifeTimeType = LifeTimeType.Time;
        [SerializeField] private float bulletDuration = 1f;
        [SerializeField] private int bulletAngleDistance = 180;
        [SerializeField] private float damageCaused = 3f;
        [SerializeField] private PlayerController playerController;

        private float lifeTime = 0f;
        public float direction = 1f;
        private float _angle = 0f;
        private float acumulatedAngle = 0f;
        private Vector2 startPosition;
        private Vector2 centerPosition;
        private float startAngle;
        private float radius;

        Rigidbody rb;

        void Start()
        {
            var position = transform.position;
            _angle = Mathf.Atan2(position.z, position.x);

            rb = GetComponent<Rigidbody>();

            startPosition = new Vector2(position.x, position.z);

            startAngle = calculateAngle();

            playerController = GameObject.FindWithTag("Santa").GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (lifeTimeType == LifeTimeType.Distance)
            {
                float newAngle = calculateAngle();
                acumulatedAngle += Mathf.Abs(newAngle - startAngle) * Mathf.Rad2Deg;
                startAngle = newAngle;

                if (acumulatedAngle > bulletAngleDistance)
                {
                    Destroy(gameObject);
                }
            }
        }

        void FixedUpdate()
        {
            _angle += speed * Time.deltaTime * direction;
            radius = Mathf.Sqrt(transform.position.x * transform.position.x +
                                transform.position.z * transform.position.z);

            float x = Mathf.Cos(_angle) * radius;
            float z = Mathf.Sin(_angle) * radius;

            rb.position = new Vector3(x, transform.position.y, z);

            rb.angularVelocity = new Vector3(0, 5f, 0);

            if (lifeTimeType == LifeTimeType.Time)
            {
                lifeTime += Time.deltaTime;

                if (lifeTime > bulletDuration)
                {
                    Destroy(gameObject);
                }
            }
        }

        private float calculateAngle()
        {
            var currentPosition = new Vector2(transform.position.x, transform.position.z);
            float ladoC = Vector2.Distance(startPosition, currentPosition);
            float ladoA = Vector2.Distance(startPosition, centerPosition);
            float ladoB = Vector2.Distance(currentPosition, centerPosition);

            return Mathf.Acos((Mathf.Pow(ladoA, 2) + Mathf.Pow(ladoB, 2) - Mathf.Pow(ladoC, 2)) / (2 * ladoA * ladoB));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Santa"))
            {
                playerController.damagePlayer(damageCaused);
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}