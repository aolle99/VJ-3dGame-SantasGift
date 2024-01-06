using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens
{
    public class LevelLoading : MonoBehaviour
    {
        private float _timeLoading = 0f;
        VisualElement root;
        private Boolean _isLoaded = false;
        
        private void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            
        }
        
        private void Update()
        {
            if(!_isLoaded) _timeLoading += Time.deltaTime;
            if(_timeLoading > 4f)
            {
                root.visible = false;
            }
        }
    }
}