using System;
using System.Collections.Generic;
using Input;
using Timers;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace Player.ShootingAbility
{
    public class Shooter : MonoBehaviour
    { 
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private PlayerSO player;
        [SerializeField] private GameplayInputReader input;
        [SerializeField] private Transform firePoint;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private UnityEngine.Camera cam;
        
        private List<GameObject> _bulletsPool = new List<GameObject>();
        private Cooldown _cooldown;
        private Vector3 _bulletDir;
        private Vector3 t;

        private void Awake()
        {
            _cooldown = new Cooldown(0);
            _cooldown.StartCooldown();
        }

        private void FixedUpdate()
        {
            if (input.ShootPressed)
            {
                if (_cooldown!=null && _cooldown.CooldownEnded)
                {
                    _cooldown = new Cooldown(player.shootCooldown);
                    _cooldown.StartCooldown();
                    
                    SetShotDirection();
                    SpawnShotable();
                }
            }
            else
            {
                
            }
        }

        private void SetShotDirection()
        {
            Vector3 targetPoint;
            RaycastHit hit;

            var cameraCenter = cam.transform.position;
            
            if (Physics.Raycast(cameraCenter, cam.gameObject.transform.forward, out hit,
                Mathf.Infinity, layerMask))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = cam.gameObject.transform.forward*20 + cameraCenter;
            }
            
            _bulletDir = targetPoint - firePoint.position;
            AddSpread();

            Debug.DrawRay(firePoint.position, _bulletDir, Color.red,20);
            Debug.DrawRay(cameraCenter, _bulletDir, Color.green,20);
        }

        private void AddSpread()
        {
            float x = Random.Range(-player.spreadMaxValue, player.spreadMaxValue);
            float y = Random.Range(-player.spreadMaxValue, player.spreadMaxValue);

            _bulletDir += new Vector3(x, y, 0);
        }
        
        private void SpawnShotable()
        {
            GameObject bullet = null;

            foreach (GameObject bulletInPool in _bulletsPool)
            {
                if (!bulletInPool.activeSelf)
                {
                    bullet = bulletInPool.gameObject;
                    break;
                }
            }

            if (bullet == null)
            {
                bullet = Instantiate(bulletPrefab);
                _bulletsPool.Add(bullet);
            }

            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);
            
            if (bullet.TryGetComponent(out IShotable bulletInstantiated))
            {
                bulletInstantiated.Shoot(_bulletDir);
            }
        }
    }
}