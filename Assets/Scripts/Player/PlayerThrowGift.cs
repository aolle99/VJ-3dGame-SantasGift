using System;
using System.Collections;
using Player.GiftBullet;
using UnityEngine;

namespace Player
{
    public class PlayerThrowGift : MonoBehaviour
    {
        [SerializeField] private GameObject blueBulletPrefab;
        [SerializeField] private GameObject redBulletPrefab;
        [SerializeField] private float relativeVerticalPosition = 2f;
        [SerializeField] private float timeBetweenThrowsBlue = 0.6f;
        [SerializeField] private float timeBetweenThrowsRed = 1.2f;
        private GiftStateManager _giftStateManager;
        private PlayerMovement _movePlayer;
        private bool _canThrow = true;
        private float _coldDownTimer;
        
        private Animator anim;
        
        private static readonly int AnimThrow = Animator.StringToHash("isThrowing");
        
        private void Start()
        {
            _movePlayer = GetComponent<PlayerMovement>();
            _giftStateManager = GiftStateManager.Instance;
            anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (!_canThrow)
            {
                _coldDownTimer += Time.deltaTime;
                var timeBetweenThrows = _giftStateManager.GetAmmunitionSelected() == GiftType.Blue ? timeBetweenThrowsBlue : timeBetweenThrowsRed;
                if (_coldDownTimer >= timeBetweenThrows)
                {
                    _canThrow = true;
                    _coldDownTimer = 0;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse0) && _canThrow)
            {
                anim.SetBool(AnimThrow, true);
                GiftType ammunitionSelected = _giftStateManager.GetAmmunitionSelected();
                // If there is no ammunition, the player cannot shoot
                var prefab = ammunitionSelected == GiftType.Blue ? blueBulletPrefab : redBulletPrefab;
                if (ammunitionSelected == GiftType.Blue && _giftStateManager.GetBlueGifts() <= 0)
                {
                    // Reproduce the sound of no ammunition
                    return;
                }

                if (ammunitionSelected == GiftType.Red && _giftStateManager.GetRedGifts() <= 0)
                {
                    // Reproduce the sound of no ammunition
                    return;
                }
                
                _canThrow = false;
                
                // If there is ammunition, the player can shoot
                StartCoroutine(ThrowGiftCoroutine(prefab, ammunitionSelected));
                
            }
        }
        IEnumerator ThrowGiftCoroutine(GameObject prefab, GiftType ammunitionSelected)
        {
            yield return new WaitForSeconds(0.3f);
            
            // Create the bullet
            var position = transform.position + new Vector3(0, relativeVerticalPosition, 0);
            GameObject bullet = Instantiate(prefab, position, Quaternion.identity);
            var giftController = bullet.GetComponent<GiftController>();
            if (!_movePlayer.ViewDirection) giftController.SetDirection(-1);
            bullet.transform.parent = transform.parent;
                
            // Update the number of ammunition
            if (ammunitionSelected == GiftType.Blue) _giftStateManager.RemoveBlueGift();
            else _giftStateManager.RemoveRedGift();
            
            Invoke(nameof(StopAnimation), 0.25f);
        }
        
        private void StopAnimation()
        {
            anim.SetBool(AnimThrow, false);
        }
    }
}

