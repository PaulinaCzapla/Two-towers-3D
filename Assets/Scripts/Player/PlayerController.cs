using Obstacles;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDieable
    {
        public void Die()
        {
            Debug.Log("dead");
        }
    }
}