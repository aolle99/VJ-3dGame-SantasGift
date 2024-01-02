using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Boss
{
    public class BossMovement : MonoBehaviour
    {
        [FormerlySerializedAs("rotationSpeed")] public float movementSpeed;
        private CharacterController _charControl;
        private Vector3 _startDirection;
        private Animator _anim;
        private float _angle = 0f;
        private float _timer = 0f;
        private bool _laughed = false;
        private bool _throwed = false;
        private bool _ballThrownAnim = false;
        private float _timeBetweenAnim = 5.0f;
        public GameObject ballPrefab;
        private static readonly int Laugh = Animator.StringToHash("laugh");
        private static readonly int Throw = Animator.StringToHash("throw_ball");

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
        
        void FixedUpdate()
        {
            ManageMovement();
            ManageOrientation();
            if (Math.Abs(_timer - _timeBetweenAnim) < 0.05) ManageAnimations();
        }
        
        void ManageMovement()
        {
            if (!_laughed && !_throwed)
            {
                var position = transform.position;
                _angle = movementSpeed * Time.deltaTime;
                var direction = position - transform.parent.position;
                direction = Quaternion.AngleAxis(_angle, Vector3.up) * direction;
                if (_charControl.Move(direction - position) == CollisionFlags.Sides)
                {
                    transform.position = position;
                    Physics.SyncTransforms();
                }
            }
            
        }
        
        private void ManageOrientation()
        {
            var playerTransform = transform;
            var currentDirection = playerTransform.position - playerTransform.parent.position;
            currentDirection.y = 0.0f;
            currentDirection.Normalize();
             
            Quaternion orientation;
            if ((_startDirection - currentDirection).magnitude < 1e-3)
                orientation = Quaternion.AngleAxis(0.0f, Vector3.up);
            else if ((_startDirection + currentDirection).magnitude < 1e-3)
                orientation = Quaternion.AngleAxis(180.0f, Vector3.up);
            else
                orientation = Quaternion.FromToRotation(_startDirection, currentDirection);

            orientation.eulerAngles = new Vector3(0.0f, orientation.eulerAngles.y, 0.0f);
            transform.rotation = orientation;

            //if (!ViewDirection) transform.Rotate(Vector3.up, 180.0f);
        }
        
        
        private void ManageAnimations()
        {
            if (!_laughed && !_throwed)
            {
                _anim.SetBool(Laugh, true);
                _laughed = true;
            }
            else if(_laughed && !_throwed)
            {
                _anim.SetBool(Laugh, false);
                _anim.SetBool(Throw, true);
                _laughed = false;
                _throwed = true;
                _timeBetweenAnim = 0.8f;
            } 
            else if (_throwed && !_ballThrownAnim && !_laughed)
            {
                Vector3 position = transform.position;
                Vector3 ballPosition = new Vector3(position.x + 3, position.y + 4, position.z);
                GameObject bosBall = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
                
                bosBall.GetComponent<BossBallController>().direction = -1;
                _timeBetweenAnim = 2.0f;
                _ballThrownAnim = true;
            }
            else if(_throwed && !_laughed && _ballThrownAnim)
            {
                _anim.SetBool(Throw, false);
                _throwed = false;
                _ballThrownAnim = false;
                _timeBetweenAnim = 5.0f;
            }
            _timer = 0f;
        }
    }
}