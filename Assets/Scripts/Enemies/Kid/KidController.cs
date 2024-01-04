using System;
using Enemies.Kid.LifeControllers;
using UnityEngine;

namespace Enemies.Kid
{
    public class KidController : MonoBehaviour
    {
        private float _currentHealth;
        private float _maxHealth;
        private float _currentShield;
        private float _maxShield;
        [SerializeField]private HealthBarKid healthBar;
        [SerializeField]private ShieldKid shield;
        
        void Start()
        {
            healthBar = GetComponentInChildren<HealthBarKid>();
            shield = GetComponentInChildren<ShieldKid>();
            _maxHealth = 50f;
            _currentHealth = _maxHealth;
            _maxShield = 30f;
            _currentShield = _maxShield;
            healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            shield.UpdateShield(_maxShield, _currentShield);
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        public void UpdateLifeBar()
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