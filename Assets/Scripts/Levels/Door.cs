using UnityEngine;

namespace Levels
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject doorObj;

        public void CloseDoor()
        {
            doorObj.SetActive(true);
        }
        
        public void OpenDoor()
        {
            doorObj.SetActive(false);
        }
    }
}