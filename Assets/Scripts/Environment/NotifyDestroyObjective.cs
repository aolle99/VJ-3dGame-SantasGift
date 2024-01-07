using System;
using UnityEngine;

namespace Environment
{
    public class NotifyDestroyObjective : MonoBehaviour
    {
        private void OnDestroy()
        {
            MapManager.instance.AddObjective();
        }
    }
}