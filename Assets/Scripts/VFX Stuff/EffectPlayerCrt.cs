namespace TimeActivityFx {

    using System;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UnityEngine.Rendering.Universal;

    [Serializable, VolumeComponentMenuForRenderPipeline("Custom/CRT Effect", typeof(UniversalRenderPipeline))]
    public class EffectPlayerCrt : VolumeComponent, IPostProcessComponent {
        [ShaderName("_Intensity")] public ClampedFloatParameter intensity = new ClampedFloatParameter(value: 0, min: 0, max: 1, overrideState: true);
        [ShaderName("_Curvature")] public FloatParameter curvature = new FloatParameter(value: 4, overrideState: true);
        [ShaderName("_Lines")] public IntParameter lines = new IntParameter(value: 160, overrideState: true);
        [Header("Burnouts")][ShaderName("_Burnouts")] public IntParameter burnouts = new IntParameter(value: 160, overrideState: true);
        [ShaderName("_Burnout_Opacity")] public ClampedFloatParameter burnoutOpacity = new ClampedFloatParameter(value: 0.1f, min: 0, max: 1, overrideState: true);
        [ShaderName("_Burnout_Profile")] public Vector2Parameter burnoutProfile = new Vector2Parameter(value: new Vector2(0, 0.1f), overrideState: true);
        [Header("Artifact")][ShaderName("_Artifact_Speed")] public FloatParameter artifactSpeed = new FloatParameter(value: 0.1f, overrideState: true);
        [ShaderName("_Artifact_Intensity")] public ClampedFloatParameter artifactIntensity = new ClampedFloatParameter(value: 0.01f, min: 0, max: 0.1f, overrideState: true);
        [ShaderName("_Artifact_Break_Time")] public FloatParameter artifactBreakTime = new FloatParameter(value: 0.1f, overrideState: true);
        [ShaderName("_Artifact_Profile")] public Vector2Parameter artifactProfile = new Vector2Parameter(value: new Vector2(0, 0.1f), overrideState: true);

        public bool IsActive() => intensity.value > 0 && !GameSettings.accessibleMode;
        public bool IsTileCompatible() => true;
    }

}