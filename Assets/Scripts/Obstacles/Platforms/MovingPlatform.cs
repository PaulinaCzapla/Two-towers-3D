using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Obstacles.Platforms
{
    public class MovingPlatform : MonoBehaviour
    {
        [Header("Platform params")]
        [SerializeField] private List<PlatformMovement> platformMovements;
        [SerializeField] private Transform platformTransform;
        [SerializeField] private float platformMovementsInterval = 1;
        
        [Header("Player detection")]
        
        
        private Sequence _sequence;
        private int _index = 0;

        private void Start()
        {
            _sequence = DOTween.Sequence()
                .Append(platformTransform.DOMove(platformMovements[_index].TargetPosition,
                    platformMovements[_index].timeToAchieveTargetPos))
                .AppendCallback(() => _index++)
                .AppendCallback(() => _index = (_index >= platformMovements.Count ? 0 : _index))
                .AppendInterval(platformMovementsInterval).SetLoops(-1);
        }
    }
}