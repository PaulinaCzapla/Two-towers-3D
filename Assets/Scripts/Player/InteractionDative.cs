using System;
using Buttons.Interfaces;
using Input;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace Player
{
    public class InteractionDative : MonoBehaviour
    {
        [SerializeField] private GameplayInputReader input;

        private IClickable _currentlyTargetedClickable = null;
        private ITargetable _lastTargetable = null;

        private void OnEnable()
        {
            input.ShootEvent += OnClicked;
        }

        private void OnDisable()
        {
            input.ShootEvent -= OnClicked;
        }
        
        private void Update()
        {
            CheckIfTargeting();
        }

        private void OnClicked()
        {
            if (_currentlyTargetedClickable != null)
            {
                _currentlyTargetedClickable.Clicked();
            }
        }

        private void CheckIfTargeting()
        {
            var cameraCenter = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f,
                Screen.height / 2f, UnityEngine.Camera.main.nearClipPlane));
            RaycastHit hit;
            
            Debug.DrawRay(cameraCenter, UnityEngine.Camera.main.gameObject.transform.forward, Color.magenta);

            if (Physics.Raycast(cameraCenter, UnityEngine.Camera.main.gameObject.transform.forward, out hit,
                Mathf.Infinity))
            {
                if (hit.transform.gameObject.TryGetComponent<ITargetable>(out var targetable))
                {
                    _lastTargetable = targetable;
                    targetable.Targeted();

                    if (hit.transform.gameObject.TryGetComponent<IClickable>(out var clickable))
                        _currentlyTargetedClickable = clickable;

                    return;
                }

                if (_lastTargetable != null)
                    _lastTargetable.NotTargeted();
                
                _lastTargetable = null;
                _currentlyTargetedClickable = null;
            }
        }
    }
}