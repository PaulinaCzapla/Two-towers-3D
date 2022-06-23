using System.Collections;
using Input;
using Player.ShootingAbility;
using UnityEngine;

namespace Player.Respawn
{
    public class RespawnController : MonoBehaviour
    {
        [SerializeField] private GameplayInputReader input;
        
        private Vector3 _respawnPos;
        private GameObject _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>().gameObject;
        }

        private void OnEnable()
        {
            StaticRespawnEvents.SubscribeToRespawnPointChange(ChangeRespawnPoint);
            StaticRespawnEvents.SubscribeToPlayerDied(OnPlayerDied);
        }

        private void OnDisable()
        {
            StaticRespawnEvents.UnsubscribeFromRespawnPointChange(ChangeRespawnPoint);
            StaticRespawnEvents.UnsubscribeFromPlayerDied(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            if (_player)
            {
                input.EnableInput(false);
                StartCoroutine(RespawnPlayer());
            }
        }

        private void ChangeRespawnPoint(Vector3 newPoint)
        {
            _respawnPos = newPoint;
        }

        private IEnumerator RespawnPlayer()
        {
            yield return new WaitForSeconds(1f);
            _player.transform.position = _respawnPos;
            input.EnableInput(true);
        }
    }
}