using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Speed")]
        [SerializeField] private float rotationSpeed;
        
        [Header("Jump Force")]
        [SerializeField] private float jumpSpeed;
        
        [SerializeField] private float gravity;
        
        [SerializeField] private float dashDelay = 1f;
        
        [SerializeField] private float dashDuration = 0.2f;
        
        [SerializeField] private float dashSpeedMultiplier = 3f;
        
        

        
        private bool _dashed;

        private Vector3 _startDirection;
        
        private float _speedY;
        
        private bool _doubleJump;
        
        private bool _singleJump;

        private float _dashTimer;

        private CharacterController _charControl;
        
        public bool ViewDirection { get; private set; }

        // Start is called before the first frame update
        private void Start()
        {
            // Store starting direction of the player with respect to the axis of the level
            _charControl = GetComponent<CharacterController>();

            var playerTransform = transform;
            _startDirection = playerTransform.position - playerTransform.parent.position;
            _startDirection.y = 0.1f;
            _startDirection.Normalize();

            _speedY = 0;
            _doubleJump = false;
            _singleJump = false;

            ViewDirection = true; // true = right, false = left

            _dashTimer = dashDelay + dashDuration; // To allow first dash

        }

        private void Update()
        {
            ManageInputs();
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
            // Left-right movement

            Vector3 target;

            var position = transform.position;
            var angle = rotationSpeed * Time.deltaTime;

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

            var direction = position - transform.parent.position;
            if (Input.GetKey(KeyCode.A) || (_dashed && !ViewDirection))
            {
                target = transform.parent.position + Quaternion.AngleAxis(angle, Vector3.up) * direction;
                ViewDirection = false;
                if (_charControl.Move(target - position) != CollisionFlags.None)
                {

                    transform.position = position;
                    Physics.SyncTransforms();
                }
            }

            if (Input.GetKey(KeyCode.D) || (_dashed && ViewDirection))
            {
                target = transform.parent.position + Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                ViewDirection = true;
                if (_charControl.Move(target - position) != CollisionFlags.None)
                {
                    transform.position = position;
                    Physics.SyncTransforms();
                }
            }

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
    }
}
