using UnityEngine;

namespace Enemies.Boss
{
    public class BossOrientation : MonoBehaviour
    {
        private BossMovement bossMovement;
        public void Start()
        {
            bossMovement = GetComponent<BossMovement>();
            Orientate();
        }

        public void Update()
        {
            Orientate();
        }

        public void Orientate()
        {

            Vector3 directionToParent = transform.parent.position - transform.position;


            Vector3 horizontalDirection = new Vector3(directionToParent.x, 0.0f, directionToParent.z);


            if (horizontalDirection.sqrMagnitude > 0.001f)
            {

                transform.LookAt(transform.position + horizontalDirection);
        

                transform.Rotate(Vector3.up, 90.0f);


                if (!bossMovement.ViewDirection)
                    transform.Rotate(Vector3.up, 180.0f);
            }
        }
        
        public int GetDirection()
        {
            if(bossMovement.ViewDirection) return 1;
            return -1;
        }
    }
}