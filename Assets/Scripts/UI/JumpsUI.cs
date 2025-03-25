using TMPro;
using UnityEngine;

namespace UI
{
    public class JumpsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        
        private int _jumps = 0;
        
        private void OnEnable()
        {
            textMesh.text = "0";
            UIStaticEvents.SubscribeToPlayerJumped(OnPlayerJumped);
        }

        private void OnDisable()
        {
            UIStaticEvents.UnsubscribeFromPlayerJumped(OnPlayerJumped);
        }

        private void OnPlayerJumped()
        {
            _jumps++;
            textMesh.text = _jumps.ToString();
        }
    }
}