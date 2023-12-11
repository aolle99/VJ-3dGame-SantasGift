using UnityEngine;

namespace Camera
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        [SerializeField]
        private float smoothness = 3.0f; // Ajusta este valor para controlar la suavidad del seguimiento

        private Vector3 _startDirection;
        private Transform _playerTransform;

        // Start is called before the first frame update
        private void Start()
        {

            // Store starting direction of the player with respect to the axis of the level
            _startDirection = player.transform.position - player.transform.parent.position;
            _startDirection.y = 0.0f;
            _startDirection.Normalize();

            _playerTransform = player.transform;
        }

        // LateUpdate is called once per frame, after Update
        private void LateUpdate()
        {
            // Smoothly interpolate camera position
            var position = transform.position;
            var playerPosition = _playerTransform.position;
            var targetPosition = new Vector3(position.x, playerPosition.y + 5, position.z);
            transform.position = Vector3.Lerp(position, targetPosition, smoothness * Time.deltaTime);

            // Compute current direction
            var currentDirection = playerPosition - player.transform.parent.position;
            currentDirection.y = 0.0f;
            currentDirection.Normalize();

            // Smoothly interpolate camera rotation
            Quaternion targetRotation;
            if (Mathf.Approximately(Vector3.Distance(_startDirection, currentDirection), 0.0f))
            {
                targetRotation = Quaternion.identity;
            }
            else
            {
                targetRotation = Quaternion.FromToRotation(_startDirection, currentDirection);
            }

            var parent = transform.parent;
            parent.rotation = Quaternion.Slerp(parent.rotation, targetRotation, smoothness * Time.deltaTime);
        }
    }
}