using UnityEngine;

namespace Obstacles.Traps
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private YoyoMovementBetweenPoints movement;
        
        private void Awake()
        {
            movement.StartMovement();
        }

        private void Update()
        {
            movement.UpdateMovement();
        }
    }
}