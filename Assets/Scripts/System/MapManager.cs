using System.Collections.Generic;
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
            faseActual = 0;
            Radius = configuracionFases[faseActual].radious;
            teleport_particles.Play();
        }
        
        public void NextPhase()
        {
            if (faseActual < numeroDeFases - 1)
            {
                faseActual++;
                player.transform.position = configuracionFases[faseActual].startPoint;
                teleport_particles.Play();
            }
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
        }
    }
}

