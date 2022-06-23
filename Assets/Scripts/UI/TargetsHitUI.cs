using System;
using Obstacles.Targets;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TargetsHitUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;

        private int _targetsHit = 0;
        
        private void OnEnable()
        {
            textMesh.text = "0";
            ShootingStaticEvents.SubscribeToTargetHit(OnTargetHit);
        }

        private void OnDisable()
        {
            ShootingStaticEvents.UnsubscribeFromTargetHit(OnTargetHit);
        }

        private void OnTargetHit()
        {
            _targetsHit++;
            textMesh.text = _targetsHit.ToString();
        }
    }
}