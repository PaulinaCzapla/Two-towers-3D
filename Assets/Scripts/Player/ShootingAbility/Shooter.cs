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
        
        private List<GameObject> _bulletsPool = new List<GameObject>();
        private Cooldown _cooldown;
        private Vector3 _bulletDir;

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
            var cameraCenter = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f,
                Screen.height / 2f, UnityEngine.Camera.main.nearClipPlane));
            
            if (Physics.Raycast(cameraCenter, UnityEngine.Camera.main.gameObject.transform.forward, out hit,
                Mathf.Infinity))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = UnityEngine.Camera.main.gameObject.transform.forward*10 + cameraCenter;
            }
            
            _bulletDir = targetPoint - firePoint.position;
            AddSpread();
            
            Debug.DrawRay(firePoint.position, targetPoint, Color.green);
            Debug.DrawRay(cameraCenter, targetPoint, Color.green);
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