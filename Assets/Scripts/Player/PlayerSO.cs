using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "MovementParameters", menuName = "RedDeerProject/MovementParameters")]
    public class PlayerSO : ScriptableObject
    {
        [Header("Jump")]
        public float jumpMaxHeight;
        public float maxDownwardSpeed;
        public float jumpMultiplier;
        public float downwardMultiplier;
        
        [Header("Run")]
        public float walkMaxSpeed;
        public float maxSpeedInAir;
        public float runMaxSpeed;

        [Header("Shooting ability")] 
        public float shootCooldown;
        public float range;
        public float spreadMaxValue;

        [Header("Camera")] 
        public float mouseSensitivity;
        public float maxAngle;
        public float minAngle;
        public float maxZoom;
        public float initialZoom;
        public float zoomInSpeed;
        public float zoomOutSpeed;
    }
}