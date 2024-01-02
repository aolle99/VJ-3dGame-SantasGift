using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Boss
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;
        
        public void UpdeateHealthBar(float maxHealth, float currentHealth)
        {
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }
    }
}