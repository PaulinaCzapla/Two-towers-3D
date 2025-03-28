﻿using System;
using System.Collections;
using Obstacles.Targets;
using Timers;
using UnityEngine;

namespace Player.ShootingAbility
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IShotable
    {
        private const float MaxLifespan = 10f;
        
        [SerializeField] private float bulletForce;
        [Tooltip("Bullet lifespan after collision")]
        [SerializeField] private float bulletLifespan;
        
        private Rigidbody _rb;
        private bool _isAfterCollision = false;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            StartCoroutine(BulletLifespan(MaxLifespan));
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _isAfterCollision = false;
            _rb.velocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision other)
        {
            OnHitObject(other.collider);
        }

        private void OnHitObject(Collider col)
        {
            if (!_isAfterCollision)
                StartCoroutine(BulletLifespan(bulletLifespan));
            if (col.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
                hitable.Hit();
        }

        private IEnumerator BulletLifespan(float time)
        {
            yield return new WaitForSeconds(time);
            _isAfterCollision = true;
            gameObject.SetActive(false);
        }
        
        public void Shoot(Vector3 direction)
        {
            _rb.AddForce(direction.normalized * bulletForce, ForceMode.Impulse);
        }
    }
}