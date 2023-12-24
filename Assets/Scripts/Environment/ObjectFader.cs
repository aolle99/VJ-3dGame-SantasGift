using UnityEngine;

namespace Environment
{
    public class ObjectFader : MonoBehaviour
    {
        [SerializeField] private float fadeSpeed = 1.0f;
        [SerializeField] private float fadeAmount = 0.0f;
        
        private Material[] _materials;
        private float _originalAlpha;
        private bool _fading;
        
        private void Start()
        {
            _materials = GetComponent<Renderer>().materials;
            foreach (var material in _materials)
            {
                _originalAlpha = material.color.a;
            }
            
            _fading = false;
            
        }
        
        private void Update()
        {
            if (_fading)
                FadeIn();
            else
                FadeOut();
        }
        
        private void FadeIn()
        {
            foreach (var material in _materials)
            {
                var color = material.color;
                color.a = Mathf.Lerp(color.a, _originalAlpha, fadeSpeed * Time.deltaTime);
                material.color = color;
            }
            
        }
        
        private void FadeOut()
        {
            foreach (var material in _materials)
            {
                var color = material.color;
                color.a = Mathf.Lerp(color.a, fadeAmount, fadeSpeed * Time.deltaTime);
                material.color = color;
            }
           
        }
        
        public void setFade(bool fade)
        {
            _fading = fade;
        }
    }
}