using System;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Boss
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;
        private UnityEngine.Camera _camera;
        
        private void Start()
        {
            _camera = UnityEngine.Camera.main;
        }
        
        public void UpdeateHealthBar(float maxHealth, float currentHealth)
        {
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }
    }
}