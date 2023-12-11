using UnityEngine;

namespace Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        public UnityEngine.Camera[] cameras; 
        private int _currentCameraIndex = 0;
        public KeyCode switchKey = KeyCode.C; 

        void Start()
        {
            for (int i = 1; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(switchKey))
            {
                cameras[_currentCameraIndex].gameObject.SetActive(false);

                _currentCameraIndex = (_currentCameraIndex + 1) % cameras.Length;

                cameras[_currentCameraIndex].gameObject.SetActive(true);
            }
        }
    }
}