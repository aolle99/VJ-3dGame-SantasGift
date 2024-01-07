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
            // Obtén la dirección desde la posición actual hasta la posición del padre
            Vector3 directionToParent = transform.parent.position - transform.position;

            // Proyecta la dirección sobre el plano horizontal (X-Z)
            Vector3 horizontalDirection = new Vector3(directionToParent.x, 0.0f, directionToParent.z);

            // Asegúrate de que la dirección no sea cero para evitar problemas con LookAt
            if (horizontalDirection.sqrMagnitude > 0.001f)
            {
                // Utiliza LookAt solo en las coordenadas X y Z
                transform.LookAt(transform.position + horizontalDirection);
        
                // Gira el objeto 90 grados alrededor del eje Y
                transform.Rotate(Vector3.up, 90.0f);

                // Invierte la dirección si es necesario
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