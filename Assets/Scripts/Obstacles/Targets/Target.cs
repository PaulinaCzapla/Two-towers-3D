using System;
using System.Collections;
using DG.Tweening;
using Obstacles.Traps;
using Player.ShootingAbility;
using UnityEngine;

namespace Obstacles.Targets
{
    public class Target : MonoBehaviour, IHitable
    {
        private MovingObject _movingObject;
        
        private void Awake()
        {
            _movingObject = GetComponent<MovingObject>();
        }

        public void Hit()
        {
            ShootingStaticEvents.InvokeTargetHit();
            StartCoroutine(TargetShot());
        }

        private IEnumerator TargetShot()
        {
            if (_movingObject)
                _movingObject.Stop();

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DORotate(transform.rotation. eulerAngles - new Vector3(0f, 0f, -180f), 0.2f)
                    .SetEase(Ease.Flash)).SetLoops(-1);

            yield return new WaitForSeconds(0.5f);
            
            sequence.Pause();
            gameObject.SetActive(false);
        }
    }
}