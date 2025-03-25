using System;
using Input;
using Player;
using UnityEngine;

namespace Camera
{
    [Serializable]
    public class CameraZoom 
    {
        [Header("Scriptable objects")]
        [SerializeField] private GameplayInputReader inputReader;
        [SerializeField] private PlayerSO playerParams;

        [Header("Components")]
        [SerializeField] private UnityEngine.Camera cam;

        public void HandleCameraZoom()
        {
            LerpFieldOfView((inputReader.AimPressed ? playerParams.maxZoom : playerParams.initialZoom),
                (inputReader.AimPressed ? playerParams.zoomInSpeed: playerParams.zoomOutSpeed));
        }

        public void LerpFieldOfView(float targetValue, float t)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetValue, t * Time.deltaTime);
                
            if (Mathf.Abs(cam.fieldOfView - targetValue) < 0.1)
                cam.fieldOfView = targetValue;
        }

    }
}