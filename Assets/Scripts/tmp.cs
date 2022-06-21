using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class tmp : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("enter");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger");
        }
    }
}