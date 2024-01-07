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
            // active randomly one snowman
            var random = Random.Range(0, snowmans.Count);
            while (snowmansShown.Contains(random))
            {
                random = Random.Range(0, snowmans.Count);
            }
            snowmansShown.Add(random);
            snowmans[random].SetActive(true);
            _currentSnowmans++;
        }
/*
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
            if (_timeToRaise > 8f)
            {
                numSnowman = InstantiateSnowman();
                _timeToRaise = 0f;
                _isRaised = true;
            }

            if (_isRaised && _currentSnowmans < 2)
            {
                // increase the y position during 2 seconds until the y position is 2
                if (_timeToInstantiate < 2f)
                {
                    var a = new Vector3(0, 0.03f, 0);
                    var b = _rb.transform.position;
                    var c = a + b;
                    print("a: " + a.y);
                    print("b: " + b.y);
                    print("c: " + c.y);
                    _rb.transform.position += a;
                    print("Final: " + _rb.transform.position.y);
                    _timeToInstantiate += Time.deltaTime;
                }
                else
                {
                    _currentSnowmans++;
                    _isRaised = false;
                    _timeToInstantiate = 0f;
                    _rb.useGravity = true;
                }
            }
        }

        private int InstantiateSnowman()
        {
            // active randomly one snowman
            var random = Random.Range(0, snowmans.Count);
            while (snowmansShown.Contains(random))
            {
                random = Random.Range(0, snowmans.Count);
            }
            snowmansShown.Add(random);
            snowmans[random].SetActive(true);
            _rb = snowmans[random].GetComponent<Rigidbody>();
            _rb.useGravity = false;

            return random;
        }
        */
    }
}