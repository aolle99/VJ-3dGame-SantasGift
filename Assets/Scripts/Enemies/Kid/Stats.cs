using UnityEngine;

namespace Enemies.Kid
{
    public class Stats : MonoBehaviour
    {
        public bool shield;
        public int gifts = 0;
        public int giftsNeeded = 3;

        void Start()
        {
            shield = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Gift"))
            {
                if (shield)
                {
                    shield = false;
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
    }
}