using System.Collections;
using Input;
using Timers;
using UnityEngine;

namespace Obstacles.Platforms
{
    public class FallingPlatform : MonoBehaviour
    {
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

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (_shakeCooldown != null && _shakeCooldown.CooldownEnded)
            {
                //todo: camera shake
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
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("collision");
            if (other.gameObject.layer.ToString().Equals("Player"))
            {
                if (other.contacts[0].normal.y >= -1 && other.contacts[0].normal.y < -0.5f)
                {
                    _fallCooldown = new Cooldown(timeBeforeFall);
                    _fallCooldown.StartCooldown();
                    _shakeCooldown = new Cooldown(timeBeforeFall / 2);
                    _shakeCooldown.StartCooldown();
                }
            }
        }

        private IEnumerator Fall()
        {
            rb.isKinematic = false;
            platformCollider.isTrigger = true;
            yield return new WaitForSeconds(0.25f);

            _shakeCooldown = null;
            mesh.SetActive(false);
            _restoreCooldown = new Cooldown(timeBeforeRespawn);
            _restoreCooldown.StartCooldown();
        }
    }
}