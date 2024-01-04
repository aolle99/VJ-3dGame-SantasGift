using UnityEngine;

namespace Environment
{
    public class ObjectFader : MonoBehaviour
    {
        [SerializeField] private float fadeSpeed = 1.0f;
        [SerializeField] private float alpha = 0.3f;
        [SerializeField] private float fadeTime = 0.5f;
        
        private Shader[] _shaders;
        private Color[] _colors;
        private bool _fading;
        
        private float _alphaValue;
        
        private Renderer _renderer;
        
        private Coroutine _coroutine;

        private float _timer;
        
        private Shader _transparentShader;
        private bool _isRendererNotNull;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _isRendererNotNull = _renderer !=null;
            var materials = _renderer.materials;
            _shaders = new Shader[materials.Length];
            _colors = new Color[materials.Length];
            
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                _shaders[i] = _renderer.materials[i].shader;
            }
            
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                _colors[i] = _renderer.materials[i].color;
            }
            
            _fading = false;
            _timer = 0.0f;
            
            _transparentShader = Shader.Find("TransparentDiffuse");
        }
        
        private void Update()
        {
            if (_fading)
            {
                _timer += Time.deltaTime;
                FadeIn();
                if (_timer >= fadeTime)
                {
                    _fading = false;

                }
            }

            if (!_fading)
            {
                if (_alphaValue < 1.0f)
                {
                    FadeOut();
                }
            }
                
        }
        
        private void FadeIn()
        {

            _alphaValue = Mathf.Lerp(_alphaValue,alpha, fadeSpeed * Time.deltaTime);
            UpdateAlpha();
        }
        
        private void FadeOut()
        {
            _alphaValue = Mathf.Lerp(_alphaValue, 1.0f, fadeSpeed * Time.deltaTime);
            if (_alphaValue < 0.8f)
            {
                UpdateAlpha();
            }
            else
            {
                Reset();
            }
            
        }

        private void UpdateAlpha()
        {
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                var materials = _renderer.materials;
                Color color = materials[i].color;
                color.a = _alphaValue;
                materials[i].color = color;
            }
        }
        
        private void Reset()
        {
            _timer = 0.0f;
            _alphaValue = 0.0f;
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                _renderer.materials[i].shader = _shaders[i];
            }
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                _renderer.materials[i].color=_colors[i];
            }
        }
        
        public void SetFade()
        {
            if (!_fading)
            {

                if (_isRendererNotNull && _renderer.materials.Length > 0)
                {
                    for (int i = 0; i < _renderer.materials.Length; i++)
                    {
                        _renderer.materials[i].shader = _transparentShader;
                    }
                    _fading = true;
                }
            }
            _timer = 0.0f;
        }
    }
}