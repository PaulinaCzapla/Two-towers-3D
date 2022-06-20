using System;
using UnityEngine;

namespace Obstacles
{
    [Serializable]
    public class PlayerInsideChecker
    {
        [Header("Player detection")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Vector3 checkPosCorrection = Vector3.zero;
        [SerializeField] private Vector3 halfExtends;
        [SerializeField] private Transform objTransform;

        public GameObject CheckIfPlayerInside()
        {
            Collider[] hitColliders = Physics.OverlapBox(objTransform.transform.position, halfExtends,
                Quaternion.identity, playerLayer);

            if (hitColliders.Length > 0)
            {
                return hitColliders[0].gameObject;
            }
            return null;
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(checkPosCorrection+ objTransform.position, halfExtends);
        }

    }
}