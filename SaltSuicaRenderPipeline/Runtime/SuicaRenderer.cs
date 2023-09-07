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

        public void Render(ScriptableRenderContext context, Camera camera)
        {
            // Init render data
            this.context = context;
            this.camera = camera;

            Setup();
            // Do culling
            
            // draw object

            // draw skybox
            context.DrawSkybox(camera);
            
            // submit draw
            Submit();

        }


        #region private method

        private void Cull()
        {
            
        }

        private void DrawGeometry()
        {
            
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


