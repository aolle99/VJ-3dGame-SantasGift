using UnityEngine;

namespace Enemies.Boss
{
    public class BossController : MonoBehaviour
    {
        private float _currentHealth;
        private float _maxHealth;
        [SerializeField]private HealthBar healthBar;
        void Start()
        {
            _maxHealth = 100f;
            _currentHealth = _maxHealth;
            healthBar.UpdeateHealthBar(_maxHealth, _currentHealth);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _currentHealth -= 10f;
                healthBar.UpdeateHealthBar(_maxHealth, _currentHealth);
            }
        }
    }
}