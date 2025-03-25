using System;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public struct LevelTrigger
    {
        public GameObject level;
        public PlayerDetector detector;
    }
}