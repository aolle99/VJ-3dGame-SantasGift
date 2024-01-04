﻿using System;
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
            //print(shield);
            _maxHealth = 50f;
            _currentHealth = _maxHealth;
            _maxShield = 30f;
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
                print("gift detected");
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
                    _currentShield -= 5f;
                    shield.UpdateShield(_maxShield, _currentShield);
                }
            }
            else if (_currentHealth > 0f)
            {
                _currentHealth -= 5f;
                healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            }
        }
    }
}