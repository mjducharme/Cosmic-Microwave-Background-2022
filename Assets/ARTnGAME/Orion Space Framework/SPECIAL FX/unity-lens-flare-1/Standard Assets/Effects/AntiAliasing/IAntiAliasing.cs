using UnityEngine;

namespace Artngame.Orion.ImageFX
{
    public interface IAntiAliasing
    {
        void Awake();
        void OnEnable(AntiAliasing owner);
        void OnDisable();
        void OnPreCull(Camera camera);
        void OnPostRender(Camera camera);
        void OnRenderImage(Camera camera, RenderTexture source, RenderTexture destination);
    }
}
