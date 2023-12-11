using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ballPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // execute the following code every 2 seconds
        if (Time.frameCount % 300 == 0)
        {
            Vector3 ballPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
            ball.GetComponent<BallController>().direction = 1;
            ball.transform.parent = transform.parent;
        }
    }
}
