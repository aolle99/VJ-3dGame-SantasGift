using UnityEngine;

namespace Camera
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        [SerializeField] private float distanceFromPlayer = 10.0f;

        [SerializeField] private float heightOffset = 5.0f;

        private Vector3 _startDirection;
        private Transform _playerTransform;
        private Transform _parentTransform;

        private void Start()
        {
            _playerTransform = player.transform;
        }

        private void LateUpdate()
        {
            Vector3 playerPosition = _playerTransform.position;
            Vector3 parentPosition = _playerTransform.parent.position;
            Vector3 directionToPlayer = playerPosition - parentPosition;

            float angle = Mathf.Atan2(directionToPlayer.z, directionToPlayer.x);

            angle = Mathf.Rad2Deg * angle;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromPlayer;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromPlayer;


            Vector3 targetPosition =
                new Vector3(playerPosition.x + x, playerPosition.y + heightOffset, playerPosition.z + z);


            transform.position = targetPosition;

            var targetRotation = new Vector3(parentPosition.x, playerPosition.y + heightOffset, parentPosition.z);

            transform.LookAt(targetRotation);
        }
    }
}