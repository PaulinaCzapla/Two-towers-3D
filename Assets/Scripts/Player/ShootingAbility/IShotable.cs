using UnityEngine;

namespace Player.ShootingAbility
{
    public interface IShotable
    {
        public void Shoot(Vector3 direction);
    }
}