using UnityEngine;

namespace Enemies.Snowman
{
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
                Vector3 position = transform.position;
                Vector3 ballPosition = new Vector3(position.x, position.y + 1, position.z);
                GameObject ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
                ball.GetComponent<SnowmanBallController>().direction = 1;
                ball.transform.parent = transform.parent;
            }
        }
    }
}
