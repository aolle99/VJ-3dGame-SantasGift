using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Boss;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
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
