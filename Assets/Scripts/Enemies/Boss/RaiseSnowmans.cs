using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Boss
{
    public class RaiseSnowmans : MonoBehaviour
    {
        private float _timeToRaise = 0f;
        [SerializeField] private int maxSnowmans = 5;
        private int _currentSnowmans = 0;
        private List<int> snowmansShown = new List<int>();

        [SerializeField] private List<GameObject> snowmans = new List<GameObject>();

        private void Start()
        {
            foreach (var snowman in snowmans)
            {
                snowman.SetActive(false);
            }
        }

        private void Update()
        {
            _timeToRaise += Time.deltaTime;
            if (_timeToRaise > 8f && _currentSnowmans < maxSnowmans)
            {
                InstantiateSnowman();
                _timeToRaise = 0f;
            }
        }

        private void InstantiateSnowman()
        {
            var random = Random.Range(0, snowmans.Count);
            while (snowmansShown.Contains(random))
            {
                random = Random.Range(0, snowmans.Count);
            }

            snowmansShown.Add(random);
            snowmans[random].SetActive(true);
            _currentSnowmans++;
        }
    }
}