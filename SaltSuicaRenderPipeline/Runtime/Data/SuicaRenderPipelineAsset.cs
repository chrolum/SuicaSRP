using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Suica.Rendering.Runtime
{
    [CreateAssetMenu(menuName = "Rendering/Suica Render Pipeline")]
    public class SuicaRenderPipelineAsset : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new SuicaRenderPipeline();
        }
    }
}
