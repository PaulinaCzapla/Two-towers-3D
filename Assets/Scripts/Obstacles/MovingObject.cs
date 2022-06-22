using UnityEngine;

namespace Obstacles
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