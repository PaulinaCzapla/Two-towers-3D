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
        private bool _playerWasOn;

        private void Start()
        {
            MovePlatform();
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
           MovePlatform();
        }

        private void CheckIfPlayerOn()
        {
            var player = checker.CheckIfPlayerInside();

            if (_player == null && player != null)
                _player = player;

            if (_player && player == null && _playerWasOn)
            {
                _player.transform.SetParent(null);
                _playerWasOn = false;
            }
            else if (player && player != null)
            {
                _player.transform.SetParent(transform);
                _playerWasOn = true;
            }
        }

        private void MovePlatform()
        {
            platformTransform.DOMove(platformMovements[_index].TargetPosition,
                platformMovements[_index].timeToAchieveTargetPos).SetEase(Ease.InOutSine);
        }

        private void OnDrawGizmos()
        {
            checker.OnDrawGizmos();
        }
    }
}