using System;
using UnityEngine;

namespace Obstacles.Platforms
{
    [Serializable]
    public struct MovementParams
    {
        public Vector3 TargetPosition => targetTransform.position;
        public float timeToAchieveTargetPos;
        public float interval;
        [SerializeField] private Transform targetTransform;
    }
}