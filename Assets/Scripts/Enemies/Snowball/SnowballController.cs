using System;
using Enemies.LifeControllers;
using UnityEngine;

namespace Enemies.Snowball
{
    public class SnowballController : MonoBehaviour
    {
        private float _currentHealth;
        private float _maxHealth;
        private float _currentShield;
        private float _maxShield;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Shield shield;
        [SerializeField] private float damageCaused = 5f;
        private GiftStateManager _giftStateManager;


        void Start()
        {
            healthBar = GetComponentInChildren<HealthBar>();
            shield = GetComponentInChildren<Shield>();
            _maxHealth = 50f;
            _currentHealth = _maxHealth;
            _maxShield = 20f;
            _currentShield = _maxShield;
            healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            shield.UpdateShield(_maxShield, _currentShield);
            _giftStateManager = GiftStateManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gift"))
            {
                UpdateLifeBar();
            }
        }

        public void UpdateLifeBar()
        {
            GiftType amunitionSelected = _giftStateManager.GetAmmunitionSelected();
            GiftType shieldColor = shield.GetShieldColor();

            if (_currentShield > 0f)
            {
                if (amunitionSelected == shieldColor)
                {
                    _currentShield -= damageCaused;
                    shield.UpdateShield(_maxShield, _currentShield);
                }
            }
            else if (_currentHealth > 0f)
            {
                _currentHealth -= damageCaused;
                healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
                var actualSize = transform.localScale;
                var reduceSize = 0.1f;
                transform.localScale = new Vector3(actualSize.x - reduceSize, actualSize.y - reduceSize,
                    actualSize.z - reduceSize);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}