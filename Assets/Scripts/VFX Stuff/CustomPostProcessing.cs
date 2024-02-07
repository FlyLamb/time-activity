namespace TimeActivityFx {

    using UnityEngine.Rendering.Universal;
    using System;

    [Serializable]
    public class CustomPostProcessRenderer : ScriptableRendererFeature {
        private CustomPostProcessPass m_pass;

        public override void Create() {
            m_pass = new CustomPostProcessPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
            renderer.EnqueuePass(m_pass);
        }
    }

}