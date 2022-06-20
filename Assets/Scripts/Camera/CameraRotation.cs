using System;
using Input;
using Player;
using UnityEngine;

namespace Camera
{
    [Serializable]
    public class CameraRotation : IEventSubscriber
    {
        [Header("Scriptable objects")]
        [SerializeField] private GameplayInputReader inputReader;
        [SerializeField] private PlayerSO playerParams;

        [Header("Components")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform camTransform;
        
        private Vector2 _mouseAxis = Vector2.zero;
        private float _xRotation = 0f;
        
        public void SubscribeToEvents()
        {
            inputReader.MousePositionChangedEvent += MousePositionChanged;
            inputReader.MousePositionChangedCanceledEvent += ResetMouseAxis;
        }
        
         public void UnsubscribeFromAllEvents()
        {
            inputReader.MousePositionChangedEvent -= MousePositionChanged;
            inputReader.MousePositionChangedCanceledEvent -= ResetMouseAxis;
        }

        public void HandleCameraRotation()
        {
            _xRotation -= _mouseAxis.y * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, playerParams.minAngle, playerParams.maxAngle);
            playerTransform.Rotate(Vector3.up * _mouseAxis.x * Time.deltaTime);
            camTransform.localRotation = Quaternion.Euler(_xRotation,0f,0f);
        }
        
        private void ResetMouseAxis() => _mouseAxis = Vector2.zero;

        private void MousePositionChanged(Vector2 dir)
        {
            _mouseAxis.x = dir.x * playerParams.mouseSensitivity ;
            _mouseAxis.y = dir.y * playerParams.mouseSensitivity;
        }
    }
}