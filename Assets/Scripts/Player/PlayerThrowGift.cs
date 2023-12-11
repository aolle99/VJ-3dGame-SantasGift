using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowGift : MonoBehaviour
{
    public GameObject bulletPrefab;
    PlayerMovement movePlayer;
    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            if (movePlayer.charDirection)
                bullet.GetComponent<GiftController>().direction = 1;
            else
                bullet.GetComponent<GiftController>().direction = -1;
            bullet.transform.parent = transform.parent;
        }
    }
}
