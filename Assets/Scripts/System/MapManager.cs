using UnityEngine;

namespace System
{
    public class MapManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static MapManager instance;
        
        public bool MapZoneInner {get; private set; }
        public float Radius { get; private set; }
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
            MapZoneInner = !MapZoneInner;
            if (MapZoneInner)
            {
                Radius = 7.5f;
            }
            else
            {
                Radius = 12.5f;
            }

        }
    }
}

