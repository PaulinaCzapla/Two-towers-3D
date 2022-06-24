using System.Collections;
using DG.Tweening;
using Input;
using Player.ShootingAbility;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Respawn
{
    public class RespawnController : MonoBehaviour
    {
        [SerializeField] private GameplayInputReader input;
        [SerializeField] private Image fadeImage;
        
        private Vector3 _respawnPos;
        private GameObject _player;

        private void Start()
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
            if(_player == null)
                _player = FindObjectOfType<PlayerController>().gameObject;
            
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
            fadeImage.DOFade(1, 1);
            yield return new WaitForSeconds(1f);
            _player.transform.position = _respawnPos;
            input.EnableInput(true);
            fadeImage.DOFade(0, 0.8f);
        }
    }
}