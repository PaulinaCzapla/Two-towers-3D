using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu(fileName = "Gameplay Input Reader", menuName = "Input/GameplayInputReader")]
    public class GameplayInputReader : ScriptableObject, InputActions.IGameplayActions
    {
        public event UnityAction<Vector2> MoveEvent = delegate { };
        public event UnityAction MoveCanceledEvent = delegate { };
        public bool MovePressed { get; set; }
        
        public event UnityAction AimEvent = delegate { };
        public event UnityAction AimCanceledEvent = delegate { };
        public bool AimPressed { get; set; }
        
        public event UnityAction ShootEvent = delegate { };
        public event UnityAction ShootCanceledEvent = delegate { };
        public bool ShootPressed { get; set; }
        
        public event UnityAction JumpEvent = delegate { };
        public event UnityAction JumpCanceledEvent = delegate { };
        public bool JumpPressed { get; set; }
        
        public event UnityAction<Vector2> MousePositionChangedEvent = delegate { };
        public event UnityAction MousePositionChangedCanceledEvent = delegate { };
        public bool SprintPressed { get; set; }

        private InputActions _inputActions;
        private InputActions.IGameplayActions _gameplayActionsImplementation;
        
        
        public void SetInput()
        {
            if (_inputActions == null )
            {
                _inputActions = new InputActions();
                _inputActions.Gameplay.SetCallbacks(this);
                _inputActions.Gameplay.Enable();
            }
        }

        public void EnableInput(bool enable)
        {
            if (enable)
            {
                _inputActions.Gameplay.Enable();
            }
            else
            {
                _inputActions.Gameplay.Disable();
            }
        }
        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MoveEvent.Invoke(context.ReadValue<Vector2>());
                MovePressed = true;
            }
            if (context.canceled)
            {
                MoveCanceledEvent.Invoke();
                MovePressed = false;
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ShootEvent.Invoke();
                ShootPressed = true;
            }
            if (context.canceled)
            {
                ShootCanceledEvent.Invoke();
                ShootPressed = false;
            }
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                AimEvent.Invoke();
                AimPressed = true;
            }
            if (context.canceled)
            {
                AimCanceledEvent.Invoke();
                AimPressed = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpEvent.Invoke();
                JumpPressed = true;
            }
            if (context.canceled)
            {
                JumpCanceledEvent.Invoke();
                JumpPressed = false;
            }
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MousePositionChangedEvent.Invoke(context.ReadValue<Vector2>());
            }

            if (context.canceled)
            {
                MousePositionChangedCanceledEvent.Invoke();
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SprintPressed = true;
            }

            if (context.canceled)
            {
                SprintPressed = false;
            }
        }
        
    }
}
