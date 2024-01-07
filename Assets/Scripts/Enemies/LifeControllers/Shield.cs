using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace Enemies.LifeControllers
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Image shieldSprite;
        [SerializeField] private GiftType giftType;
        [SerializeField] private Boolean randomColor = true;

        private void Start()
        {
            if (randomColor)
            {
                ChangeRandomColor();
            }
        }

        public void UpdateShield(float maxShield, float currentShield)
        {
            shieldSprite.fillAmount = currentShield / maxShield;
        }

        public GiftType GetShieldColor()
        {
            return giftType;
        }

        public void ChangeRandomColor()
        {
            Color blue, red;
            ColorUtility.TryParseHtmlString("#8D5558", out red);
            ColorUtility.TryParseHtmlString("#4A6584", out blue);

            Random random = new Random();

            if (random.Next(0, 2) == 0)
            {
                shieldSprite.color = red;
                giftType = GiftType.Red;
            }

            else
            {
                shieldSprite.color = blue;
                giftType = GiftType.Blue;
            }
        }
    }
}