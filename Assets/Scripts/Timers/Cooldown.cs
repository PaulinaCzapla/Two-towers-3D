using System;
using UnityEngine;

namespace Timers
{
    public class Cooldown
    {
        public bool CooldownEnded => IsCooldownEnded();

        private readonly float _cooldownTime;
        private float _nextFireTime;

        public Cooldown(float cooldownTime)
        {
            _cooldownTime = cooldownTime;
        }

        public void StartCooldown()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            Console.WriteLine((int)t.TotalSeconds);
            _nextFireTime = Time.time + _cooldownTime;
        }

        private bool IsCooldownEnded()
        {
            return Time.time > _nextFireTime;
        }
    }
}