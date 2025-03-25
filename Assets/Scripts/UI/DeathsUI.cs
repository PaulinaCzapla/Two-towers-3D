using Obstacles.Targets;
using Player.Respawn;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DeathsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;

        private int _deaths = 0;
        
        private void OnEnable()
        {
            textMesh.text = "0";
            StaticRespawnEvents.SubscribeToPlayerDied(OnPlayerDied);
        }

        private void OnDisable()
        {
            StaticRespawnEvents.UnsubscribeFromPlayerDied(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            _deaths++;
            textMesh.text = _deaths.ToString();
        }
    }
}