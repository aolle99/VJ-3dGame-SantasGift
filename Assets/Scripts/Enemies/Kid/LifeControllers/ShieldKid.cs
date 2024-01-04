using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Kid.LifeControllers
{
    public class ShieldKid : MonoBehaviour
    {
        [SerializeField] private Image shieldSprite;
        
        public void UpdateShield(float maxShield, float currentShield)
        {
            shieldSprite.fillAmount = currentShield / maxShield;
        }
    }
}