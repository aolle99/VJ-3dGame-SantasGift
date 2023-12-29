using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class UILevel : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            ProgressBar progressBar = root.Q<ProgressBar>("Bar");
            progressBar.value = 50;
            progressBar.style.backgroundColor = Color.green;
            
            Label label = root.Q<Label>("GiftsLeft");
            label.text = "3";
        }
    }
}