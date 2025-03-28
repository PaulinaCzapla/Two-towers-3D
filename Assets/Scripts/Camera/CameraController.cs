﻿using System;
using Player;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraRotation cameraRotation = new CameraRotation();
        [SerializeField] private CameraZoom cameraZoom = new CameraZoom();
        [SerializeField] private InteractionDative interactionDative;
        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            cameraRotation.SubscribeToEvents();
        }

        private void OnDisable()
        {
            cameraRotation.UnsubscribeFromAllEvents();
        }

        private void Update()
        {
            cameraRotation.HandleCameraRotation();

            if (!interactionDative.IsCurrentlyTargetingClickable)
                cameraZoom.HandleCameraZoom();
        }
    }
}