using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

namespace Suica.Rendering.Runtime
{
    public class SuicaRenderer
    {
        ScriptableRenderContext context;
        Camera camera;

        private const string rendererBufferName = "Suica Renderer";
        CommandBuffer cmd = new CommandBuffer {name = rendererBufferName};
        
        CullingResults cullingResults;

        public void Render(ScriptableRenderContext context, Camera camera)
        {
            // Init render data
            this.context = context;
            this.camera = camera;

            Setup();
            // Do culling
            Cull();
            // draw object
            DrawGeometry();

            // draw skybox
            context.DrawSkybox(camera);
            
            // submit draw
            Submit();

        }


        #region private method

        private bool Cull()
        {
            if (camera.TryGetCullingParameters(out ScriptableCullingParameters cullingParameters))
            {
                cullingResults =  context.Cull(ref cullingParameters);
                return true;
            }
            return false;
        }

        private void DrawGeometry()
        {
            // Setup drawing setting and filteringSetting
            var sortingSettings = new SortingSettings(camera);
            var drawingSettings = new DrawingSettings(new ShaderTagId("SuicaForward"), sortingSettings);
            // drawingSettings.SetShaderPassName(1, new ShaderTagId("SuicaForwardPlus"));
            var opaqueFilteringSettings = new FilteringSettings(RenderQueueRange.opaque);
            var transparentFilteringSettings = new FilteringSettings(RenderQueueRange.transparent);
            
            context.DrawRenderers(cullingResults, ref drawingSettings, ref opaqueFilteringSettings);
            
            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings;
            context.DrawRenderers(cullingResults, ref drawingSettings, ref transparentFilteringSettings);
        }
        
        #endregion
        private void Setup()
        {
            cmd.BeginSample(rendererBufferName);
            context.SetupCameraProperties(camera);
            cmd.ClearRenderTarget(true, true, Color.clear);
            ExecuteCommandBuffer();
        }
        
        private void Submit()
        {
            context.Submit();
            ExecuteCommandBuffer();
            cmd.EndSample(rendererBufferName);
        }
        
        private void ExecuteCommandBuffer()
        {
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
        }
    }
}


