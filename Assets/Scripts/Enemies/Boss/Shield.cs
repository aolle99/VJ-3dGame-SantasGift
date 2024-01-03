using System;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Boss
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Image _shieldSprite;
        
        private void Start()
        {
        }
        
        public void UpdateShield(float maxShield, float currentShield)
        {
            _shieldSprite.fillAmount = currentShield / maxShield;
        }
    }
}