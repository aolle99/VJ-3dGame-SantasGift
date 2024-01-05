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
    public int objectives;
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

        private int _currentPhaseObjectives;
        


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
            DesactivateAllFases();
            ConfigurePhase();
            MovePlayerToStartPoint();
            

        }
        
        private void DesactivateAllFases()
        {
            foreach (var fase in configuracionFases)
            {
                fase.faseObject.SetActive(false);
            }
        }
        
        public void NextPhase()
        {
            if (faseActual < numeroDeFases - 1)
            {
                faseActual++;
                
                ConfigurePhase();
                MovePlayerToStartPoint();
            }
        }
        
        private void ConfigurePhase()
        {
            configuracionFases[faseActual].faseObject.SetActive(true);
            MapZoneInner = false;
            if( configuracionFases[faseActual].objectives > 0)
                configuracionFases[faseActual].nextFaseObject.SetActive(false);
            else configuracionFases[faseActual].nextFaseObject.SetActive(true);
            
            Radius = configuracionFases[faseActual].radious;
        }

        private void MovePlayerToStartPoint()
        {
            playerMovement.enabled = false;
            player.transform.position = configuracionFases[faseActual].startPoint;
            
            Invoke(nameof(ActivatePlayerMovement), 0.3f);
            //teleport_particles.Play();
        }

        private void MovePlayerBetweenRadius()
        {
            var playerPos = player.transform.position;
            var oldRadius = MapZoneInner ? configuracionFases[faseActual].radious : configuracionFases[faseActual].innerRadious;
            
            var newX = playerPos.x * Radius / oldRadius;
            var newZ = playerPos.z * Radius / oldRadius;
            
            var targetPos = new Vector3(newX, playerPos.y, newZ);
            
            playerMovement.enabled = false;
            player.transform.position = targetPos;
            
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
        
        public float GetFaseRadius()
        {
            return configuracionFases[faseActual].radious;
        }
        
        public float GetFaseInnerRadius()
        {
            return configuracionFases[faseActual].innerRadious;
        }
        
        public float GetCurrentFaseRadius()
        {
            return MapZoneInner ? configuracionFases[faseActual].innerRadious : configuracionFases[faseActual].radious;
        }
        
        public void AddObjective()
        {
            _currentPhaseObjectives++;
            if (_currentPhaseObjectives >= configuracionFases[faseActual].objectives)
            {
                configuracionFases[faseActual].nextFaseObject.SetActive(true);
            }
        }
    }
}

