using System;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Boss
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;
        
        private void Start()
        {
        }
        
        public void UpdateHealthBar(float maxHealth, float currentHealth)
        {
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }

    }
}