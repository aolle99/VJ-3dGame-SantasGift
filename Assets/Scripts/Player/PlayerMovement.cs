using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float maxRotationSpeed;
        [SerializeField] private float accelerationFactor;
        [SerializeField] private float decelerationFactor;
        [SerializeField] private float slowFactor = 0.1f;
        
        [Header("Jump")]
        [SerializeField] private float jumpSpeed;
        
        [SerializeField] private float gravity;
        
        [Header("Dash")]
        [SerializeField] private float dashDelay = 1f;
        
        [SerializeField] private float dashDuration = 0.2f;
        
        [SerializeField] private float dashSpeedMultiplier = 3f;

        [Header("Objects")] 
        //[SerializeField] private GameObject animatorObject;
        
        [SerializeField] private InputActionAsset playerActions;
        
        private float _inputMove;
        
        private float _moveAcceleration;
        
        private bool _dashed;

        private Vector3 _startDirection;
        
        private float _speedY;
        
        private bool _doubleJump;
        
        private bool _singleJump;

        private float _dashTimer;

        private CharacterController _charControl;

        private Animator anim;
        
        private InputAction _walkAction;
        
        private float _radio = 1.0f;
        
        private bool _isSlow = false;
        
        //Animator parameters
        private static readonly int AnimSpeed = Animator.StringToHash("movSpeed");
        private static readonly int AnimJumping = Animator.StringToHash("isJumping");
        private static readonly int AnimDashing = Animator.StringToHash("isDashing");
        private static readonly int AnimDoubleJumping = Animator.StringToHash("isDoubleJumping");

        public bool ViewDirection { get; private set; }

        // Start is called before the first frame update
        private void Start()
        {
            // Store starting direction of the player with respect to the axis of the level
            _charControl = GetComponent<CharacterController>();

            var playerTransform = transform;
            var parent = playerTransform.parent;
            _startDirection = playerTransform.position - parent.position;
            _startDirection.y = 0.1f;
            _startDirection.Normalize();

            _speedY = 0;
            _doubleJump = false;
            _singleJump = false;

            ViewDirection = true; // true = right, false = left

            _dashTimer = dashDelay + dashDuration; // To allow first dash

            anim = GetComponentInChildren<Animator>();
            
            _walkAction = playerActions.FindActionMap("Player").FindAction("Walk");
            
            _radio = Vector3.Distance(transform.position, parent.position);

        }
        
        private void OnEnable()
        {
            playerActions.Enable();
        }
        
        private void OnDisable()
        {
            playerActions.Disable();
        }

        private void Update()
        {
            ManageInputs();
            anim.SetBool(AnimJumping, _singleJump);
            anim.SetBool(AnimDashing, _dashed);
            anim.SetBool(AnimDoubleJumping, _doubleJump);
            
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            ManageMovement();
            ManageOrientation();
            ManageJump();
        }

        private void ManageInputs()
        {
            _inputMove = _walkAction.ReadValue<float>();
            
           
            
            if(_inputMove != 0) ViewDirection = _inputMove > 0;
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!_dashed && _dashTimer > (dashDuration + dashDelay))
                {
                    _dashed = true;
                    _dashTimer = 0.0f;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_singleJump && !_doubleJump)
                {
                    _doubleJump = true;
                }
            }
        }

        private void ManageMovement()
        {
            // calculate acceleration
            if(_inputMove > 0)
                _moveAcceleration = Mathf.Min(_moveAcceleration + accelerationFactor, maxRotationSpeed);
            else if (_inputMove < 0)
                _moveAcceleration = Mathf.Max(_moveAcceleration-accelerationFactor, -maxRotationSpeed);
            else if (_moveAcceleration > 0)
                _moveAcceleration = Mathf.Max(_moveAcceleration - decelerationFactor, 0.0f);
            else if (_moveAcceleration < 0)
                _moveAcceleration = Mathf.Min(_moveAcceleration + decelerationFactor, 0.0f);
            else
                _moveAcceleration = 0.0f;

            var position = transform.position;
            
            var angle = _moveAcceleration * Time.deltaTime;

            _dashTimer += Time.deltaTime;
            if (_dashed)
            {
                if (_dashTimer > dashDuration)
                {
                    _dashed = false;

                }
                else
                {

                    angle *= dashSpeedMultiplier;

                }
            }
            else
            {
                if (_isSlow)
                {
                    angle *= slowFactor;
                }
            }
            
            
            var direction = position - transform.parent.position;
            direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            if (_charControl.Move(direction - position) == CollisionFlags.Sides)
            {
                transform.position = position;
                Physics.SyncTransforms();
                _moveAcceleration = 0.0f;
            }
            anim.SetFloat(AnimSpeed, _moveAcceleration);
        }

        private void ManageOrientation()
        {
            // Correct orientation of player
            // Compute current direction
            var playerTransform = transform;
            var currentDirection = playerTransform.position - playerTransform.parent.position;
            currentDirection.y = 0.0f;
            currentDirection.Normalize();
            // Change orientation of player accordingly
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

        private void ManageJump()
        {
            var position =
                // Apply up-down movement
                transform.position;
            if (_charControl.Move(_speedY * Time.deltaTime * Vector3.up) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }

            if (_charControl.isGrounded)
            {
                _doubleJump = false;
                _singleJump = false;
                if (_speedY < 0.0f)
                    _speedY = 0.0f;
                if (!_singleJump && Input.GetKey(KeyCode.W))
                {
                    _speedY = jumpSpeed;
                    _singleJump = true;

                }

            }
            else
                _speedY -= gravity * Time.deltaTime;

            if (_singleJump && _doubleJump)
            {
                if (_speedY < 0.0f) _speedY = 0.0f;
                _speedY = jumpSpeed;
                _doubleJump = false;
                _singleJump = false;

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("SlowZone"))
            {
                _isSlow = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("SlowZone"))
            {
                _isSlow = false;
            }
        }
    }
}
