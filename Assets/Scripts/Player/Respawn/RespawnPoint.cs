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
                Debug.Log(transform.position);
                StaticRespawnEvents.InvokeRespawnPointChange(transform.position);
            }
        }
    }
}