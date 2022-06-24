using UnityEngine;

namespace Obstacles.Traps
{
    public class CrushingTrap : MonoBehaviour
    {
        private const float Margin = 0.4f;
        
        [SerializeField] private Transform trapTransform;
        [SerializeField] private KillingObject killingObject;

        private Vector3 _initialPosition;
        
        private void Awake()
        {
            _initialPosition = trapTransform.position;
            killingObject.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Vector3.Distance(trapTransform.position, _initialPosition) <= Margin)
            {
                killingObject.gameObject.SetActive(true);
            }
            else if(killingObject.isActiveAndEnabled)
            {
                killingObject.gameObject.SetActive(false);
            }
        }
    }
}