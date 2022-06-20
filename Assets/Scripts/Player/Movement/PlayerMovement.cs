using System;
using Input;
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
    
        private Vector2 _movementDirection = Vector3.zero;
        public void SubscribeToEvents()
        {
            inputReader.SetInput();
            inputReader.MoveEvent += OnMove;
            inputReader.MoveCanceledEvent += OnMoveCanceled;
        }

        public void UnsubscribeFromAllEvents()
        {
            inputReader.MoveEvent -= OnMove;
            inputReader.MoveCanceledEvent -= OnMoveCanceled;
        }

        private void OnMoveCanceled() => _movementDirection = Vector3.zero;

        private void OnMove(Vector2 dir) => _movementDirection = dir;

        public void HandleMoveCharacter()
        {
            Vector3 movement = playerTransform.right * _movementDirection.x +
                               playerTransform.forward * _movementDirection.y;
            controller.Move(movement * Time.deltaTime * 
                            (inputReader.SprintPressed ? playerParams.runMaxSpeed : playerParams.walkMaxSpeed));
        }
    }
}
