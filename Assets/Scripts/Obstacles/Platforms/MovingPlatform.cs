using System;
using System.Collections;
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
        [SerializeField] private PlayerInsideChecker checker;
        
        private Sequence _sequence;
        private int _index = 1;
        private GameObject _player;
        private bool _increment = true;

        private void Start()
        {
            // _sequence = DOTween.Sequence()
            //     .Append(platformTransform.DOMove(platformMovements[_index].TargetPosition,
            //         platformMovements[_index].timeToAchieveTargetPos))
            //     .AppendCallback(() => _index++)
            //     .AppendCallback(() => _index = (_index >= platformMovements.Count ? 0 : _index))
            //     .AppendInterval(platformMovementsInterval).SetLoops(-1);
            
            transform.DOMove(platformMovements[_index].TargetPosition,
                platformMovements[_index].timeToAchieveTargetPos).SetEase(Ease.InOutSine);
            
        }

        private void Update()
        {
            CheckIfPlayerOn();
            
            
            if (Vector3.Distance(platformMovements[_index].TargetPosition,
                transform.position) < 0.2)
            {
                StartCoroutine(MoveCompleted());
            }
                
        }

       private  IEnumerator  MoveCompleted()
       {
           if (_increment)
               _index++;
           else
               _index--;

           if (_index == platformMovements.Count - 1)
               _increment = false;
           else if (_index == 0)
               _increment = true;
           
           yield return new WaitForSeconds(platformMovements[_index].timeToAchieveTargetPos);

           platformTransform.DOMove(platformMovements[_index].TargetPosition,
                platformMovements[_index].timeToAchieveTargetPos).SetEase(Ease.InOutSine);
        }

        private void CheckIfPlayerOn()
        {
            var player = checker.CheckIfPlayerInside();

            if (_player == null && player != null)
                _player = player;
            
            if(_player && player == null)
                _player.transform.SetParent(null);
            else if (player && player != null)
                _player.transform.SetParent(transform);
        }

        private void OnDrawGizmos()
        {
            checker.OnDrawGizmos();
        }
    }
}