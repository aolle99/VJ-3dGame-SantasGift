using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidMovement : MonoBehaviour
{
    public float rotationSpeed, gravityScale, fallingGravityScale, jumpForce;
    private float angle = 0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ManageMovement()
    {
        Vector3 position, direction, target;

        position = transform.position;
        direction = position - transform.parent.position;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        target = transform.parent.position + rotation * direction;
        
        rb.Move(target, rotation);

        angle = rotationSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "House")
        {
            print("House Collision");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House")
        {
            //print("House Trigger");
            manageJump();
        }
    }
    
    void manageJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        ManageMovement();
        ApplyGravity();
    }

    void ApplyGravity()
    {
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallingGravityScale - 1f) * Time.deltaTime;
            
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityScale - 1f) * Time.deltaTime; 
        }
    }

}
