using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Suica.Rendering.Runtime
{
    public class SuicaRenderPipeline : RenderPipeline
    {
        private SuicaRenderer _renderer = new SuicaRenderer();
        
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                _renderer.Render(context, cameras[i]);
            }
        }
    }
}
