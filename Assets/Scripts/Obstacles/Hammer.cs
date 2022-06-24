using DG.Tweening;
using UnityEngine;

namespace Obstacles
{
    public class Hammer : MonoBehaviour
    {
        [SerializeField] private float rotationTime;
        [SerializeField] private float pause;
        [SerializeField] private Transform hammer;
        [SerializeField] private Vector3 angle = new Vector3(85f, 0f, 0f);
        
        private Sequence _sequence;

        private void Awake()
        {
            hammer.rotation = Quaternion.Euler(-angle);
            
            _sequence = DOTween.Sequence();
            _sequence.Append(hammer.DORotate(angle,
                    rotationTime).SetEase(Ease.InOutQuart))
                .AppendInterval(pause)
                .Append(hammer.DORotate(- angle,
                    rotationTime).SetEase(Ease.InOutQuart))
                .AppendInterval(pause)
                .SetLoops(-1);
        }
    }
}