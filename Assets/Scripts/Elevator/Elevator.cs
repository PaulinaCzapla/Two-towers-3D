using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obstacles;
using UnityEngine;

namespace Elevator
{
    public class Elevator  : MonoBehaviour
    {
        [Header("Player detection")]
        [SerializeField] private Vector3 checkPosCorrection = Vector3.zero;
        [SerializeField] private Vector3 halfExtends;
        [SerializeField] private PlayerInsideChecker checker;

        [Header("Floor positions")]
        [SerializeField] private List<float> floorYPositions;
        
        private int _currentFloor;
        private GameObject _player;

        private void OnEnable()
        {
            StaticElevatorEvents.SubscribeToElevatorButtonClicked(OnElevatorButtonClicked);
        }

        private void OnDisable()
        {
            StaticElevatorEvents.UnsubscribeFromElevatorButtonClicked(OnElevatorButtonClicked);
        }

        private void OnElevatorButtonClicked(int floor)
        {
            _player = checker.CheckIfPlayerInside();
            
            if ( _player|| floor ==0)
            {
                if (floor != _currentFloor)
                {
                    if (_player)
                    {
                        _player.transform.SetParent(transform);
                        StartCoroutine(WaitForElevator(floor));
                    }

                    gameObject.transform.DOLocalMoveY(floorYPositions[floor], 2f);
                    _currentFloor = floor;
                }
            }
        }

        private IEnumerator WaitForElevator(int floor)
        {
            yield return new WaitUntil(() =>
                Mathf.Approximately(transform.localPosition.y - floorYPositions[floor], 0));
            _player.transform.SetParent(null);
        }

        private void OnDrawGizmos()
        {
           checker.OnDrawGizmos();
        }
    }
}