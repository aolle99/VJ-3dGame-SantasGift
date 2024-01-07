using System;
using TMPro;
using UnityEngine;

namespace Environment.InteractionSystem
{
    public class InteractionPromptUI : MonoBehaviour
    {
        private UnityEngine.Camera mainCamera;

        [SerializeField] GameObject uiPanel;

        [SerializeField] private TextMeshProUGUI promptText;

        public bool IsDisplayed
        {
            get => uiPanel.activeSelf;
            set => uiPanel.SetActive(value);
        }


        private void Start()
        {
            mainCamera = UnityEngine.Camera.main;
            uiPanel.SetActive(false);
        }

        private void Update()
        {
            var rotation = mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }

        public void SetUp(string prompt)
        {
            promptText.text = prompt;

            IsDisplayed = true;
        }

        public void Close()
        {
            IsDisplayed = false;
        }
    }
}