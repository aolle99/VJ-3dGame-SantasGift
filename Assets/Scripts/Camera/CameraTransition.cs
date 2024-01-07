using System;
using UnityEngine;
using System.Collections;


namespace Camera
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField] private float transitionDuration = 1.0f;
        [SerializeField] private bool startFadeOut = true;
        private bool inTransition = false;
        private float alpha = 0f;
        
        private static CameraTransition _instance;
        public static CameraTransition Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = FindObjectOfType<CameraTransition>();
                }
    
                return _instance;
            }
        }

        private void OnGUI()
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }

        private void Start()
        {
            
            
            if (startFadeOut)
            {
                StartFadeOut();
            }
        }
        
        public void StartFadeOut()
        {
            if (!inTransition)
            {
                StartCoroutine(FadeOut());
            }
        }
        
        public void StartFadeIn()
        {
            if (!inTransition)
            {
                StartCoroutine(FadeIn());
            }
        }

        private IEnumerator FadeOut()
        {
            inTransition = true;
            float startTime = Time.time;
            while (Time.time - startTime < transitionDuration)
            {
                alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / transitionDuration);
                yield return null;
            }

            inTransition = false;
        }

        private IEnumerator FadeIn()
        {
            inTransition = true;
            float startTime = Time.time;

            while (Time.time - startTime < transitionDuration)
            {
                alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / transitionDuration);
                yield return null;
            }

            inTransition = false;
        }
    }
}