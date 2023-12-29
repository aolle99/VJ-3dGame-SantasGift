using System;
using UnityEngine;

namespace Enemies.Snowman
{
    public class SnowmanController : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject ballPrefab;
        private float _timer = 0f;
        [SerializeField] private float ballSpeed = 2f;
        private Animator _anim;
        [SerializeField] private bool throwed;
        private static readonly int Throw = Animator.StringToHash("throw_ball");
        
        void Start()
        {
            _anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            _timer += Time.deltaTime;
            // execute the following code every 2 seconds
            if (Math.Abs(_timer - ballSpeed) < 0.05)
            {
                _anim.SetBool(Throw, true);
            }
            else if (Math.Abs(_timer - (ballSpeed + 0.9f)) < 0.1)
            {
                print(throwed);
                if (!throwed)
                {
                    print("trowing");
                    Vector3 position = transform.position;
                    Vector3 ballPosition = new Vector3(position.x, position.y + 2, position.z);
                    GameObject ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
                
                    ball.GetComponent<SnowmanBallController>().direction = 1;
                    ball.transform.parent = transform.parent;
                    throwed = true;
                }
            }
            else if(Math.Abs(_timer - (ballSpeed + 1.46f)) < 0.1)
            {
                _anim.SetBool(Throw, false);
                _timer = 0f;
                throwed = false;
            }
        }
    }
}
