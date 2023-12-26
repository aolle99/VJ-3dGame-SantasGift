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
        private bool _throwed = false;
        private static readonly int Throw = Animator.StringToHash("throw_ball");
        
        void Start()
        {
            _anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _timer += Time.deltaTime;
            // execute the following code every 2 seconds
            if (Math.Abs(_timer - ballSpeed) < 0.05)
            {
                _anim.SetBool(Throw, true);
            }
            else if (Math.Abs(_timer - (ballSpeed + 0.9f)) < 0.1)
            {
                if (!_throwed)
                {
                    Vector3 position = transform.position;
                    Vector3 ballPosition = new Vector3(position.x, position.y + 1, position.z);
                    GameObject ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
                
                    ball.GetComponent<SnowmanBallController>().direction = 1;
                    ball.transform.parent = transform.parent;
                    _throwed = true;
                }
            }
            else if(Math.Abs(_timer - (ballSpeed + 1.46f)) < 0.1)
            {
                _anim.SetBool(Throw, false);
                _timer = 0f;
                _throwed = false;
            }
        }
    }
}
