using System;
using Player.GiftBullet;
using UnityEngine;

namespace Player
{
    public class PlayerThrowGift : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float relativeVerticalPosition = 2f;
        private GiftStateManager _giftStateManager;
        private PlayerMovement _movePlayer;
        
        private void Start()
        {
            _movePlayer = GetComponent<PlayerMovement>();
            _giftStateManager = GiftStateManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GiftType ammunitionSelected = _giftStateManager.GetAmmunitionSelected();
                // If there is no ammunition, the player cannot shoot
                if(ammunitionSelected == GiftType.Blue && _giftStateManager.GetBlueGifts() <= 0) return;
                if(ammunitionSelected == GiftType.Red && _giftStateManager.GetRedGifts() <= 0) return;
                
                // Create the bullet
                var position = transform.position + new Vector3(0, relativeVerticalPosition, 0);
                GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
                var giftController = bullet.GetComponent<GiftController>();
                if (_movePlayer.ViewDirection) giftController.direction = 1;
                else giftController.direction = -1;
                bullet.transform.parent = transform.parent;
                
                // Update the number of ammunition
                if (ammunitionSelected == GiftType.Blue) _giftStateManager.RemoveBlueGift();
                else _giftStateManager.RemoveRedGift();
            }
        }
    }
}

