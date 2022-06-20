using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Elevator
{
    public class Elevator  : MonoBehaviour
    {
        [Header("Player detection")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Vector3 checkPosCorrection = Vector3.zero;
        [SerializeField] private Vector3 halfExtends;

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
            if (CheckIfPlayerInside() || floor ==0)
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

        private bool CheckIfPlayerInside()
        {
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, halfExtends,
                Quaternion.identity, playerLayer);

            if (hitColliders.Length > 0)
            {
                _player = hitColliders[0].gameObject;
                return true;
            }

            _player = null;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(checkPosCorrection+ transform.position, halfExtends);
        }
    }
}