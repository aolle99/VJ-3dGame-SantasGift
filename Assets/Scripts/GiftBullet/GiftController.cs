using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour
{
    // Start is called before the first frame update
    public int direction = 1;
    public float speed = 5.0f;
    float angle = 0f;
    public float bulletDuration = 1f;
    float lifeTime = 0f;
    void Start()
    {
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
        Debug.Log(transform.position.x.ToString());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move bullet in a circle
        angle += speed * Time.deltaTime * direction;

        float radius = GameManager.instance.radius;

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, transform.position.y, z);

        lifeTime += Time.deltaTime;

        if (lifeTime > bulletDuration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
