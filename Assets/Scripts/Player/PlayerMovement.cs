using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed, jumpSpeed, gravity;

    Vector3 startDirection;
    float speedY;
    bool doubleJump;
    bool singleJump;
    [SerializeField]

    private bool dashed;
    float dashTimer;
    [SerializeField]
    float dashDelay = 1f;
    [SerializeField]
    float dashDuration = 0.2f;
    [SerializeField]
    float dashSpeedMultiplier = 3f;

    public bool charDirection;

    CharacterController charControl;

    // Start is called before the first frame update
    void Start()
    {
        // Store starting direction of the player with respect to the axis of the level
        charControl = GetComponent<CharacterController>();
        startDirection = transform.position - transform.parent.position;
        startDirection.y = 0.1f;
        startDirection.Normalize();

        speedY = 0;
        doubleJump = false;
        singleJump = false;

        charDirection = true; // true = right, false = left

        dashTimer = dashDelay + dashDuration; // To allow first dash

    }

    private void Update()
    {
        ManageInputs();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageMovement();
        ManageOrientation();
        ManageJump();
    }

    void ManageInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!dashed && dashTimer > (dashDuration + dashDelay))
            {
                dashed = true;
                dashTimer = 0.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (singleJump && !doubleJump)
            {
                doubleJump = true;
            }
        }



    }

    void ManageMovement()
    {
        Vector3 position;

        // Left-right movement

        float angle;
        Vector3 direction, target;

        position = transform.position;
        angle = rotationSpeed * Time.deltaTime;

        dashTimer += Time.deltaTime;
        if (dashed)
        {
            if (dashTimer > dashDuration)
            {
                dashed = false;

            }
            else
            {

                angle *= dashSpeedMultiplier;

            }
        }

        direction = position - transform.parent.position;
        if (Input.GetKey(KeyCode.A) || (dashed && !charDirection))
        {
            target = transform.parent.position + Quaternion.AngleAxis(angle, Vector3.up) * direction;
            charDirection = false;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {

                transform.position = position;
                Physics.SyncTransforms();
            }
        }
        if (Input.GetKey(KeyCode.D) || (dashed && charDirection))
        {
            target = transform.parent.position + Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            charDirection = true;
            if (charControl.Move(target - position) != CollisionFlags.None)
            {
                transform.position = position;
                Physics.SyncTransforms();
            }
        }

    }

    void ManageOrientation()
    {
        // Correct orientation of player
        // Compute current direction
        Vector3 currentDirection = transform.position - transform.parent.position;
        currentDirection.y = 0.0f;
        currentDirection.Normalize();
        // Change orientation of player accordingly
        Quaternion orientation;
        if ((startDirection - currentDirection).magnitude < 1e-3)
            orientation = Quaternion.AngleAxis(0.0f, Vector3.up);
        else if ((startDirection + currentDirection).magnitude < 1e-3)
            orientation = Quaternion.AngleAxis(180.0f, Vector3.up);
        else
            orientation = Quaternion.FromToRotation(startDirection, currentDirection);

        orientation.eulerAngles = new Vector3(0.0f, orientation.eulerAngles.y, 0.0f); // Corregeix un bug raro
        transform.rotation = orientation;

        if (!charDirection) transform.Rotate(Vector3.up, 180.0f);


    }

    void ManageJump()
    {
        Vector3 position;
        // Apply up-down movement
        position = transform.position;
        if (charControl.Move(speedY * Time.deltaTime * Vector3.up) != CollisionFlags.None)
        {
            transform.position = position;
            Physics.SyncTransforms();
        }
        if (charControl.isGrounded)
        {
            doubleJump = false;
            singleJump = false;
            if (speedY < 0.0f)
                speedY = 0.0f;
            if (!singleJump && Input.GetKey(KeyCode.W))
            {
                speedY = jumpSpeed;
                singleJump = true;

            }

        }
        else
            speedY -= gravity * Time.deltaTime;

        if (singleJump && doubleJump)
        {
            if (speedY < 0.0f) speedY = 0.0f;
            speedY = jumpSpeed;
            doubleJump = false;
            singleJump = false;

        }
    }
}
