using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            UIStaticEvents.SubscribeToSecondPassed(OnUpdateTimer);
        }

        private void OnDisable()
        {
            UIStaticEvents.UnsubscribeFromSecondPassed(OnUpdateTimer);
        }

        private void OnUpdateTimer(int time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            textMesh.text = timeSpan.ToString(@"hh\:mm\:ss");
        }
    }
}