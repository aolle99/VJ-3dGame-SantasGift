using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;


namespace System
{
    [Serializable]
    public class SerializableDictionary
    {
        public string effectName;
        public AudioClip audioClip;
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        
        [Header("Audio Settings")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float globalVolume = 1.0f;
        
        [Range(0.0f, 1.0f)]
        [SerializeField] private float musicVolume = 1.0f;
        
        [Range(0.0f, 1.0f)]
        [SerializeField] private float sfxVolume = 1.0f;
        
        [Header("Music Clips")]
        [SerializeField] private AudioClip mainMenuMusic;
        [SerializeField] private AudioClip gameplayMusic;
        [SerializeField] private AudioClip victoryMusic;
        [SerializeField] private AudioClip gameOverMusic;
        [SerializeField] private AudioClip bossMusic;
        
        [SerializeField]
        private List<SerializableDictionary> audioClipsList = new List<SerializableDictionary>();

        // Puedes usar este método para convertir la lista a un diccionario
        private Dictionary<string, AudioClip> audioClips;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            audioClips = audioClipsList.ToDictionary(x => x.effectName, x => x.audioClip);
        }
        
        private void Start()
        {
            AdjustGlobalVolume(globalVolume);
            _audioSource = GetComponent<AudioSource>();
            AdjustMusicVolume(musicVolume);
            AdjustSFXVolume(sfxVolume);
        }
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        public void AdjustGlobalVolume(float newVolume)
        {
            globalVolume = Mathf.Clamp01(newVolume);
            
            AudioListener.volume = globalVolume;
        }
        
        public void AdjustMusicVolume(float newVolume)
        {
            musicVolume = Mathf.Clamp01(newVolume);
            
            _audioSource.volume = musicVolume;
        }
        
        public void AdjustSFXVolume(float newVolume)
        {
            sfxVolume = Mathf.Clamp01(newVolume);
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Lógica para detener el sonido al cargar una escena específica
            if (scene.name == "MainMenu" || scene.name == "Credits" || scene.name == "Instructions")
            {
                if (!_audioSource.clip || _audioSource.clip != mainMenuMusic)
                {
                    _audioSource.clip = mainMenuMusic;
                    _audioSource.Play();
                }
            }
            else if (scene.name == "FirstLevel" || scene.name == "SecondLevel")
            {
                if (!_audioSource.clip || _audioSource.clip != gameplayMusic)
                {
                    _audioSource.clip = gameplayMusic;
                    _audioSource.Play();
                }
            }
            else if (scene.name == "Boss")
            {
                if (!_audioSource.clip || _audioSource.clip != bossMusic)
                {
                    _audioSource.clip = bossMusic;
                    _audioSource.Play();
                }
                
            }
            else if (scene.name == "Victory")
            {
                if (_audioSource.clip != victoryMusic)
                {
                    _audioSource.clip = victoryMusic;
                    _audioSource.Play();
                }
                
            }
            else if (scene.name == "GameOver")
            {
                if (_audioSource.clip != gameOverMusic)
                {
                    _audioSource.clip = gameOverMusic;
                    _audioSource.Play();
                }
                
            }
        }
        
        public void PlaySound(string soundName)
        {
            if (audioClips.ContainsKey(soundName))
            {
                AudioSource tmpAudioSource = gameObject.AddComponent<AudioSource>();
                tmpAudioSource.clip = audioClips[soundName];
                tmpAudioSource.volume = sfxVolume;
                
                tmpAudioSource.Play();
                
                Destroy(tmpAudioSource, audioClips[soundName].length);
            }
            else
            {
                Debug.LogWarning($"Audio clip {soundName} not found!");
            }
        }
    }

}