using System;
using System.Collections;
using DG.Tweening;
using Input;
using Timers;
using UnityEngine;

namespace Obstacles.Platforms
{
    public class FallingPlatform : MonoBehaviour
    {
        private const float ShakeStrength = 0.06f;
        private const float ShakeDuration = 0.2f;
        
        [Header("Platform params")]
        [SerializeField] private float timeBeforeFall = 1;
        [SerializeField] private float timeBeforeRespawn = 3;
        
        [Header("Components")]
        [SerializeField] private PlayerInsideChecker checker;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider platformCollider;
        [SerializeField] private GameObject mesh;

        private Cooldown _fallCooldown;
        private Cooldown _shakeCooldown;
        private Cooldown _restoreCooldown;
        private Vector2 _initialPosition;
        private bool _wasTriggered;
        private UnityEngine.Camera _cam;

        private void Awake()
        {
            _initialPosition = transform.position;
            _cam = UnityEngine.Camera.main;
        }

        private void FixedUpdate()
        {
            if (checker.CheckIfPlayerInside())
                OnPlayerOnPlatform();
                
            if (_shakeCooldown != null && _shakeCooldown.CooldownEnded && _cam)
            {
                _cam.DOShakePosition(ShakeDuration, ShakeStrength);
            }

            if (_fallCooldown != null && _fallCooldown.CooldownEnded)
            {
                _fallCooldown = null;
                StartCoroutine(Fall());
            }

            if (_restoreCooldown != null && _restoreCooldown.CooldownEnded)
            {
                _restoreCooldown = null;
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                platformCollider.isTrigger = false;
                mesh.SetActive(true);
                transform.position = _initialPosition;
                _wasTriggered = false;
            }
        }

        private void OnPlayerOnPlatform()
        {
            if (!_wasTriggered)
            {
                _fallCooldown = new Cooldown(timeBeforeFall);
                _fallCooldown.StartCooldown();
                _shakeCooldown = new Cooldown(timeBeforeFall / 2);
                _shakeCooldown.StartCooldown();
                _wasTriggered = true;
            }
        }

        private IEnumerator Fall()
        {
            rb.isKinematic = false;
            platformCollider.isTrigger = true;
            rb.velocity = new Vector3(0, -6, 0);
            yield return new WaitForSeconds(0.2f);

            _shakeCooldown = null;
            mesh.SetActive(false);
            _restoreCooldown = new Cooldown(timeBeforeRespawn);
            _restoreCooldown.StartCooldown();
        }

        private void OnDrawGizmos()
        {
            checker.OnDrawGizmos();
        }
    }
}