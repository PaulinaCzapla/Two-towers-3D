﻿
using System;
using System.Collections.Generic;
using DG.Tweening;
using Obstacles.Platforms;
using Timers;
using UnityEngine;

namespace Obstacles
{
    [Serializable]
    public class YoyoMovementBetweenPoints
    {
        [SerializeField] private List<MovementParams> movements;
        [SerializeField] private Transform movingObjectTransform;
        [SerializeField] private float movementStartDelay = 0;
        private Sequence _sequence;
        private int _index = 1;
        private bool _increment = true;
        private Cooldown _cooldown;
        private bool _isMoving = false;

        public void StartMovement()
        {
            if (movements.Count == 0)
                return;
            
            _cooldown = new Cooldown(movementStartDelay);
            _cooldown.StartCooldown();
            
            //Move();

            if (movements.Count == 2)
                _increment = false;
        }
        
        public void UpdateMovement()
        {
            if (movements.Count == 0)
                return;
            
            if (Vector3.Distance(movements[_index].TargetPosition,
                movingObjectTransform.position) < 0.2)
            {
                MoveCompleted();
                _isMoving = false;
            }

            if (!_isMoving && _cooldown.CooldownEnded)
            {
                _isMoving = true;
                Move();
            }
        }

       private  void  MoveCompleted()
       {
           if (_increment)
               _index++;
           else
               _index--;

           if (_index >= movements.Count - 1)
               _increment = false;
           else if (_index == 0)
               _increment = true;

           _cooldown = new Cooldown(movements[_index].interval);
           _cooldown.StartCooldown();
       }

       private void Move()
        {
            movingObjectTransform.DOMove(movements[_index].TargetPosition,
                movements[_index].timeToAchieveTargetPos).SetEase(Ease.InOutSine);
        }
        
    }
}