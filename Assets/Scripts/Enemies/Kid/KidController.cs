using System;
using Enemies.LifeControllers;
using UnityEngine;

namespace Enemies.Kid
{
    public class KidController : MonoBehaviour
    {
        private float _currentHealth;
        private float _maxHealth;
        private float _currentShield;
        private float _maxShield;
        [SerializeField]private HealthBar healthBar;
        [SerializeField]private Shield shield;
        private GiftStateManager _giftStateManager;
        
        void Start()
        {
            healthBar = GetComponentInChildren<HealthBar>();
            shield = GetComponentInChildren<Shield>();
            _maxHealth = 40f;
            _currentHealth = _maxHealth;
            _maxShield = 15f;
            _currentShield = _maxShield;
            healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            shield.UpdateShield(_maxShield, _currentShield);
            _giftStateManager = GiftStateManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gift"))
            {
                UpdateLifeBar(5f);
            }
        }

        public void UpdateLifeBar(float damage)
        {
            GiftType amunitionSelected = _giftStateManager.GetAmmunitionSelected();
            GiftType shieldColor = shield.GetShieldColor();
            
            if (_currentShield > 0f)
            {
                if (amunitionSelected == shieldColor)
                {
                    _currentShield -= damage;
                    shield.UpdateShield(_maxShield, _currentShield);
                }
            }
            else if (_currentHealth > 0f)
            {
                _currentHealth -= damage;
                healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}