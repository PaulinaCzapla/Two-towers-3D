using System;
using System.Threading;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Timers
{
    public class TimeCounter : MonoBehaviour
    {
        public float TimeElapsed => Time.time - _startTime;
        
        private float _startTime;
        private Cooldown _cooldown;
        private int _secondsPassed = 0;
        
        public void StartTimer()
        {
            _startTime = Time.time;
            
            
        }

        private void Awake()
        {
            _cooldown = new Cooldown(1);
            _cooldown.StartCooldown();
        }

        private void Update()
        {
            if (_cooldown.CooldownEnded)
            {
                UIStaticEvents.InvokeSecondPassed(_secondsPassed++);
                _cooldown.StartCooldown();
            }
        }
    }
}