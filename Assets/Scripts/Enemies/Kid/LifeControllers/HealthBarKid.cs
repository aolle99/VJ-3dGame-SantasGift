using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Kid.LifeControllers
{
    public class HealthBarKid : MonoBehaviour
    {
        [SerializeField] private Image healthBarSprite;
        
        public void UpdateHealthBar(float maxHealth, float currentHealth)
        {
            healthBarSprite.fillAmount = currentHealth / maxHealth;
        }

    }
}