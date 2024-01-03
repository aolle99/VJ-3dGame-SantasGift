using UnityEngine;

namespace Enemies.Boss
{
    public class BossController : MonoBehaviour
    {
        private float _currentHealth;
        private float _maxHealth;
        private float _currentShield;
        private float _maxShield;
        [SerializeField]private HealthBar healthBar;
        [SerializeField]private Shield shield;
        
        void Start()
        {
            _maxHealth = 100f;
            _currentHealth = _maxHealth;
            _maxShield = 50f;
            _currentShield = _maxShield;
            healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            shield.UpdateShield(_maxShield, _currentShield);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_currentShield > 0f)
                {
                    _currentShield -= 5f;
                    shield.UpdateShield(_maxShield, _currentShield);
                }
                else if (_currentHealth > 0f)
                {
                    _currentHealth -= 5f;
                    healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
                }
            }
        }
    }
}