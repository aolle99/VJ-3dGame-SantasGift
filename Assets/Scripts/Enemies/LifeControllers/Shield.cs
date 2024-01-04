using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Enemies.LifeControllers
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Image shieldSprite;
        GiftType _giftType;
        
        private void Start()
        {
            Color blue, red;
            ColorUtility.TryParseHtmlString("#8D5558", out red);
            ColorUtility.TryParseHtmlString("#4A6584", out blue);
            
            Random random = new Random();

            if (random.Next(0, 2) == 0)
            {
                shieldSprite.color = red;
                _giftType = GiftType.Red;
            }

            else
            {
                shieldSprite.color = blue;
                _giftType = GiftType.Blue;
            }
                
        }
        
        public void UpdateShield(float maxShield, float currentShield)
        {
            shieldSprite.fillAmount = currentShield / maxShield;
        }

        public GiftType GetShieldColor()
        {
            return _giftType;
        }
    }
}