using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private List<LevelTrigger> levels;

        private void Awake()
        {
            foreach (var level in levels)
            {
                level.detector.OnPlayerEnterAction += () => OnPlayerTriggeredLevel(level.level);
            }
        }

        private void OnDisable()
        {
            foreach (var level in levels)
            {
                level.detector.OnPlayerEnterAction = null;
            }
        }

        private void OnPlayerTriggeredLevel(GameObject levelObj)
        {
            levelObj.gameObject.SetActive(true);

            foreach (var level  in levels)
            {
                if(level.level == levelObj)
                    continue;
                
                level.level.SetActive(false);
            }
        }
    }
}