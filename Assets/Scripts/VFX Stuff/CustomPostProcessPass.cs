namespace TimeActivityFx {

    using System;
    using System.Linq;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UnityEngine.Rendering.Universal;

    [Serializable]
    public class CustomPostProcessPass : ScriptableRenderPass {
        private RenderTargetIdentifier m_source;
        private RenderTargetIdentifier m_destinationA;
        private RenderTargetIdentifier m_destinationB;
        private RenderTargetIdentifier m_latestDest;

        private readonly int m_temporaryRTIdA = Shader.PropertyToID("_TempRT");
        private readonly int m_temporaryRTIdB = Shader.PropertyToID("_TempRTB");

        private Material m_playerCrtMaterial;

        public CustomPostProcessPass() {
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData) {
            // Grab the camera target descriptor. We will use this when creating a temporary render texture.
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = 0;

            var renderer = renderingData.cameraData.renderer;
            m_source = renderer.cameraColorTarget;

            SetupMaterials();

            // Create a temporary render texture using the descriptor from above.
            cmd.GetTemporaryRT(m_temporaryRTIdA, descriptor, FilterMode.Bilinear);
            m_destinationA = new RenderTargetIdentifier(m_temporaryRTIdA);
            cmd.GetTemporaryRT(m_temporaryRTIdB, descriptor, FilterMode.Bilinear);
            m_destinationB = new RenderTargetIdentifier(m_temporaryRTIdB);
        }

        private void SetupMaterials() {
            m_playerCrtMaterial = new Material(Shader.Find("Shader Graphs/Player CRT"));
        }

        // The actual execution of the pass. This is where custom rendering occurs.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
            if (renderingData.cameraData.isSceneViewCamera)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("Custom Post Processing");
            cmd.Clear();

            // This holds all the current Volumes information
            // which we will need later
            var stack = VolumeManager.instance.stack;

            #region Local Methods

            // Swaps render destinations back and forth, so that
            // we can have multiple passes and similar with only a few textures
            void BlitTo(Material mat, int pass = 0) {
                var first = m_latestDest;
                var last = first == m_destinationA ? m_destinationB : m_destinationA;
                Blit(cmd, first, last, mat, pass);

                m_latestDest = last;
            }

            void ApplyEffect(Type effect, Material material) {
                var eff = stack.GetComponent(effect);
                if (!((IPostProcessComponent)eff).IsActive()) return;
                var fields = effect.GetFields().Where(w => w.HasAttribute<ShaderNameAttribute>());
                var floats = fields.Where(w => w.FieldType.IsSubclassOf(typeof(VolumeParameter<float>)));
                var ints = fields.Where(w => w.FieldType.IsSubclassOf(typeof(VolumeParameter<int>)));
                var vec2s = fields.Where(w => w.FieldType.IsSubclassOf(typeof(VolumeParameter<Vector2>)));
                var vec3s = fields.Where(w => w.FieldType.IsSubclassOf(typeof(VolumeParameter<Vector3>)));


                foreach (var w in floats) {
                    var k = w.GetAttribute<ShaderNameAttribute>().shaderParameter;
                    var v = (VolumeParameter<float>)w.GetValue(eff);
                    material.SetFloat(k, v.GetValue<float>());
                }

                foreach (var w in ints) {
                    var k = w.GetAttribute<ShaderNameAttribute>().shaderParameter;
                    var v = (VolumeParameter<int>)w.GetValue(eff);
                    material.SetFloat(k, v.GetValue<int>());
                }

                foreach (var w in vec2s) {
                    var k = w.GetAttribute<ShaderNameAttribute>().shaderParameter;
                    var v = (VolumeParameter<Vector2>)w.GetValue(eff);
                    material.SetVector(k, v.GetValue<Vector2>());
                }

                // foreach (var w in vec3s) { UNUSED, DISABLED FOR PERFORMANCE
                //     var k = w.GetAttribute<ShaderNameAttribute>().shaderParameter;
                //     var v = (VolumeParameter<Vector3>)w.GetValue(eff);
                //     material.SetVector(k, v.GetValue<Vector3>());
                // }

                BlitTo(material);
            }

            #endregion

            // Starts with the camera source
            m_latestDest = m_source;

            //---Custom effect here---

            ApplyEffect(typeof(EffectPlayerCrt), m_playerCrtMaterial);

            // Add any other custom effect/component you want, in your preferred order
            // Custom effect 2, 3 , ...


            // DONE! Now that we have processed all our custom effects, applies the final result to camera
            Blit(cmd, m_latestDest, m_source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        //Cleans the temporary RTs when we don't need them anymore
        public override void OnCameraCleanup(CommandBuffer cmd) {
            cmd.ReleaseTemporaryRT(m_temporaryRTIdA);
            cmd.ReleaseTemporaryRT(m_temporaryRTIdB);
        }
    }

}