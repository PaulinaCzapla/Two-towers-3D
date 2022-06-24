using System;
using DG.Tweening;
using UnityEngine;

namespace Obstacles.Platforms
{
    public class RotatingPlatform : MonoBehaviour
    {
        [SerializeField] private float rotationTime;
        [SerializeField] private float pause;
        [SerializeField] private Transform platform;

        private Sequence _sequence;
        private void Awake()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(platform.DORotate(platform.rotation.eulerAngles - new Vector3(0f, 0f, 180f), rotationTime))
                .AppendInterval(pause).SetLoops(-1);
        }
    }
}