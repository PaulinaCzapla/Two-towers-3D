using System.Collections;
using Buttons.Interfaces;
using Elevator;
using UnityEngine;

namespace Buttons
{
    public class ElevatorButton : Button , IClickable, ITargetable
    {
        [Range(0, 3)] [SerializeField] private int floorNum;
        
        private const float ButtonClickMove = 0.2f;
        private void OnEnable()
        {
            StaticElevatorEvents.SubscribeToElevatorButtonClicked(OnElevatorButtonClicked);
            StaticElevatorEvents.SubscribeToElevatorButtonTargeted(OnElevatorButtonTargeted);
        }
        
        private void OnDisable()
        {
            StaticElevatorEvents.UnsubscribeFromElevatorButtonClicked(OnElevatorButtonClicked);
            StaticElevatorEvents.UnsubscribeFromElevatorButtonTargeted(OnElevatorButtonTargeted);
        }

        public void Clicked()
        {
            StaticElevatorEvents.InvokeElevatorButtonClicked(floorNum);
            meshRenderer.material = clickedMaterial;
            transform.localScale -= new Vector3(0, ButtonClickMove, 0);
            StartCoroutine(ButtonClickedTimer());
        }

        public void Targeted()
        {
            StaticElevatorEvents.InvokeElevatorButtonTargeted(floorNum);
            meshRenderer.material = targetedMaterial;
        }

        public void NotTargeted()
        {
            meshRenderer.material = initialMaterial;
        }

        private void OnElevatorButtonTargeted(int floor)
        {
            if (floor != floorNum)
            {
                meshRenderer.material = initialMaterial;
            }
        }

        private void OnElevatorButtonClicked(int floor)
        {
            if (floor != floorNum)
            {
                meshRenderer.material = initialMaterial;
            }
        }
    }
}