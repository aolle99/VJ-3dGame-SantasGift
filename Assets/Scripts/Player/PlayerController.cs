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
        // Start is called before the first frame update
        void Start()
        {
            _maxHealth = 100f;
            _currentHealth = _maxHealth;
            healthBar.UpdeateHealthBar(_maxHealth, _currentHealth);
        }

        private void OnMouseDown()
        {
            _currentHealth -= 10f;
            healthBar.UpdeateHealthBar(_maxHealth, _currentHealth);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
