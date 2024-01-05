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
        
        private PlayerInput _playerInput;
        
        private float _inputMove;
        
        private float _moveAcceleration;
        
        private bool _dashed;

        private Vector3 _startDirection;
        
        private float _speedY;
        
        private bool _inputJump = false;
        
        
        private bool _doubleJump;
        
        private bool _doubleJumped;
        
        private bool _singleJump;

        private float _dashTimer;

        private CharacterController _charControl;

        private Animator anim;
        
        private bool _isSlow = false;
        
        private PlayerController _playerController;
        
        //Animator parameters
        private static readonly int AnimSpeed = Animator.StringToHash("movSpeed");
        private static readonly int AnimJumping = Animator.StringToHash("isJumping");
        private static readonly int AnimDashing = Animator.StringToHash("isDashing");
        private static readonly int AnimDoubleJumping = Animator.StringToHash("isDoubleJumping");

        public bool ViewDirection { get; private set; } = true;

        // Start is called before the first frame update
        private void Start()
        {
            // Store starting direction of the player with respect to the axis of the level
            _charControl = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
            
            _playerController = GetComponent<PlayerController>();
            
            ViewDirection = true;
        }
        
        private void OnEnable()
        {
            playerActions.Enable();


            _speedY = 0;
            _doubleJump = false;
            _singleJump = false;

            ViewDirection = true; // true = right, false = left

            _dashTimer = dashDelay + dashDuration; // To allow first dash

            anim = GetComponentInChildren<Animator>();
            
            _isSlow = false;
            
        }
        
        private void OnDisable()
        {
            playerActions.Disable();
        }

        private void Update()
        {
            _inputMove = _playerInput.actions["Walk"].ReadValue<float>();
            
            if(_inputMove != 0) ViewDirection = _inputMove > 0;
            
            anim.SetBool(AnimJumping, _singleJump);
            anim.SetBool(AnimDashing, _dashed);
            anim.SetBool(AnimDoubleJumping, _doubleJump);
            
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            ManageMovement();
            ManageJump();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!_singleJump)
                {
                    _inputJump = true;
                    AudioManager.instance.PlaySound("Jump");
                }
                else if (_singleJump && !_doubleJump && !_doubleJumped)
                {
                    _doubleJump = true;
                    AudioManager.instance.PlaySound("DoubleJump");
                }
            }
        }
        
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!_dashed && _dashTimer > (dashDuration + dashDelay))
                {
                    AudioManager.instance.PlaySound("Dash");
                    _dashed = true;
                    _dashTimer = 0.0f;
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
            
            var actualRadius = Mathf.Sqrt(position.x * position.x + position.z * position.z);
            
            
            var wantedRadius = 70.0f;
            
            
            var angle = _moveAcceleration * Time.deltaTime * wantedRadius / actualRadius;

            _dashTimer += Time.deltaTime;
            if (_dashed)
            {
                if (_dashTimer >= dashDuration)
                {
                    _playerController.SetImortal(false);
                    _dashed = false;

                }
                else
                {
                    _playerController.SetImortal(true);
                    if (_moveAcceleration == 0.0f)
                    {
                        if (ViewDirection)
                        {
                            angle = 20.0f * Time.deltaTime;
                        }
                        else
                        {
                            angle = -20.0f * Time.deltaTime;
                        }
                    }
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
            var animMoveAcceleration = _moveAcceleration;
            if (_isSlow)
            {
                animMoveAcceleration *= slowFactor;
            }
            anim.SetFloat(AnimSpeed, Mathf.Abs(animMoveAcceleration/5));
            
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
                _doubleJumped = false;
                if (_speedY < 0.0f)
                    _speedY = 0.0f;
                if (_inputJump)
                {
                    _speedY = jumpSpeed;
                    _singleJump = true;
                    _inputJump = false;
                }
            }
            else
            {
                _speedY -= gravity * Time.deltaTime;
            }
            
            if (_singleJump && _doubleJump && !_doubleJumped)
            {
                if (_speedY < 0.0f) _speedY = 0.0f;
                _speedY = jumpSpeed;
                _doubleJump = false;
                _doubleJumped = true;
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
