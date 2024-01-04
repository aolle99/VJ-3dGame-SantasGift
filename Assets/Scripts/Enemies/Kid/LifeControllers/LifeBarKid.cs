using UnityEngine;

namespace Enemies.Kid.LifeControllers
{
    public class LifeBarKid : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        
        private void Start()
        {
            _camera = UnityEngine.Camera.main;
        }
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }
    }
}