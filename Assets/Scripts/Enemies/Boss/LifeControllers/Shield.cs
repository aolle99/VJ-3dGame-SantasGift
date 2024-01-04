using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Boss.LifeControllers
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Image shieldSprite;
        
        public void UpdateShield(float maxShield, float currentShield)
        {
            shieldSprite.fillAmount = currentShield / maxShield;
        }
    }
}