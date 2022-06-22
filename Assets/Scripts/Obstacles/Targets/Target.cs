using System;
using Player.ShootingAbility;
using UnityEngine;

namespace Obstacles.Targets
{
    public class Target : MonoBehaviour, IHitable
    {
        public void Hit()
        {
            ShootingStaticEvents.InvokeTargetHit();
            gameObject.SetActive(false);
        }
    }
}