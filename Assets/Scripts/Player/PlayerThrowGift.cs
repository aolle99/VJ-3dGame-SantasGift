using Player.GiftBullet;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerThrowGift : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float relativeVerticalPosition = 2f;
        private LevelScript _levelScript;

        private PlayerMovement _movePlayer;
        // Start is called before the first frame update
        private void Start()
        {
            _movePlayer = GetComponent<PlayerMovement>();
            _levelScript = GameObject.Find("UIFirstLevel").GetComponent<LevelScript>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var position = transform.position + new Vector3(0, relativeVerticalPosition, 0);
                GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
                var giftController = bullet.GetComponent<GiftController>();
                if (_movePlayer.ViewDirection)
                    giftController.direction = 1;
                else
                    giftController.direction = -1;
                bullet.transform.parent = transform.parent;
            }
        }
    }
}

