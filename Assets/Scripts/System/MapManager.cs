using System.Collections;
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
        
        private GameObject _santaModel;

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
            
            _santaModel = player.transform.Find("Santa").gameObject;
            
            MapZoneInner = false;
            Radius = configuracionFases[faseActual].radious;
            playerMovement = player.GetComponent<PlayerMovement>();
            DesactivateAllFases();
            ConfigurePhase();
            MovePlayerToStartPoint();
            SantaIn();
            
            Invoke(nameof(ActivatePlayerMovement), 3f);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.N))
                NextPhase();
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                faseActual = 0;
                NextPhase();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                faseActual = 1;
                NextPhase();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                faseActual = 2;
                NextPhase();
            }
                
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
                DesactivatePlayerMovement();
                ConfigurePhase();
                SantaOutIn();
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
            //teleport_particles.Play();
        }

        private void MovePlayerBetweenRadius()
        {
            var playerPos = player.transform.position;
            var oldRadius = MapZoneInner ? configuracionFases[faseActual].radious : configuracionFases[faseActual].innerRadious;
            
            var newX = playerPos.x * Radius / oldRadius;
            var newZ = playerPos.z * Radius / oldRadius;
            
            var targetPos = new Vector3(newX, playerPos.y, newZ);
            
            DesactivatePlayerMovement();
            player.transform.position = targetPos;
            
            Invoke(nameof(ActivatePlayerMovement),0.7f);
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
        
        private void DesactivatePlayerMovement()
        {
            playerMovement.enabled = false;
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
        
        private void SantaIn()
        {
            StartCoroutine(SantaInAnimation());
        }
        
        private IEnumerator SantaInAnimation()
        {
            _santaModel.SetActive(false);
            teleport_particles.Play();
            yield return new WaitForSeconds(2f);
            _santaModel.SetActive(true);
            yield return new WaitForSeconds(1f);
            teleport_particles.Stop();
        }
        
        private void SantaOut()
        {
            StartCoroutine(SantaOutAnimation());
        }
        
        private IEnumerator SantaOutAnimation()
        {
            teleport_particles.Play();
            yield return new WaitForSeconds(1f);
            _santaModel.SetActive(false);
        }
        
        private void SantaOutIn()
        {
            StartCoroutine(SantaOutInAnimation());
        }
        
        private IEnumerator SantaOutInAnimation()
        {
            teleport_particles.Play();
            yield return new WaitForSeconds(2f);
            _santaModel.SetActive(false);
            teleport_particles.Stop();
            
            yield return new WaitForSeconds(1f);
            
            MovePlayerToStartPoint();
            teleport_particles.Play();
            yield return new WaitForSeconds(2f);
            _santaModel.SetActive(true);
            
            yield return new WaitForSeconds(1f);
            teleport_particles.Stop();
            
            ActivatePlayerMovement();
        }
        
    }
}

