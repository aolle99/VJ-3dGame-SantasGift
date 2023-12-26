using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Kid
{
    public class KidMovement : MonoBehaviour
    {
        [FormerlySerializedAs("rotationSpeed")] public float movementSpeed;
        public float gravityScale, fallingGravityScale, jumpForce;
        private float _angle = 0f;
        private Rigidbody _rb;
        
        private static readonly int Speed = Animator.StringToHash("speed");
        private Animator _anim;
        private static readonly int Jump = Animator.StringToHash("jump");

        // Start is called before the first frame update
        void Start()
        {
            _anim = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody>();
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    
        void ManageMovement()
        {
            Vector3 position = transform.position;
            Vector3 parentPosition = transform.parent.position;
            Vector3 direction = position - parentPosition;
            Quaternion rotation = Quaternion.AngleAxis(_angle, Vector3.up);
            Vector3 target = parentPosition + rotation * direction;
            float rotationY = Mathf.Atan2(target.z, target.x) * Mathf.Rad2Deg + 180f;
            
            rotation = Quaternion.Euler(0, -rotationY, 0f);
            _rb.Move(target, rotation);
            
            _angle = movementSpeed * Time.deltaTime;
            _anim.SetFloat(Speed, 1);
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            
            if (collision.gameObject.CompareTag("House"))
            {
                //print("House Collision");
            }
            
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("House"))
            {
                //print("House Trigger");
                ManageJump();
            }
        }
        
        void ManageJump()
        {
            //print("jumping");
            _anim.SetBool(Jump, true);
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //_anim.SetBool(Jump, false);
        }
    
        void FixedUpdate()
        {
            ManageMovement();
            ApplyGravity();
        }
    
        void ApplyGravity()
        {
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector3.up * (Physics.gravity.y * (fallingGravityScale - 1f) * Time.deltaTime);
                
            }
            else if (_rb.velocity.y > 0)
            {
                _rb.velocity += Vector3.up * (Physics.gravity.y * (gravityScale - 1f) * Time.deltaTime); 
            }
        }
    
    }

}
