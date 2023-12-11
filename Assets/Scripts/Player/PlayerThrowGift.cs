using Player.GiftBullet;
using UnityEngine;

namespace Player
{
    public class PlayerThrowGift : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        private PlayerMovement _movePlayer;
        private GiftController _giftController;
        // Start is called before the first frame update
        private void Start()
        {
            _movePlayer = GetComponent<PlayerMovement>();
            _giftController = GetComponent<GiftController>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                if (_movePlayer.ViewDirection)
                    _giftController.Direction = 1;
                else
                    _giftController.Direction = -1;
                bullet.transform.parent = transform.parent;
            }
        }
    }
}

