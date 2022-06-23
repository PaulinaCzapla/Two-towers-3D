using System;
using System.Collections;
using Buttons.Interfaces;
using Player;
using UnityEditor;
using UnityEngine;

namespace Buttons
{
    public class ReturnButton : MonoBehaviour, IClickable, ITargetable
    {
        [SerializeField] private Material initialMaterial;
        [SerializeField] private Material targetedMaterial;
        [SerializeField] private Material clickedMaterial;
        [SerializeField] private MeshRenderer meshRenderer;

        private string _guid;
        
        private const float ButtonClickMove = 0.2f;
        private readonly Vector3 _pos = new Vector3(10f, 2f, -3f);

        private void Awake()
        {
            _guid = GUID.Generate().ToString();
        }

        private void OnEnable()
        {
            StaticButtonEvents.SubscribeToButtonClicked(OnElevatorButtonClicked);
            StaticButtonEvents.SubscribeToButtonTargeted(OnElevatorButtonTargeted);
        }
        
        private void OnDisable()
        {
            StaticButtonEvents.UnsubscribeFromButtonClicked(OnElevatorButtonClicked);
            StaticButtonEvents.UnsubscribeFromButtonTargeted(OnElevatorButtonTargeted);
        }

        public void Clicked()
        {
            StaticButtonEvents.InvokeButtonClicked(_guid);
            meshRenderer.material = clickedMaterial;
            transform.localScale -= new Vector3(0, ButtonClickMove, 0);
            StartCoroutine(ButtonClickedTimer());
        }

        public void Targeted()
        {
            StaticButtonEvents.InvokeButtonTargeted(_guid);
            meshRenderer.material = targetedMaterial;
        }

        public void NotTargeted()
        {
            meshRenderer.material = initialMaterial;
        }

        private void OnElevatorButtonTargeted(string guid)
        {
            if (!guid.Equals(_guid))
            {
                meshRenderer.material = initialMaterial;
            }
        }

        private void OnElevatorButtonClicked(string guid)
        {
            if (!guid.Equals(_guid))
            {
                meshRenderer.material = initialMaterial;
            }
        }

        private IEnumerator ButtonClickedTimer()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject player = FindObjectOfType<PlayerController>().gameObject;

            if (player)
            {
                player.transform.position = _pos;
            }
        }

    }
}