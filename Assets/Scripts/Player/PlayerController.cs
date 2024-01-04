using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private bool _isDead = false;
        private bool _isInvincible = false;
        private GiftStateManager _giftStateManager;
        private ParticleSystem _inmortalParticles;
        private ParticleSystem _maxAmmoParticles;
        private ParticleSystem _damageBlueParticles;
        private ParticleSystem _damageRedParticles;
        
        private void Start()
        {
            _isDead = false;
            _isInvincible = false;
            _giftStateManager = GiftStateManager.Instance;
            _inmortalParticles = transform.Find("InmortalEffect").GetComponent<ParticleSystem>();
            _maxAmmoParticles = transform.Find("MaxAmmoEffect").GetComponent<ParticleSystem>();
            _damageBlueParticles = transform.Find("DamageBlueEffect").GetComponent<ParticleSystem>();
            _damageRedParticles = transform.Find("DamageRedEffect").GetComponent<ParticleSystem>();
            
        }
        
        public void OnImortal(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _isInvincible = !_isInvincible;
                if (_isInvincible)
                {
                    _inmortalParticles.Play();
                }
                else
                {
                    _inmortalParticles.Stop();
                }
            }
        }
        
        public void SetImortal(bool isImortal)
        {
            _isInvincible = isImortal;
            if (_isInvincible)
            {
                _inmortalParticles.Play();
            }
            else
            {
                _inmortalParticles.Stop();
            }
        }
        
        public void OnRefillAmmo(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _maxAmmoParticles.Play();
                _giftStateManager.refillFullGifts();
            }
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
            
            _damageBlueParticles.Play();
            _damageRedParticles.Play();
            
            if(_isDead) playerdead();
            
            return true;
        }

        private void playerdead()
        {
            
        }

    }
}
