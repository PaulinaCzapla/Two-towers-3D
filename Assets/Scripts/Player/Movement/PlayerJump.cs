using System;
using System.Threading;
using Input;
using Player.Movement.Checks;
using UI;
using UnityEngine;

namespace Player.Movement
{
    [Serializable]
    public class PlayerJump
    {
        private const float LateJumpDelay = 0.2f;
        
        [Header("Scriptable objects")] 
        [SerializeField] private PlayerSO playerParams;
        [SerializeField] private GameplayInputReader inputReader;

        [Header("Components")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private GroundCheck groundCheck;

        private Vector3 _velocity = Vector3.zero;
        private const float Gravity = -9.81f;
        private bool _justJumped = false;
        private float _lastOnGroundTime;

        public void HandleJump()
        {
            _velocity.x = controller.velocity.x;
            _velocity.z = controller.velocity.z;

            if ( _lastOnGroundTime != 0 && groundCheck.CheckIfOnGround())
            {
                _justJumped = false;
                _lastOnGroundTime = 0;
            }

            if (!groundCheck.CheckIfOnGround())
            {
                _lastOnGroundTime += Time.deltaTime;
            }

            if (inputReader.JumpPressed && (groundCheck.CheckIfOnGround() || (_lastOnGroundTime<=LateJumpDelay && !_justJumped)))
            {
                CalculateJumpHeight();
            }

            if (!groundCheck.CheckIfOnGround() && _velocity.y < 4)
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
            _justJumped = true;
            UIStaticEvents.InvokePlayerJumped();
            _velocity.y = Mathf.Sqrt(-2f * Gravity * playerParams.jumpMaxHeight);
        }
    }
}