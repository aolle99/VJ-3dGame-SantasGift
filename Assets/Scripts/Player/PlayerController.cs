using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private bool _isDead = false;
        private bool _isInvincible = false;
        private bool _godMode = false;
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
                AudioManager.instance.PlaySound("GodMode");
                _godMode = !_godMode;
                _isInvincible = _godMode;
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
            if(_godMode) return;
            
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
                AudioManager.instance.PlaySound("ReloadAmmo");
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
            
            AudioManager.instance.PlaySound("Damage");
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
