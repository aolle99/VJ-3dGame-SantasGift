using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Kid
{
    public class KidMovement : MonoBehaviour
    {
        public float movementSpeed;
        public float gravityScale, jumpForce;
        private float _angle = 0f;
        private Animator _anim;
        private Boolean _jumping = false;
        private Boolean _canJump = true;
        private Boolean _objDetected = false;
        private CharacterController _charControl;
        private Vector3 _startDirection;
        private float _speedY;
        public Transform centerPoint; // Center point for the circular raycast
        public LayerMask layerMask; // Layers to include in the raycast
        public int numberOfRays = 36; // Number of rays to cast
        public float maxRayLength = 5f;
        
        private static readonly int Jump = Animator.StringToHash("jump");
        private static readonly int Steal = Animator.StringToHash("steal");
        
        public bool ViewDirection { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            _charControl = GetComponent<CharacterController>();
            var kidTransform = transform;
            var parent = kidTransform.parent;
            _startDirection = kidTransform.position - parent.position;
            ViewDirection = false;
            _speedY = 0;
            
            _anim = GetComponentInChildren<Animator>();
        }

        private void OnCollisionEnter(Collision other)
        {
            print("Collision detected");
            if (other.gameObject.CompareTag("Santa"))
            {
                print("Santa detected");
                _anim.SetBool(Steal, true);
            }
        }

        void FixedUpdate()
        {
            ManageMovement();
            ManageOrientation();
            ManageJump(); 
            DetectObjecteNearBy();
        }

        void DetectObjecteNearBy()
        {
            var kidPosition = transform.position;
            float radius = Mathf.Sqrt(kidPosition.x * kidPosition.x + kidPosition.z * kidPosition.z);
            
            for (int i = 0; i < numberOfRays; i++)
            {
                //float angle = (i * 360f / numberOfRays) * Mathf.Deg2Rad; // Calculate angle in radians
                //Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)); // Create direction vector
                float angle = (i * 90f / numberOfRays) - (90f / 2f); // Calculate angle in degrees
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.forward; // Create direction vector

                // Calculate ray origin and direction
                Vector3 rayOrigin = centerPoint.position;
                Vector3 rayDirection = direction * radius;

                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxRayLength, layerMask) && !_objDetected)
                {
                    if (hit.collider.gameObject.CompareTag("Obstacle"))
                    {
                        _jumping = true;
                        _objDetected = true;
                    }
                    
                    if(hit.collider.gameObject.CompareTag("Santa"))
                    {
                        _anim.SetBool(Steal, true);
                        _objDetected = true;
                    }
                }
            }
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
            else
            {
                _speedY -= gravityScale * Time.deltaTime;
                _jumping = false;
            }
        }
    }
}
