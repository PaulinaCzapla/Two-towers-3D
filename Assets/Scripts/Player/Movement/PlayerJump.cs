using System;
using Input;
using Player.Movement.Checks;
using UI;
using UnityEngine;

namespace Player.Movement
{
    [Serializable]
    public class PlayerJump
    {
        [Header("Scriptable objects")] 
        [SerializeField] private PlayerSO playerParams;
        [SerializeField] private GameplayInputReader inputReader;

        [Header("Components")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private Transform playerTransform;

        private Vector3 _velocity = Vector3.zero;
        private const float Gravity = -9.81f;

        public void HandleJump()
        {
            _velocity.x = controller.velocity.x;
            _velocity.z = controller.velocity.z;

            if (inputReader.JumpPressed && groundCheck.CheckIfOnGround())
            {
                CalculateJumpHeight();
            }

            if (!groundCheck.CheckIfOnGround() && _velocity.y < 3)
            {
                _velocity.y = Mathf.Clamp(
                    _velocity.y - Mathf.Abs(Gravity * Time.deltaTime * (playerParams.downwardMultiplier - 1)),
                    -1 * playerParams.maxDownwardSpeed, 1f);
            }
            else
                _velocity.y += Gravity * Time.deltaTime;

            controller.Move(_velocity * Time.deltaTime);
        }


        private void CalculateJumpHeight()
        {
            UIStaticEvents.InvokePlayerJumped();
            _velocity.y = Mathf.Sqrt(-2f * Gravity * playerParams.jumpMaxHeight);
        }
    }
}