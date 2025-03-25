using System.Collections;
using UnityEngine;

namespace Buttons
{
    public abstract class Button : MonoBehaviour
    {
        [SerializeField] protected Material initialMaterial;
        [SerializeField] protected Material targetedMaterial;
        [SerializeField] protected Material clickedMaterial;
        [SerializeField] protected MeshRenderer meshRenderer;
        
        private const float ButtonClickMove = 0.2f;
        
        protected IEnumerator ButtonClickedTimer()
        {
            yield return new WaitForSeconds(0.5f);
            meshRenderer.material = initialMaterial;
            transform.localScale += new Vector3(0, ButtonClickMove, 0);
        }
    }
}