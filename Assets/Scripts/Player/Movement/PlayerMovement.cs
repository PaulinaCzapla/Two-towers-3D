using System;
using Input;
using Player.Movement.Checks;
using Player.Respawn;
using UnityEngine;

namespace Player.Movement
{
    [Serializable]
    public class PlayerMovement : IEventSubscriber
    {
        [Header("Scriptable objects")]
        [SerializeField] private PlayerSO playerParams;
        [SerializeField] private GameplayInputReader inputReader;

        [Header("Components")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private GroundCheck groundCheck;
    
        private Vector2 _movementDirection = Vector3.zero;
        public void SubscribeToEvents()
        {
            inputReader.SetInput();
            inputReader.MoveEvent += OnMove;
            inputReader.MoveCanceledEvent += OnMoveCanceled;
            StaticRespawnEvents.SubscribeToPlayerDied(OnMoveCanceled);
        }

        public void UnsubscribeFromAllEvents()
        {
            inputReader.MoveEvent -= OnMove;
            inputReader.MoveCanceledEvent -= OnMoveCanceled;
            StaticRespawnEvents.UnsubscribeFromPlayerDied(OnMoveCanceled);
        }

        private void OnMoveCanceled()
        {
            _movementDirection = Vector3.zero;
            controller.Move(_movementDirection);
        }

        private void OnMove(Vector2 dir) => _movementDirection = dir;

        public void HandleMoveCharacter()
        {
            if (inputReader.MovePressed)
            {
                Vector3 movement = playerTransform.right * _movementDirection.x +
                                   playerTransform.forward * _movementDirection.y;

                if (!groundCheck.CheckIfOnGround() || inputReader.JumpPressed)
                    movement = movement * Time.deltaTime * playerParams.maxSpeedInAir;
                else
                    movement = movement * Time.deltaTime *
                               (inputReader.SprintPressed ? playerParams.runMaxSpeed : playerParams.walkMaxSpeed);

                controller.Move(movement);
            }
        }
    }
}
