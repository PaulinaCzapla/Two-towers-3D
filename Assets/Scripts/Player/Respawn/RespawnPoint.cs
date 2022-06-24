using System;
using UnityEngine;

namespace Player.Respawn
{
    public class RespawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                StaticRespawnEvents.InvokeRespawnPointChange(transform.position);
            }
        }
    }
}