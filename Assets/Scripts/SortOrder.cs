    using UnityEngine;
     
    public class SortOrder : MonoBehaviour
    {
        public int sortPriority = -10; // Or whatever HDRP sortPriority you want to set it to.
        private Renderer VfxRenderer = null;
     
        private void Awake()
        {
            VfxRenderer = GetComponent<Renderer>();
        }
     
        private void OnValidate()
        {
            //if (VfxRenderer)
            //    VfxRenderer.sharedMaterial.renderQueue = 3000 + sortPriority;
            //    VfxRenderer.sharedMaterial.SetFloat("_TransparentSortPriority", sortPriority);
        }
    }
