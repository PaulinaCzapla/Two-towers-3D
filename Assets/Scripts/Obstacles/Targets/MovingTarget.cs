using Obstacles.Traps;
using UnityEngine;

namespace Obstacles.Targets
{
    public class MovingTarget : Target
    {
        [SerializeField] private YoyoMovementBetweenPoints movement;

        private void Update()
        {
            movement.UpdateMovement();
        }
    }
}