using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public class PlayerDetector : MonoBehaviour
    {
        public UnityAction OnPlayerEnterAction { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<PlayerController>())
                OnPlayerEnterAction.Invoke();
        }
    }
}