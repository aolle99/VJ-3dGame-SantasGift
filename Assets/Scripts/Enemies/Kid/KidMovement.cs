using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Kid
{
    public class KidMovement : MonoBehaviour
    {
        [FormerlySerializedAs("rotationSpeed")] public float movementSpeed;
        public float gravityScale, jumpForce;
        private float _angle = 0f;
        private Animator _anim;
        private Boolean _jumping = false;
        private Boolean _canJump = true;
        private CharacterController _charControl;
        private Vector3 _startDirection;
        private float _speedY;
        
        private static readonly int Jump = Animator.StringToHash("jump");
        
        public bool ViewDirection { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            _charControl = GetComponent<CharacterController>();
            var playerTransform = transform;
            var parent = playerTransform.parent;
            _startDirection = playerTransform.position - parent.position;
            ViewDirection = false;
            _speedY = 0;
            
            _anim = GetComponentInChildren<Animator>();
        }
    
        // Update is called once per frame
        void Update()
        {
            //if(_anim.GetBool(Jump)) print(_anim.GetBool(Jump));
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Vector3 sideDirection = Vector3.forward;
            Vector3 directionToOther = other.transform.position - transform.position;
            directionToOther.Normalize();
            if (other.gameObject.CompareTag("Obstacle"))
            {
                print("house");
                _jumping = true;
            }
        }
        
        void FixedUpdate()
        {
            ManageMovement();
            ManageOrientation();
            ManageJump(); 
        }
        
        void ManageMovement()
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

            if (!ViewDirection) transform.Rotate(Vector3.up, 180.0f);
        }
        
        void ManageJump()
        {
            var position = transform.position;
            if (_charControl.Move(_speedY * Time.deltaTime * Vector3.up) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
            
            if (_charControl.isGrounded)
            {
                _anim.SetBool(Jump, false);
                _canJump = true;
                if (_speedY < 0.0f) _speedY = 0.0f;
                if(_canJump && _jumping)
                {
                    _speedY = jumpForce;
                    _canJump = false;
                    _jumping = false;
                    _anim.SetBool(Jump, true);
                }
            }
            else _speedY -= gravityScale * Time.deltaTime;
        }
    }
}
