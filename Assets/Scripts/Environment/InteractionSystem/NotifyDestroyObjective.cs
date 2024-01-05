using System;
using UnityEngine;

namespace Environment.InteractionSystem
{
    public class NotifyDestroyObjective : MonoBehaviour
    {
        private void OnDestroy()
        {
            MapManager.instance.AddObjective();
        }
    }
}