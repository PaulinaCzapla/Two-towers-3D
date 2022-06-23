using Obstacles;
using Player.Respawn;
using Player.ShootingAbility;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDieable
    {
        public void Die()
        {
            StaticRespawnEvents.InvokePlayerDied();
        }
    }
}