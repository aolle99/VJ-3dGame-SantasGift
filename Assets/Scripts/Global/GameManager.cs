using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public bool mapZoneInner = false;
    public float radius = 12.5f;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ChangeMapZone()
    {
        mapZoneInner = !mapZoneInner;
        if (mapZoneInner)
        {
            radius = 7.5f;
        }
        else
        {
            radius = 12.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
