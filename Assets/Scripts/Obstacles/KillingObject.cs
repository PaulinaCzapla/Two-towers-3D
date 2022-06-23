using Player;
using Player.Respawn;
using UnityEngine;

namespace Obstacles
{
    public class KillingObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDieable dieable))
            {
                dieable.Die();
            }
        }
    }
}