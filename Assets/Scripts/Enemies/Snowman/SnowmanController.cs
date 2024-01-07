using System;
using Enemies.LifeControllers;
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
        
        private float _currentHealth;
        private float _maxHealth;
        private float _currentShield;
        private float _maxShield;
        private float _radius;
        [SerializeField]private HealthBar healthBar;
        [SerializeField]private Shield shield;
        [SerializeField]private SnowmanOrientation snowmanOrientation;
        private GiftStateManager _giftStateManager;
        
        void Start()
        {
            _anim = GetComponentInChildren<Animator>();
            healthBar = GetComponentInChildren<HealthBar>();
            shield = GetComponentInChildren<Shield>();
            _maxHealth = 70f;
            _currentHealth = _maxHealth;
            _maxShield = 30f;
            _currentShield = _maxShield;
            healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            shield.UpdateShield(_maxShield, _currentShield);
            _giftStateManager = GiftStateManager.Instance;
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
                if (!throwed)
                {
                    Vector3 position = transform.position;
                    Vector3 ballPosition = new Vector3(position.x, position.y + 2, position.z);
                    GameObject ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
                
                    ball.GetComponent<SnowmanBallController>().direction = snowmanOrientation.GetDirection();
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gift"))
            {
                UpdateLifeBar();
            }
        }

        private void UpdateLifeBar()
        {
            GiftType amunitionSelected = _giftStateManager.GetAmmunitionSelected();
            GiftType shieldColor = shield.GetShieldColor();
            
            if (_currentShield > 0f)
            {
                if (amunitionSelected == shieldColor)
                {
                    _currentShield -= 5f;
                    shield.UpdateShield(_maxShield, _currentShield);
                }
            }
            else if (_currentHealth > 0f)
            {
                _currentHealth -= 5f;
                healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
