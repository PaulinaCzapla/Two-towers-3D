using System;
using UnityEditor;
using UnityEngine;

namespace Player.Movement.Checks
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private Vector3 groundCheckPos;
        [Range(0,2)]
        [SerializeField] private float checkRadius;

        public bool CheckIfOnGround()
        {
            return Physics.CheckSphere(transform.position + groundCheckPos, checkRadius, groundLayers);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheckPos+ transform.position, checkRadius);
       }
    }
}