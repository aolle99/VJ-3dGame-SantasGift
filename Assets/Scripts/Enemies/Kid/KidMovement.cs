using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Kid
{
    public class KidMovement : MonoBehaviour
    {
        public float movementSpeed;
        public float gravityScale, jumpForce;
        public Transform centerPoint;
        public LayerMask layerMask;
        public int numberOfRays = 36;
        public float maxRayLength = 5f;
        public bool ViewDirection { get; private set; }

        private float _angle = 0f;
        private Animator _anim;
        private Boolean _jumping = false;
        private Boolean _canJump = true;
        private Boolean _steal = false;
        private CharacterController _charControl;
        private Vector3 _startDirection;
        private float _speedY;
        private float _timerRunAway = 0f;
        private GiftStateManager _giftStateManager;
        private KidController _kidController;
        [SerializeField] private int damageCaused = 3;
        [SerializeField] private PlayerController playerController;

        private static readonly int Jump = Animator.StringToHash("jump");
        private static readonly int Steal = Animator.StringToHash("steal");
        private static readonly int Walk = Animator.StringToHash("walk");

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
            _giftStateManager = GiftStateManager.Instance;
            _kidController = GetComponent<KidController>();
        }

        void FixedUpdate()
        {
            ManageMovement();
            ManageJump();
            DetectObjecteNearBy();
            ManageStealAnimation();
        }

        void ManageStealAnimation()
        {
            if (_anim.GetBool(Steal))
            {
                if (!_steal) _steal = true;
                else _timerRunAway += Time.deltaTime;
                movementSpeed = 0f;
            }

            if (_timerRunAway > 1f)
            {
                if (_anim.GetBool(Steal))
                {
                    ViewDirection = !ViewDirection;
                    _anim.SetBool(Steal, false);
                    movementSpeed = 12f;
                    _kidController.UpdateLifeBar(2f);
                }
                else
                {
                    _timerRunAway += Time.deltaTime;
                }
            }

            if (_timerRunAway > 3f)
            {
                movementSpeed = 8f;
                _timerRunAway = 0f;
                _steal = false;
                _anim.SetTrigger(Walk);
            }
        }

        void ManageGiftsCount()
        {
            GiftType ammunitionSelected = _giftStateManager.GetAmmunitionSelected();
            if (ammunitionSelected == GiftType.Blue)
            {
                _giftStateManager.RemoveBlueGift();
            }
            else
            {
                _giftStateManager.RemoveRedGift();
            }
        }

        void DetectObjecteNearBy()
        {
            var kidPosition = transform.position;
            float radius = Mathf.Sqrt(kidPosition.x * kidPosition.x + kidPosition.z * kidPosition.z);

            for (int i = 0; i < numberOfRays; i++)
            {
                float angle = (i * 90f / numberOfRays) - (90f / 2f);
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.forward;

                Vector3 rayOrigin = centerPoint.position;
                Vector3 rayDirection = direction * radius;

                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxRayLength, layerMask))
                {
                    if (hit.collider.gameObject.CompareTag("Obstacle") && !_jumping)
                    {
                        _jumping = true;
                    }

                    if (hit.collider.gameObject.CompareTag("Santa") && !_anim.GetBool(Steal))
                    {
                        _anim.SetBool(Steal, true);
                        playerController.damagePlayer(damageCaused);
                    }
                }
            }
        }

        void ManageMovement()
        {
            var position = transform.position;

            var actualRadius = Mathf.Sqrt(position.x * position.x + position.z * position.z);
            var wantedRadius = 25.0f;
            if (ViewDirection)
            {
                _angle = -movementSpeed * Time.deltaTime * wantedRadius / actualRadius;
            }
            else
            {
                _angle = movementSpeed * Time.deltaTime * wantedRadius / actualRadius;
            }

            var direction = position - transform.parent.position;
            direction = Quaternion.AngleAxis(_angle, Vector3.up) * direction;
            if (_charControl.Move(direction - position) == CollisionFlags.Sides)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
        }

        void ManageJump()
        {
            var position = transform.position;

            Vector3 verticalMovement = new Vector3(0, _speedY, 0) * Time.deltaTime;


            var collisions = _charControl.Move(verticalMovement);
            if (collisions != CollisionFlags.None)
            {
                if (collisions == CollisionFlags.Above)
                {
                    transform.position = position;
                    Physics.SyncTransforms();
                }
            }


            if (_charControl.isGrounded)
            {
                _anim.SetBool(Jump, false);
                _canJump = true;
                if (_speedY < 0.0f) _speedY = 0.0f;
                if (_canJump && _jumping)
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