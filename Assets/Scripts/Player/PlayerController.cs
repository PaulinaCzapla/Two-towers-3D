using Player.Movement;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement = new PlayerMovement();
        [SerializeField] private PlayerJump jump = new PlayerJump();
        private void OnEnable()
        {
            movement.SubscribeToEvents();
        }

        private void OnDisable()
        {
            movement.UnsubscribeFromAllEvents();
        }

        private void Update()
        {
            movement.HandleMoveCharacter();
            jump.HandleJump();
        }
    }
}