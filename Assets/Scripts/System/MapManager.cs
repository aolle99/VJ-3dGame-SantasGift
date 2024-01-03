using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ConfiguracionFase
{
    public Vector3 startPoint;
    public float radious;
    public float innerRadious;
    public GameObject faseObject;
    public GameObject nextFaseObject;
}

namespace System
{
    public class MapManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static MapManager instance;
        
        public bool MapZoneInner {get; private set; }
        public float Radius { get; private set; }
        
        [Header("Configuración de Fases")]
        [SerializeField]
        private int numeroDeFases;
        
        [SerializeField]
        private int faseActual;

        [Header("Configuración de Fases")]
        [SerializeField]
        private List<ConfiguracionFase> configuracionFases = new List<ConfiguracionFase>();
        
        [Header("Jugador")]
        [SerializeField]
        private GameObject player;
        private PlayerMovement playerMovement;
        
        [Header("Particulas")]
        [SerializeField]
        private ParticleSystem teleport_particles;
        


        private void OnValidate()
        {
            numeroDeFases = Mathf.Max(0, numeroDeFases);

            // Ajustar automáticamente la longitud de la lista
            while (configuracionFases.Count < numeroDeFases)
            {
                configuracionFases.Add(new ConfiguracionFase());
            }

            while (configuracionFases.Count > numeroDeFases)
            {
                configuracionFases.RemoveAt(configuracionFases.Count - 1);
            }
        }

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
            
            MapZoneInner = false;
            Radius = configuracionFases[faseActual].radious;
            playerMovement = player.GetComponent<PlayerMovement>();
            
            MovePlayerToStartPoint();
        }
        
        public void NextPhase()
        {
            if (faseActual < numeroDeFases - 1)
            {
                faseActual++;
                
                MovePlayerToStartPoint();
            }
        }

        private void MovePlayerToStartPoint()
        {
            playerMovement.enabled = false;
            player.transform.position = configuracionFases[faseActual].startPoint;
            
            Invoke(nameof(ActivatePlayerMovement), 0.3f);
            teleport_particles.Play();
        }

        private void MovePlayerBetweenRadius()
        {
            var playerPos = player.transform.position;
            playerPos = playerPos.normalized * Radius;
            
            playerMovement.enabled = false;
            player.transform.position = playerPos;
            
            Invoke(nameof(ActivatePlayerMovement), 0.3f);
        }

        public void ChangeMapZone()
        {
            MapZoneInner = !MapZoneInner;
            if (MapZoneInner)
            {
                Radius = configuracionFases[faseActual].innerRadious;
            }
            else
            {
                Radius = configuracionFases[faseActual].radious;
            }
            MovePlayerBetweenRadius();
        }
        
        private void ActivatePlayerMovement()
        {
            playerMovement.enabled = true;
        }
    }
}

