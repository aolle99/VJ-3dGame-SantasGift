using System;
using Enemies.Snowman;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Boss
{
    public class BossMovement : MonoBehaviour
    {
        public float movementSpeed;
        private CharacterController _charControl;
        private Vector3 _startDirection;
        private Animator _anim;
        private float _angle = 0f;
        private float _timer = 0f;
        private bool _laughed = false;
        private bool _throwed = false;
        private bool _left = false;
        private bool _objectDetected = false;
        private int _animNum = -1;
        private float _timeBetweenAnim = 5.0f;
        private float _timeBetweenActions = 0.0f;
        public GameObject ballPrefab;
        private static readonly int Laugh = Animator.StringToHash("laugh");
        private static readonly int Throw = Animator.StringToHash("throw_ball");
        
        [SerializeField] private PlayerController playerController;
        [SerializeField]private float damageCaused = 10f;
        [SerializeField]private BossOrientation bossOrientation;
        
        public Transform centerPoint; // Center point for the circular raycast
        public LayerMask layerMask; // Layers to include in the raycast
        public int numberOfRays = 36; // Number of rays to cast
        public float maxRayLength = 5f;
        public bool ViewDirection { get; private set; }
        
        void Start()
        {
            _charControl = GetComponent<CharacterController>();
            var playerTransform = transform;
            var parent = playerTransform.parent;
            _startDirection = playerTransform.position - parent.position;
            ViewDirection = false;
            
            _anim = GetComponentInChildren<Animator>();
        }
        
        void Update()
        {
            _timer += Time.deltaTime;
        }
        
        void DetectObjecteNearBy()
        {
            var bossPosition = transform.position;
            float radius = Mathf.Sqrt(bossPosition.x * bossPosition.x + bossPosition.z * bossPosition.z);
            
            for (int i = 0; i < numberOfRays; i++)
            {
                float angle = (i * 90f / numberOfRays) - (90f / 2f); // Calculate angle in degrees
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.forward; // Create direction vector

                // Calculate ray origin and direction
                Vector3 rayOrigin = centerPoint.position;
                Vector3 rayDirection = direction * radius;

                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxRayLength, layerMask) && _timeBetweenActions == 0.0f)
                {
                    if (hit.collider.gameObject.CompareTag("Obstacle"))
                    {
                        _left = !_left;
                        ViewDirection = !ViewDirection;
                        _objectDetected = true;
                        _timeBetweenActions += Time.deltaTime;
                    }
                    
                    if(hit.collider.gameObject.CompareTag("Santa"))
                    {
                        playerController.damagePlayer(damageCaused);
                        _objectDetected = true;
                        _timeBetweenActions += Time.deltaTime;
                    }
                }
            }
            
            if (_objectDetected) _timeBetweenActions += Time.deltaTime;
            if (Math.Abs(_timeBetweenActions - 1.0f) < 0.05)
            {
                _timeBetweenActions = 0.0f;
                _objectDetected = false;
            }
        }

        
        void FixedUpdate()
        {
            ManageMovement();
            DetectObjecteNearBy();
            if (Math.Abs(_timer - _timeBetweenAnim) < 0.05) ManageAnimations();
        }
        
        void ManageMovement()
        {
            if (!_laughed && !_throwed)
            {
                var position = transform.position;
                _angle = movementSpeed * Time.deltaTime;
                if (_left) _angle *= -1;
                
                var direction = position - transform.parent.position;
                direction = Quaternion.AngleAxis(_angle, Vector3.up) * direction;
                if (_charControl.Move(direction - position) == CollisionFlags.Sides)
                {
                    transform.position = position;
                    Physics.SyncTransforms();
                }
            }
            
        }
        
        private void ManageAnimations()
        {
            _animNum++;
            if (_animNum == 0)
            {
                _anim.SetBool(Laugh, true);
                AudioManager.instance.PlaySound("EvilLaugh");
                _laughed = true;
            } 
            else if (_animNum == 1)
            {
                _anim.SetBool(Throw, true);
                _anim.SetBool(Laugh, false);
                _throwed = true;
                _laughed = false;
                _timeBetweenAnim = 0.8f;
            }
            else if(_animNum == 2)
            {
                Vector3 position = transform.position;
                Vector3 ballPosition = new Vector3(position.x + 3, position.y + 4, position.z);
                GameObject bosBall = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
                
                bosBall.GetComponent<SnowmanBallController>().direction = bossOrientation.GetDirection();;
                _timeBetweenAnim = 1.0f;
            }
            else if(_animNum == 3){
                _throwed = false;
                _anim.SetBool(Throw, false);
                _timeBetweenAnim = 5.0f;
                _animNum = -1;
            }
            
            _timer = 0f;
        }
    }
}