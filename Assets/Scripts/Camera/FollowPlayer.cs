using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    Vector3 startDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Store starting direction of the player with respect to the axis of the level
        startDirection = player.transform.position - player.transform.parent.position;
        startDirection.y = 0.0f;
        startDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 3, transform.position.z);
        // Compute current direction
        Vector3 currentDirection = player.transform.position - player.transform.parent.position;
        currentDirection.y = 0.0f;
        currentDirection.Normalize();
        // Change orientation of the camera pivot to match the player's
        Quaternion orientation;
        if ((startDirection - currentDirection).magnitude < 1e-3)
            orientation = Quaternion.AngleAxis(0.0f, Vector3.up);
        else if ((startDirection + currentDirection).magnitude < 1e-3)
            orientation = Quaternion.AngleAxis(180.0f, Vector3.up);
        else
            orientation = Quaternion.FromToRotation(startDirection, currentDirection);
        orientation.eulerAngles = new Vector3(0.0f, orientation.eulerAngles.y, 0.0f); // Corregeix un bug raro
        transform.parent.rotation = orientation;
    }
}
