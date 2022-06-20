using System;
using UnityEngine;

namespace Obstacles.Platforms
{
    [Serializable]
    public struct PlatformMovement
    {
        public Vector3 TargetPosition => targetTransfom.position;
        public float timeToAchieveTargetPos;
        [SerializeField] private Transform targetTransfom;
    }
}