using System;
using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.Events;

namespace Obstacles.Targets
{
    public class ShootingRange : MonoBehaviour
    {
        [SerializeField] private List<ShootingRound> rounds;
        [SerializeField] private Door door;
        [SerializeField] private PlayerDetector playerDetector;

        private int _currentRound;

        private void OnEnable()
        {
            door.OpenDoor();
            playerDetector.OnPlayerEnterAction += OnPlayerEntered;
        }

        private void OnDisable()
        {
            playerDetector.OnPlayerEnterAction -= OnPlayerEntered;
            ShootingStaticEvents.UnsubscribeFromTargetHit(TargetHit);
        }

        private void TargetHit(int sumOfHit)
        {
            foreach (var target in rounds[_currentRound].shootingTargets)
            {
                if (target.gameObject.activeSelf)
                    return;
            }

            _currentRound++;
            StartRound(_currentRound);
        }

        private void OnPlayerEntered()
        {
            door.CloseDoor();
            ShootingStaticEvents.SubscribeToTargetHit(TargetHit);
            playerDetector.OnPlayerEnterAction -= OnPlayerEntered;
            StartRound(0);
        }

        private void StartRound(int round)
        {
            if (round < rounds.Count)
            {
                foreach (var target in rounds[_currentRound].shootingTargets)
                {
                    target.gameObject.SetActive(true);
                }
            }
        }
    }
}