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
        [SerializeField] private YoyoMovementBetweenPoints movement;
        [SerializeField] private PlayerInsideChecker checker;
        
        private Sequence _sequence;
        private int _index = 1;
        private GameObject _player;
        private bool _increment = true;
        private bool _playerWasOn;

        private void Awake()
        {
            movement.StartMovement();
        }

        private void Update()
        {
            CheckIfPlayerOn();
            movement.UpdateMovement();
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

        private void OnDrawGizmos()
        {
            checker.OnDrawGizmos();
        }
    }
}