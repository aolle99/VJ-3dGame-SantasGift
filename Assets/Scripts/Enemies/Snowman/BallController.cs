using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int direction = 1;
    public float speed = 2.0f;
    float angle = 0f;
    public float ballDuration = 1f;
    float lifeTime = 0f;
    void Start()
    {
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
    }

    void FixedUpdate()
    {
        // move bullet in a circle
        angle += speed * Time.deltaTime * direction;

        float radius = 12.5f;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, transform.position.y, z);

        lifeTime += Time.deltaTime;

        if (lifeTime > ballDuration)
        {
            print("destroy ball");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Santa")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Ground")
        {
            print("destroy ball");
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
