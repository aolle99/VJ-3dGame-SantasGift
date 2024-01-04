using System;
using UnityEngine;


namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private bool _isDead = false;
        private bool _isInvincible = false;
        private GiftStateManager _giftStateManager;
        
        private void Start()
        {
            _isDead = false;
            _isInvincible = false;
            _giftStateManager = GiftStateManager.Instance;
        }

        private void Update()
        {
            
        }

        public bool damagePlayer(float damage)
        {
            if (_isInvincible)
            {
                return false;
            }

            _isDead = _giftStateManager.removeRandomGift((int) damage);
            
            if(_isDead) playerdead();
            
            return true;
        }

        private void playerdead()
        {
            
        }

    }
}
