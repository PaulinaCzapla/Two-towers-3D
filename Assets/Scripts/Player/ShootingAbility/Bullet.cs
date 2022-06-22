using System;
using System.Collections;
using Obstacles.Targets;
using UnityEngine;

namespace Player.ShootingAbility
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IShotable
    {
        [SerializeField] private float bulletForce;
        [Tooltip("Bullet lifespan after collision")]
        [SerializeField] private float bulletLifespan;
        
        private Rigidbody _rb;
        private bool _isAfterCollision = false;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            OnHitObject(other.collider);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHitObject(other);
        }

        private void OnHitObject(Collider col)
        {
            if (!_isAfterCollision)
                StartCoroutine(BulletLifespan());
            if (col.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
                hitable.Hit();
        }

        private IEnumerator BulletLifespan()
        {
            _isAfterCollision = true;
            yield return new WaitForSeconds(bulletLifespan);
            gameObject.SetActive(false);
        }
        
        public void Shoot(Vector3 direction)
        {
            _rb.AddForce(direction.normalized * bulletForce, ForceMode.Impulse);
        }
    }
}