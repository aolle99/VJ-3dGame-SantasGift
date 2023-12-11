using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public bool shield;
    public int gifts = 0;
    public int giftsNeeded = 3;
    // Start is called before the first frame update
    void Start()
    {
        shield = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gift")
        {
            if(shield)
            {
                shield = false;
                print("Shield Lost");
            }
            else
            {
                gifts++;
                if (gifts == giftsNeeded)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
     
    }
}
