using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnowballMovement : MonoBehaviour
{
    public float rotationSpeed;
    private float angle = 0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "House")
        {
            print("House Collision");
        }
    }

    void ManageMovement()
    {
        Vector3 position, direction, target;

        position = transform.position;
        direction = position - transform.parent.position;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        target = transform.parent.position + rotation * direction;

        rb.Move(target, rotation);

        // Simulate rotation based on movement direction
        rb.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        angle = rotationSpeed * Time.deltaTime;
    }
}
