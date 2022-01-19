using System;
using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class PostProcessingVolume : BaseActorNode
    {
        public float Priority { get; }
        public float BlendWeight { get; }
        public bool Enabled { get; }
        public bool Unbound { get; }
        public PostProcessingVolumeProcessor.PostProcessingVolumeSettings Settings { get; }

        public PostProcessingVolume(
            string name,
            ResourceReference archetype,
            string actorLabel,
            SpawnCollisionHandlingMethod spawnCollisionHandlingMethod,
            string folderPath,
            string rootComponentName,
            Node[] children,
            float priority,
            float blendWeight,
            bool enabled,
            bool unbound,
            PostProcessingVolumeProcessor.PostProcessingVolumeSettings settings
        )
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children)
        {
            Priority = priority;
            BlendWeight = blendWeight;
            Enabled = enabled;
            Unbound = unbound;
            Settings = settings;
        }
    }

    public class PostProcessingVolumeProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.PostProcessVolume";

        public PostProcessingVolumeProcessor()
        {
            AddRequiredProperty("bUnbound", PropertyDataType.Boolean);
            
            AddOptionalProperty("Priority", PropertyDataType.Float);
            AddOptionalProperty("bEnabled", PropertyDataType.Boolean);
            AddOptionalProperty("BlendWeight", PropertyDataType.Float);

            AddIgnoredProperty("bHidden");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            var list = ValueUtil.ParseAttributeList(node.FindPropertyValue("Settings"));

            // var parser = new DocumentParser("Begin Settings " + node.FindProperty("Settings").Sub);

            var settingNode = new ParsedNode("Settings", ParsedNodeBag.Empty, ParsedPropertyBag.Empty, list); 
            var settingsDocument = new ParsedDocument("Settings", settingNode);
            var settingsProcessor = new GenericDocumentProcessor<PostProcessingVolumeSettings>();
            settingsProcessor.AddNodeProcessor(new PostProcessingVolumeSettingsProcessor());
            
            var settingState = settingsProcessor.Convert(settingsDocument);
            var root = settingState.RootNode;
            
            return new PostProcessingVolume(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                ValueUtil.ParseFloat(node.FindPropertyValue("Priority")),
                ValueUtil.ParseFloat(node.FindPropertyValue("Priority") ?? "1.0"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bEnabled") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bUnbound")),
                root
            );
        }

        public class PostProcessingVolumeSettings : Node
        {
            public ColorGradingSettings ColorGrading { get; }
            public FilmSettings Film { get; }
            public LensSettings Lens { get; }
            public RenderingFeaturesSettings RenderingFeatures { get; }
            public RayTracingSettings RayTracing { get; }
            public PathTracingSettings PathTracing { get; }
            public MotionBlurSettings MotionBlur { get; }
            public LightPropagationVolumeSettings LightPropagationVolume { get; }
            public GlobalIlluminationSettings GlobalIllumination { get; }
            
            public PostProcessingVolumeSettings(
                string name,
                Node[] children,
                ColorGradingSettings colorGrading,
                FilmSettings film,
                LensSettings lens,
                RenderingFeaturesSettings renderingFeatures,
                RayTracingSettings rayTracing,
                PathTracingSettings pathTracing,
                MotionBlurSettings motionBlur,
                LightPropagationVolumeSettings lightPropagationVolume,
                GlobalIlluminationSettings globalIllumination
            )
                : base(name, children)
            {
                ColorGrading = colorGrading;
                Film = film;
                Lens = lens;
                RenderingFeatures = renderingFeatures;
                RayTracing = rayTracing;
                PathTracing = pathTracing;
                MotionBlur = motionBlur;
                LightPropagationVolume = lightPropagationVolume;
                GlobalIllumination = globalIllumination;
            }
        }
        
        internal class PostProcessingVolumeSettingsProcessor : NodeProcessor
        {
            public PostProcessingVolumeSettingsProcessor()
            {
                AddOptionalProperty("bOverride_TemperatureType", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_WhiteTemp", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_WhiteTint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorSaturation", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorContrast", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGamma", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGain", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorOffset", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorSaturationShadows", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorContrastShadows", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGammaShadows", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGainShadows", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorOffsetShadows", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorSaturationMidtones", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorContrastMidtones", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGammaMidtones", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGainMidtones", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorOffsetMidtones", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorSaturationHighlights", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorContrastHighlights", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGammaHighlights", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGainHighlights", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorOffsetHighlights", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorCorrectionShadowsMax", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorCorrectionHighlightsMin", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BlueCorrection", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ExpandGamut", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ToneCurveAmount", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_FilmSlope", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_FilmToe", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_FilmShoulder", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_FilmBlackClip", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_FilmWhiteClip", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_SceneColorTint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_SceneFringeIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ChromaticAberrationStartOffset", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientCubemapTint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientCubemapIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomMethod", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomThreshold", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom1Tint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom1Size", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom2Size", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom2Tint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom3Tint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom3Size", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom4Tint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom4Size", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom5Tint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom5Size", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom6Tint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_Bloom6Size", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomSizeScale", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomConvolutionTexture", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomConvolutionSize", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomConvolutionCenterUV", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomConvolutionPreFilterMin", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomConvolutionPreFilterMax", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomConvolutionPreFilterMult", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomConvolutionBufferScale", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomDirtMaskIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomDirtMaskTint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_BloomDirtMask", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_CameraShutterSpeed", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_CameraISO", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureMethod", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureLowPercent", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureHighPercent", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureMinBrightness", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureMaxBrightness", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureSpeedUp", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureSpeedDown", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureBias", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureBiasCurve", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureMeterMask", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AutoExposureApplyPhysicalCameraExposure", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_HistogramLogMin", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_HistogramLogMax", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LensFlareIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LensFlareTint", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LensFlareBokehSize", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LensFlareBokehShape", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LensFlareThreshold", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_VignetteIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_GrainIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_GrainJitter", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionStaticFraction", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionRadius", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionFadeDistance", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionFadeRadius", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionRadiusInWS", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionPower", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionBias", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionQuality", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionMipBlend", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionMipScale", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionMipThreshold", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_AmbientOcclusionTemporalBlendWeight", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingAO", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingAOSamplesPerPixel", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingAOIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingAORadius", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVDirectionalOcclusionIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVDirectionalOcclusionRadius", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVDiffuseOcclusionExponent", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVSpecularOcclusionExponent", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVDiffuseOcclusionIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVSpecularOcclusionIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVSize", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVSecondaryOcclusionIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVSecondaryBounceIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVGeometryVolumeBias", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVVplInjectionBias", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVEmissiveInjectionIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVFadeRange", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_LPVDirectionalOcclusionFadeRange", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_IndirectLightingColor", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_IndirectLightingIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGradingIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ColorGradingLUT", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldFocalDistance", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldFstop", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldMinFstop", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldBladeCount", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldDepthBlurRadius", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldDepthBlurAmount", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldFocalRegion", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldNearTransitionRegion", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldFarTransitionRegion", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldScale", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldNearBlurSize", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldFarBlurSize", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_MobileHQGaussian", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldOcclusion", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldSkyFocusDistance", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_DepthOfFieldVignetteSize", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_MotionBlurAmount", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_MotionBlurMax", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_MotionBlurTargetFPS", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_MotionBlurPerObjectSize", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ScreenPercentage", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ScreenSpaceReflectionIntensity", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ScreenSpaceReflectionQuality", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ScreenSpaceReflectionMaxRoughness", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_ReflectionsType", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingReflectionsMaxRoughness", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingReflectionsMaxBounces", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingReflectionsSamplesPerPixel", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingReflectionsShadows", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingReflectionsTranslucency", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_TranslucencyType", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingTranslucencyMaxRoughness", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingTranslucencyRefractionRays", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingTranslucencySamplesPerPixel", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingTranslucencyShadows", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingTranslucencyRefraction", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingGI", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingGIMaxBounces", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_RayTracingGISamplesPerPixel", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_PathTracingMaxBounces", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_PathTracingSamplesPerPixel", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_PathTracingFilterWidth", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_PathTracingEnableEmissive", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_PathTracingMaxPathExposure", PropertyDataType.Boolean);
                AddOptionalProperty("bOverride_PathTracingEnableDenoiser", PropertyDataType.Boolean);
                AddOptionalProperty("bMobileHQGaussian", PropertyDataType.Boolean);
                AddOptionalProperty("BloomMethod", PropertyDataType.String);
                AddOptionalProperty("AutoExposureMethod", PropertyDataType.String);
                AddOptionalProperty("TemperatureType", PropertyDataType.String);
                AddOptionalProperty("WhiteTemp", PropertyDataType.Float);
                AddOptionalProperty("WhiteTint", PropertyDataType.Float);
                AddOptionalProperty("ColorSaturation", PropertyDataType.Vector4);
                AddOptionalProperty("ColorContrast", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGamma", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGain", PropertyDataType.Vector4);
                AddOptionalProperty("ColorOffset", PropertyDataType.Vector4);
                AddOptionalProperty("ColorSaturationShadows", PropertyDataType.Vector4);
                AddOptionalProperty("ColorContrastShadows", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGammaShadows", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGainShadows", PropertyDataType.Vector4);
                AddOptionalProperty("ColorOffsetShadows", PropertyDataType.Vector4);
                AddOptionalProperty("ColorSaturationMidtones", PropertyDataType.Vector4);
                AddOptionalProperty("ColorContrastMidtones", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGammaMidtones", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGainMidtones", PropertyDataType.Vector4);
                AddOptionalProperty("ColorOffsetMidtones", PropertyDataType.Vector4);
                AddOptionalProperty("ColorSaturationHighlights", PropertyDataType.Vector4);
                AddOptionalProperty("ColorContrastHighlights", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGammaHighlights", PropertyDataType.Vector4);
                AddOptionalProperty("ColorGainHighlights", PropertyDataType.Vector4);
                AddOptionalProperty("ColorOffsetHighlights", PropertyDataType.Vector4);
                AddOptionalProperty("ColorCorrectionHighlightsMin", PropertyDataType.Float);
                AddOptionalProperty("ColorCorrectionShadowsMax", PropertyDataType.Float);
                AddOptionalProperty("BlueCorrection", PropertyDataType.Float);
                AddOptionalProperty("ExpandGamut", PropertyDataType.Float);
                AddOptionalProperty("ToneCurveAmount", PropertyDataType.Float);
                AddOptionalProperty("FilmSlope", PropertyDataType.Float);
                AddOptionalProperty("FilmToe", PropertyDataType.Float);
                AddOptionalProperty("FilmShoulder", PropertyDataType.Float);
                AddOptionalProperty("FilmBlackClip", PropertyDataType.Float);
                AddOptionalProperty("FilmWhiteClip", PropertyDataType.Float);
                AddOptionalProperty("SceneColorTint", PropertyDataType.Vector4);
                AddOptionalProperty("SceneFringeIntensity", PropertyDataType.Float);
                AddOptionalProperty("ChromaticAberrationStartOffset", PropertyDataType.Float);
                AddOptionalProperty("BloomIntensity", PropertyDataType.Float);
                AddOptionalProperty("BloomThreshold", PropertyDataType.Float);
                AddOptionalProperty("BloomConvolutionSize", PropertyDataType.Float);
                AddOptionalProperty("BloomConvolutionTexture", PropertyDataType.ResourceReference);
                AddOptionalProperty("BloomConvolutionCenterUV", PropertyDataType.Vector2);
                AddOptionalProperty("BloomConvolutionPreFilterMin", PropertyDataType.Float);
                AddOptionalProperty("BloomConvolutionPreFilterMax", PropertyDataType.Float);
                AddOptionalProperty("BloomConvolutionPreFilterMult", PropertyDataType.Float);
                AddOptionalProperty("BloomConvolutionBufferScale", PropertyDataType.Float);
                AddOptionalProperty("BloomDirtMask", PropertyDataType.ResourceReference);
                AddOptionalProperty("BloomDirtMaskIntensity", PropertyDataType.Float);
                AddOptionalProperty("BloomDirtMaskTint", PropertyDataType.Vector4);
                AddOptionalProperty("AmbientCubemapTint", PropertyDataType.Vector4);
                AddOptionalProperty("AmbientCubemapIntensity", PropertyDataType.Float);
                AddOptionalProperty("AmbientCubemap", PropertyDataType.ResourceReference);
                AddOptionalProperty("DepthOfFieldFstop", PropertyDataType.Float);
                AddOptionalProperty("DepthOfFieldMinFstop", PropertyDataType.Float);
                AddOptionalProperty("DepthOfFieldBladeCount", PropertyDataType.Integer);
                AddOptionalProperty("DepthOfFieldBladeFocalDistance", PropertyDataType.Float);
                AddOptionalProperty("DepthOfFieldDepthBlurAmount", PropertyDataType.Float);
                AddOptionalProperty("DepthOfFieldDepthBlurRadius", PropertyDataType.Float);
                AddOptionalProperty("DepthOfFieldFocalRegion", PropertyDataType.Float);
                AddOptionalProperty("DepthOfFieldNearTransitionRegion", PropertyDataType.Float);
                AddOptionalProperty("DepthOfFieldFarTransitionRegion", PropertyDataType.Float);
                AddOptionalProperty("AutoExposureBias", PropertyDataType.Float);
                AddOptionalProperty("AutoExposureBiasBackup", PropertyDataType.Float);
                AddOptionalProperty("AutoExposureApplyPhysicalCameraExposure", PropertyDataType.Boolean);
                AddOptionalProperty("AutoExposureBiasCurve", PropertyDataType.ResourceReference);
                AddOptionalProperty("AutoExposureMeterMask", PropertyDataType.ResourceReference);
                AddOptionalProperty("AutoExposureMinBrightness", PropertyDataType.Float);
                AddOptionalProperty("AutoExposureMaxBrightness", PropertyDataType.Float);
                AddOptionalProperty("AutoExposureSpeedUp", PropertyDataType.Float);
                AddOptionalProperty("AutoExposureSpeedDown", PropertyDataType.Float);
                AddOptionalProperty("LensFlareIntensity", PropertyDataType.Float);
                AddOptionalProperty("LensFlareTint", PropertyDataType.Vector4);
                AddOptionalProperty("LensFlareBokehSize", PropertyDataType.Float);
                AddOptionalProperty("LensFlareThreshold", PropertyDataType.Float);
                AddOptionalProperty("LensFlareBokehShape", PropertyDataType.ResourceReference);
                AddOptionalProperty("VignetteIntensity", PropertyDataType.Float);
                AddOptionalProperty("GrainJitter", PropertyDataType.Float);
                AddOptionalProperty("GrainIntensity", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionIntensity", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionStaticFraction", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionRadius", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionRadiusInWS", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionFadeDistance", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionFadeRadius", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionPower", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionBias", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionQuality", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionMipBlend", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionMipScale", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionMipThreshold", PropertyDataType.Float);
                AddOptionalProperty("AmbientOcclusionTemporalBlendWeight", PropertyDataType.Float);
                AddOptionalProperty("RayTracingAO", PropertyDataType.Boolean);
                AddOptionalProperty("RayTracingAOSamplesPerPixel", PropertyDataType.Integer);
                AddOptionalProperty("RayTracingAOIntensity", PropertyDataType.Float);
                AddOptionalProperty("RayTracingAORadius", PropertyDataType.Float);
                AddOptionalProperty("IndirectLightingColor", PropertyDataType.Vector4);
                AddOptionalProperty("IndirectLightingIntensity", PropertyDataType.Float);
                AddOptionalProperty("RayTracingGIType", PropertyDataType.String);
                AddOptionalProperty("RayTracingGIMaxBounces", PropertyDataType.Integer);
                AddOptionalProperty("RayTracingGISamplesPerPixel", PropertyDataType.Integer);
                AddOptionalProperty("ColorGradingIntensity", PropertyDataType.Float);
                AddOptionalProperty("ColorGradingLUT", PropertyDataType.ResourceReference);
                AddOptionalProperty("DepthOfFieldOcclusion", PropertyDataType.Float);
                AddOptionalProperty("MotionBlurAmount", PropertyDataType.Float);
                AddOptionalProperty("MotionBlurMax", PropertyDataType.Float);
                AddOptionalProperty("MotionBlurTargetFPS", PropertyDataType.Float);
                AddOptionalProperty("MotionBlurPerObjectSize", PropertyDataType.Float);
                AddOptionalProperty("LPVIntensity", PropertyDataType.Float);
                AddOptionalProperty("LPVVplInjectionBias", PropertyDataType.Float);
                AddOptionalProperty("LPVSize", PropertyDataType.Float);
                AddOptionalProperty("LPVSecondaryOcclusionIntensity", PropertyDataType.Float);
                AddOptionalProperty("LPVSecondaryBounceIntensity", PropertyDataType.Float);
                AddOptionalProperty("LPVGeometryVolumeBias", PropertyDataType.Float);
                AddOptionalProperty("LPVEmissiveInjectionIntensity", PropertyDataType.Float);
                AddOptionalProperty("LPVDirectionalOcclusionIntensity", PropertyDataType.Float);
                AddOptionalProperty("LPVDirectionalOcclusionRadius", PropertyDataType.Float);
                AddOptionalProperty("LPVDiffuseOcclusionExponent", PropertyDataType.Float);
                AddOptionalProperty("LPVSpecularOcclusionExponent", PropertyDataType.Float);
                AddOptionalProperty("LPVDiffuseOcclusionIntensity", PropertyDataType.Float);
                AddOptionalProperty("LPVSpecularOcclusionIntensity", PropertyDataType.Float);
                AddOptionalProperty("ReflectionsType", PropertyDataType.String);
                AddOptionalProperty("ScreenSpaceReflectionIntensity", PropertyDataType.Float);
                AddOptionalProperty("ScreenSpaceReflectionQuality", PropertyDataType.Float);
                AddOptionalProperty("ScreenSpaceReflectionMaxRoughness", PropertyDataType.Float);
                AddOptionalProperty("RayTracingReflectionsMaxRoughness", PropertyDataType.Float);
                AddOptionalProperty("RayTracingReflectionsMaxBounces", PropertyDataType.Integer);
                AddOptionalProperty("RayTracingReflectionsSamplesPerPixel", PropertyDataType.Integer);
                AddOptionalProperty("RayTracingReflectionsShadows", PropertyDataType.String);
                AddOptionalProperty("RayTracingReflectionsTranslucency", PropertyDataType.Boolean);
                AddOptionalProperty("TranslucencyType", PropertyDataType.String);
                AddOptionalProperty("RayTracingTranslucencyMaxRoughness", PropertyDataType.Float);
                AddOptionalProperty("RayTracingTranslucencyRefractionRays", PropertyDataType.Integer);
                AddOptionalProperty("RayTracingTranslucencySamplesPerPixel", PropertyDataType.Integer);
                AddOptionalProperty("RayTracingTranslucencyShadows", PropertyDataType.String);
                AddOptionalProperty("RayTracingTranslucencyRefraction", PropertyDataType.Boolean);
                AddOptionalProperty("PathTracingMaxBounces", PropertyDataType.Integer);
                AddOptionalProperty("PathTracingSamplesPerPixel", PropertyDataType.Integer);
                AddOptionalProperty("PathTracingFilterWidth", PropertyDataType.Float);
                AddOptionalProperty("PathTracingEnableEmissive", PropertyDataType.Boolean);
                AddOptionalProperty("PathTracingMaxPathExposure", PropertyDataType.Float);
                AddOptionalProperty("PathTracingEnableDenoiser", PropertyDataType.Boolean);
                AddOptionalProperty("LPVFadeRange", PropertyDataType.Float);
                AddOptionalProperty("LPVDirectionalOcclusionFadeRange", PropertyDataType.Float);
                AddOptionalProperty("ScreenPercentage", PropertyDataType.Float);

            }

            public override bool Supports(ParsedNode node)
            {
                return node.SectionType == "Settings";
            }

            public override Node Convert(ParsedNode node, Node[] children)
            {
                return new PostProcessingVolumeSettings(
                    "Settings",
                    Array.Empty<Node>(),
                    new ColorGradingSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_TemperatureType")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_WhiteTemp")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_WhiteTint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorSaturation")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorContrast")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGamma")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGain")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorOffset")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorSaturationShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorContrastShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGammaShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGainShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorOffsetShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorSaturationMidtones")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorContrastMidtones")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGammaMidtones")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGainMidtones")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorOffsetMidtones")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorSaturationHighlights")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorContrastHighlights")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGammaHighlights")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGainHighlights")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorOffsetHighlights")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorCorrectionShadowsMax")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorCorrectionHighlightsMin")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BlueCorrection")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ExpandGamut")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ToneCurveAmount")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_SceneColorTint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGradingIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ColorGradingLUT")),
                        ValueUtil.ParseTemperatureType(node.FindPropertyValue("TemperatureType") ?? "TEMP_WhiteBalance"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("WhiteTemp") ?? "6500"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("WhiteTint")),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorSaturation") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorContrast") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGamma") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGain") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorOffset")),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorSaturationShadows") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorContrastShadows") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGammaShadows") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGainShadows") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorOffsetShadows")),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorSaturationMidtones") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorContrastMidtones") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGammaMidtones") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGainMidtones") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorOffsetMidtones") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorSaturationHighlights") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorContrastHighlights") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGammaHighlights") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorGainHighlights") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("ColorOffsetHighlights")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ColorCorrectionHighlightsMin") ?? "0.5"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ColorCorrectionShadowsMax") ?? "0.09"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BlueCorrection") ?? "0.6"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ExpandGamut") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ToneCurveAmount") ?? "1.0"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("SceneColorTint") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ColorGradingIntensity") ?? "1.0"),
                        ValueUtil.ParseResourceReference(node.FindPropertyValue("ColorGradingLUT"))
                    ),
                    new FilmSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_FilmSlope") ),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_FilmToe")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_FilmShoulder")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_FilmBlackClip")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_FilmWhiteClip")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("FilmSlope") ?? "0.88"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("FilmToe") ?? "0.55"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("FilmShoulder") ?? "0.26"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("FilmBlackClip")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("FilmWhiteClip") ?? "0.04")
                    ),
                    new LensSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_SceneFringeIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ChromaticAberrationStartOffset")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomMethod")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomThreshold")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom1Tint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom1Size")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom2Size")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom2Tint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom3Tint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom3Size")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom4Tint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom4Size")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom5Tint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom5Size")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom6Tint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_Bloom6Size")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomSizeScale")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomConvolutionTexture")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomConvolutionSize")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomConvolutionCenterUV")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomConvolutionPreFilterMin")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomConvolutionPreFilterMax")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomConvolutionPreFilterMult")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomConvolutionBufferScale")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomDirtMaskIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomDirtMaskTint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_BloomDirtMask")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_CameraShutterSpeed")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_CameraISO")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureMethod")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureLowPercent")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureHighPercent")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureMinBrightness")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureMaxBrightness")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureSpeedUp")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureSpeedDown")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureBias")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureBiasCurve")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureMeterMask")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AutoExposureApplyPhysicalCameraExposure")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_HistogramLogMin")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_HistogramLogMax")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LensFlareIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LensFlareTint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LensFlareBokehSize")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LensFlareBokehShape")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LensFlareThreshold")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_VignetteIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_GrainIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_GrainJitter")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldFocalDistance")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldFstop")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldMinFstop")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldBladeCount")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldDepthBlurRadius")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldDepthBlurAmount")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldFocalRegion")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldNearTransitionRegion")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldFarTransitionRegion")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldScale")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldNearBlurSize")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldFarBlurSize")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldOcclusion")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldSkyFocusDistance")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_DepthOfFieldVignetteSize")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_MobileHQGaussian")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("SceneFringeIntensity")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ChromaticAberrationStartOffset")),
                        ValueUtil.ParseBloomMethod(node.FindPropertyValue("BloomMethod")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomIntensity") ?? "0.675"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomThreshold") ?? "-1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomConvolutionSize") ?? "1.0"),
                        ValueUtil.ParseResourceReference(node.FindPropertyValue("BloomConvolutionTexture")),
                        ValueUtil.ParseVector2(node.FindPropertyValue("BloomConvolutionCenterUV") ?? "(X=0.5,Y=0.5)"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomConvolutionPreFilterMin") ?? "7.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomConvolutionPreFilterMax") ?? "15000.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomConvolutionPreFilterMult") ?? "15.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomConvolutionBufferScale") ?? "1.0"),
                        ValueUtil.ParseResourceReference(node.FindPropertyValue("BloomDirtMask")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("BloomDirtMaskIntensity")),
                        ValueUtil.ParseVector4(node.FindPropertyValue("BloomDirtMaskTint") ?? "(R=0.5,G=0.5,B=0.5,A=1)"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldFstop") ?? "4.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldMinFstop") ?? "1.2"),
                        ValueUtil.ParseInteger(node.FindPropertyValue("DepthOfFieldBladeCount") ?? "5"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldOcclusion") ?? "0.4"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldFocalDistance")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldDepthBlurAmount") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldDepthBlurRadius")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldFocalRegion")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldNearTransitionRegion") ?? "300.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("DepthOfFieldFarTransitionRegion") ?? "500.0"),
                        ValueUtil.ParseAutoExposureMethod(node.FindPropertyValue("AutoExposureMethod")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AutoExposureBias") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AutoExposureBiasBackup")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("AutoExposureApplyPhysicalCameraExposure")),
                        ValueUtil.ParseResourceReference(node.FindPropertyValue("AutoExposureBiasCurve")),
                        ValueUtil.ParseResourceReference(node.FindPropertyValue("AutoExposureMeterMask")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AutoExposureMinBrightness") ?? "0.03"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AutoExposureMaxBrightness") ?? "8.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AutoExposureSpeedUp") ?? "3.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AutoExposureSpeedDown") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LensFlareIntensity") ?? "1.0"),
                        ValueUtil.ParseVector4(node.FindPropertyValue("LensFlareTint") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LensFlareBokehSize") ?? "3.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LensFlareThreshold") ?? "8.0"),
                        ValueUtil.ParseResourceReference(node.FindPropertyValue("LensFlareBokehShape")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("VignetteIntensity") ?? "0.4"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("GrainJitter")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("GrainIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bMobileHQGaussian"))
                    ),
                    new RenderingFeaturesSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientCubemapTint")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientCubemapIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionStaticFraction")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionRadius")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionFadeDistance")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionFadeRadius")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionRadiusInWS")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionPower")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionBias")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionQuality")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionMipBlend")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionMipScale")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionMipThreshold")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_AmbientOcclusionTemporalBlendWeight")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ScreenPercentage")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ReflectionsType")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_TranslucencyType")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ScreenSpaceReflectionIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ScreenSpaceReflectionQuality")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_ScreenSpaceReflectionMaxRoughness")),
                        ValueUtil.ParseVector4(node.FindPropertyValue("AmbientCubemapTint") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientCubemapIntensity") ?? "1.0"),
                        ValueUtil.ParseResourceReference(node.FindPropertyValue("AmbientCubemap")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionIntensity") ?? "0.5"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionStaticFraction") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionRadius") ?? "200.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionRadiusInWS")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionFadeDistance") ?? "8000.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionFadeRadius") ?? "5000.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionPower") ?? "2.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionBias") ?? "3.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionQuality") ?? "50.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionMipBlend") ?? "0.6"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionMipScale") ?? "1.7"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionMipThreshold") ?? "0.01"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("AmbientOcclusionTemporalBlendWeight") ?? "0.1"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ScreenPercentage") ?? "100.0"),
                        ValueUtil.ParseReflectionsType(node.FindPropertyValue("ReflectionsType")),
                        ValueUtil.ParseTranslucencyType(node.FindPropertyValue("TranslucencyType")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ScreenSpaceReflectionIntensity") ?? "100.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ScreenSpaceReflectionQuality") ?? "50.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("ScreenSpaceReflectionMaxRoughness") ?? "0.6")
                    ),
                    new RayTracingSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingAO")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingAOSamplesPerPixel")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingAOIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingAORadius")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingReflectionsMaxRoughness")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingReflectionsMaxBounces")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingReflectionsSamplesPerPixel")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingReflectionsShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingReflectionsTranslucency")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingTranslucencyMaxRoughness")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingTranslucencyRefractionRays")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingTranslucencySamplesPerPixel")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingTranslucencyShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingTranslucencyRefraction")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingGI")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingGIMaxBounces")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_RayTracingGISamplesPerPixel")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("RayTracingAO")),
                        ValueUtil.ParseInteger(node.FindPropertyValue("RayTracingAOSamplesPerPixel") ?? "1"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("RayTracingAOIntensity") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("RayTracingAORadius") ?? "200.0"),
                        ValueUtil.ParseRayTracingGlobalIlluminationType(node.FindPropertyValue("RayTracingGIType")),
                        ValueUtil.ParseInteger(node.FindPropertyValue("RayTracingGIMaxBounces") ?? "1"),
                        ValueUtil.ParseInteger(node.FindPropertyValue("RayTracingGISamplesPerPixel") ?? "4"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("RayTracingReflectionsMaxRoughness") ?? "0.6"),
                        ValueUtil.ParseInteger(node.FindPropertyValue("RayTracingReflectionsMaxBounces") ?? "1"),
                        ValueUtil.ParseInteger(node.FindPropertyValue("RayTracingReflectionsSamplesPerPixel") ?? "1"),
                        ValueUtil.ParseRayTracingShadows(node.FindPropertyValue("RayTracingReflectionsShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("RayTracingReflectionsTranslucency")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("RayTracingTranslucencyMaxRoughness") ?? "0.6"),
                        ValueUtil.ParseInteger(node.FindPropertyValue("RayTracingTranslucencyRefractionRays") ?? "3"),
                        ValueUtil.ParseInteger(node.FindPropertyValue("RayTracingTranslucencySamplesPerPixel") ?? "1"),
                        ValueUtil.ParseRayTracingShadows(node.FindPropertyValue("RayTracingTranslucencyShadows")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("RayTracingTranslucencyRefraction") ?? "True")
                    ),
                    new PathTracingSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_PathTracingMaxBounces")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_PathTracingSamplesPerPixel")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_PathTracingFilterWidth")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_PathTracingEnableEmissive")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_PathTracingMaxPathExposure")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_PathTracingEnableDenoiser")),
                        ValueUtil.ParseInteger(node.FindPropertyValue("PathTracingMaxBounces") ?? "32"),
                        ValueUtil.ParseInteger(node.FindPropertyValue("PathTracingSamplesPerPixel") ?? "16384"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("PathTracingFilterWidth") ?? "3.0"),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("PathTracingEnableEmissive") ?? "True"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("PathTracingMaxPathExposure") ?? "30.0"),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("PathTracingEnableDenoiser") ?? "True")
                    ),
                    new MotionBlurSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_MotionBlurAmount")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_MotionBlurMax")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_MotionBlurTargetFPS")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_MotionBlurPerObjectSize")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("MotionBlurAmount") ?? "0.5"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("MotionBlurMax") ?? "5.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("MotionBlurTargetFPS") ?? "30"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("MotionBlurPerObjectSize") ?? "0.5")
                    ),
                    new LightPropagationVolumeSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVDirectionalOcclusionIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVDirectionalOcclusionRadius")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVDiffuseOcclusionExponent")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVSpecularOcclusionExponent")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVDiffuseOcclusionIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVSpecularOcclusionIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVSize")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVSecondaryOcclusionIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVSecondaryBounceIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVGeometryVolumeBias")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVVplInjectionBias")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVEmissiveInjectionIntensity")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVFadeRange")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_LPVDirectionalOcclusionFadeRange")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVIntensity") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVVplInjectionBias") ?? "0.64"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVSize") ?? "5312.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVSecondaryOcclusionIntensity")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVSecondaryBounceIntensity")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVGeometryVolumeBias") ?? "0.384"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVEmissiveInjectionIntensity") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVDirectionalOcclusionIntensity")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVDirectionalOcclusionRadius") ?? "8.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVDiffuseOcclusionExponent") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVSpecularOcclusionExponent") ?? "7.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVDiffuseOcclusionIntensity") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVSpecularOcclusionIntensity") ?? "1.0"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVFadeRange")),
                        ValueUtil.ParseFloat(node.FindPropertyValue("LPVDirectionalOcclusionFadeRange"))
                    ),
                    new GlobalIlluminationSettings(
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_IndirectLightingColor")),
                        ValueUtil.ParseBoolean(node.FindPropertyValue("bOverride_IndirectLightingIntensity")),
                        ValueUtil.ParseVector4(node.FindPropertyValue("IndirectLightingColor") ?? "(R=1,G=1,B=1,A=1)"),
                        ValueUtil.ParseFloat(node.FindPropertyValue("IndirectLightingIntensity") ?? "1.0")
                    )
                );
            }
        }

        public class ColorGradingSettings
        {
            public bool OverrideTemperatureType { get; }
            public bool OverrideWhiteTemp { get; }
            public bool OverrideWhiteTint { get; }
            public bool OverrideColorSaturation { get; }
            public bool OverrideColorContrast { get; }
            public bool OverrideColorGamma { get; }
            public bool OverrideColorGain { get; }
            public bool OverrideColorOffset { get; }
            public bool OverrideColorSaturationShadows { get; }
            public bool OverrideColorContrastShadows { get; }
            public bool OverrideColorGammaShadows { get; }
            public bool OverrideColorGainShadows { get; }
            public bool OverrideColorOffsetShadows { get; }
            public bool OverrideColorSaturationMidtones { get; }
            public bool OverrideColorContrastMidtones { get; }
            public bool OverrideColorGammaMidtones { get; }
            public bool OverrideColorGainMidtones { get; }
            public bool OverrideColorOffsetMidtones { get; }
            public bool OverrideColorSaturationHighlights { get; }
            public bool OverrideColorContrastHighlights { get; }
            public bool OverrideColorGammaHighlights { get; }
            public bool OverrideColorGainHighlights { get; }
            public bool OverrideColorOffsetHighlights { get; }
            public bool OverrideColorCorrectionShadowsMax { get; }
            public bool OverrideColorCorrectionHighlightsMin { get; }
            public bool OverrideBlueCorrection { get; }
            public bool OverrideExpandGamut { get; }
            public bool OverrideToneCurveAmount { get; }
            public bool OverrideSceneColorTint { get; }
            public bool OverrideColorGradingIntensity { get; }
            public bool OverrideColorGradingLUT { get; }

            public TemperatureType TemperatureType { get; }
            public float WhiteTemp { get; }
            public float WhiteTint { get; }
            public Vector4 ColorSaturation { get; }
            public Vector4 ColorContrast { get; }
            public Vector4 ColorGamma { get; }
            public Vector4 ColorGain { get; }
            public Vector4 ColorOffset { get; }
            public Vector4 ColorSaturationShadows { get; }
            public Vector4 ColorContrastShadows { get; }
            public Vector4 ColorGammaShadows { get; }
            public Vector4 ColorGainShadows { get; }
            public Vector4 ColorOffsetShadows { get; }
            public Vector4 ColorSaturationMidtones { get; }
            public Vector4 ColorContrastMidtones { get; }
            public Vector4 ColorGammaMidtones { get; }
            public Vector4 ColorGainMidtones { get; }
            public Vector4 ColorOffsetMidtones { get; }
            public Vector4 ColorSaturationHighlights { get; }
            public Vector4 ColorContrastHighlights { get; }
            public Vector4 ColorGammaHighlights { get; }
            public Vector4 ColorGainHighlights { get; }
            public Vector4 ColorOffsetHighlights { get; }
            public float ColorCorrectionHighlightsMin { get; }
            public float ColorCorrectionShadowsMax { get; }
            public float BlueCorrection { get; }
            public float ExpandGamut { get; }
            public float ToneCurveAmount { get; }
            public Vector4 SceneColorTint { get; }
            public float ColorGradingIntensity { get; }
            public ResourceReference ColorGradingLUT { get; }

            public ColorGradingSettings(
                bool overrideTemperatureType,
                bool overrideWhiteTemp,
                bool overrideWhiteTint,
                bool overrideColorSaturation,
                bool overrideColorContrast,
                bool overrideColorGamma,
                bool overrideColorGain,
                bool overrideColorOffset,
                bool overrideColorSaturationShadows,
                bool overrideColorContrastShadows,
                bool overrideColorGammaShadows,
                bool overrideColorGainShadows,
                bool overrideColorOffsetShadows,
                bool overrideColorSaturationMidtones,
                bool overrideColorContrastMidtones,
                bool overrideColorGammaMidtones,
                bool overrideColorGainMidtones,
                bool overrideColorOffsetMidtones,
                bool overrideColorSaturationHighlights,
                bool overrideColorContrastHighlights,
                bool overrideColorGammaHighlights,
                bool overrideColorGainHighlights,
                bool overrideColorOffsetHighlights,
                bool overrideColorCorrectionShadowsMax,
                bool overrideColorCorrectionHighlightsMin,
                bool overrideBlueCorrection,
                bool overrideExpandGamut,
                bool overrideToneCurveAmount,
                bool overrideSceneColorTint,
                bool overrideColorGradingIntensity,
                bool overrideColorGradingLUT,
                TemperatureType temperatureType,
                float whiteTemp,
                float whiteTint,
                Vector4 colorSaturation,
                Vector4 colorContrast,
                Vector4 colorGamma,
                Vector4 colorGain,
                Vector4 colorOffset,
                Vector4 colorSaturationShadows,
                Vector4 colorContrastShadows,
                Vector4 colorGammaShadows,
                Vector4 colorGainShadows,
                Vector4 colorOffsetShadows,
                Vector4 colorSaturationMidtones,
                Vector4 colorContrastMidtones,
                Vector4 colorGammaMidtones,
                Vector4 colorGainMidtones,
                Vector4 colorOffsetMidtones,
                Vector4 colorSaturationHighlights,
                Vector4 colorContrastHighlights,
                Vector4 colorGammaHighlights,
                Vector4 colorGainHighlights,
                Vector4 colorOffsetHighlights,
                float colorCorrectionHighlightsMin,
                float colorCorrectionShadowsMax,
                float blueCorrection,
                float expandGamut,
                float toneCurveAmount,
                Vector4 sceneColorTint,
                float colorGradingIntensity,
                ResourceReference colorGradingLUT
            )
            {
                OverrideTemperatureType = overrideTemperatureType;
                OverrideWhiteTemp = overrideWhiteTemp;
                OverrideWhiteTint = overrideWhiteTint;
                OverrideColorSaturation = overrideColorSaturation;
                OverrideColorContrast = overrideColorContrast;
                OverrideColorGamma = overrideColorGamma;
                OverrideColorGain = overrideColorGain;
                OverrideColorOffset = overrideColorOffset;
                OverrideColorSaturationShadows = overrideColorSaturationShadows;
                OverrideColorContrastShadows = overrideColorContrastShadows;
                OverrideColorGammaShadows = overrideColorGammaShadows;
                OverrideColorGainShadows = overrideColorGainShadows;
                OverrideColorOffsetShadows = overrideColorOffsetShadows;
                OverrideColorSaturationMidtones = overrideColorSaturationMidtones;
                OverrideColorContrastMidtones = overrideColorContrastMidtones;
                OverrideColorGammaMidtones = overrideColorGammaMidtones;
                OverrideColorGainMidtones = overrideColorGainMidtones;
                OverrideColorOffsetMidtones = overrideColorOffsetMidtones;
                OverrideColorSaturationHighlights = overrideColorSaturationHighlights;
                OverrideColorContrastHighlights = overrideColorContrastHighlights;
                OverrideColorGammaHighlights = overrideColorGammaHighlights;
                OverrideColorGainHighlights = overrideColorGainHighlights;
                OverrideColorOffsetHighlights = overrideColorOffsetHighlights;
                OverrideColorCorrectionShadowsMax = overrideColorCorrectionShadowsMax;
                OverrideColorCorrectionHighlightsMin = overrideColorCorrectionHighlightsMin;
                OverrideBlueCorrection = overrideBlueCorrection;
                OverrideExpandGamut = overrideExpandGamut;
                OverrideToneCurveAmount = overrideToneCurveAmount;
                OverrideSceneColorTint = overrideSceneColorTint;
                OverrideColorGradingIntensity = overrideColorGradingIntensity;
                OverrideColorGradingLUT = overrideColorGradingLUT;
                
                TemperatureType = temperatureType;
                WhiteTemp = whiteTemp;
                WhiteTint = whiteTint;
                ColorSaturation = colorSaturation;
                ColorContrast = colorContrast;
                ColorGamma = colorGamma;
                ColorGain = colorGain;
                ColorOffset = colorOffset;
                ColorSaturationShadows = colorSaturationShadows;
                ColorContrastShadows = colorContrastShadows;
                ColorGammaShadows = colorGammaShadows;
                ColorGainShadows = colorGainShadows;
                ColorOffsetShadows = colorOffsetShadows;
                ColorSaturationMidtones = colorSaturationMidtones;
                ColorContrastMidtones = colorContrastMidtones;
                ColorGammaMidtones = colorGammaMidtones;
                ColorGainMidtones = colorGainMidtones;
                ColorOffsetMidtones = colorOffsetMidtones;
                ColorSaturationHighlights = colorSaturationHighlights;
                ColorContrastHighlights = colorContrastHighlights;
                ColorGammaHighlights = colorGammaHighlights;
                ColorGainHighlights = colorGainHighlights;
                ColorOffsetHighlights = colorOffsetHighlights;
                ColorCorrectionHighlightsMin = colorCorrectionHighlightsMin;
                ColorCorrectionShadowsMax = colorCorrectionShadowsMax;
                BlueCorrection = blueCorrection;
                ExpandGamut = expandGamut;
                ToneCurveAmount = toneCurveAmount;
                SceneColorTint = sceneColorTint;
                ColorGradingIntensity = colorGradingIntensity;
                ColorGradingLUT = colorGradingLUT;

            }
        }

        public class FilmSettings
        {
            public bool OverrideFilmSlope { get; }
            public bool OverrideFilmToe { get; }
            public bool OverrideFilmShoulder { get; }
            public bool OverrideFilmBlackClip { get; }
            public bool OverrideFilmWhiteClip { get; }

            public float FilmSlope { get; }
            public float FilmToe { get; }
            public float FilmShoulder { get; }
            public float FilmBlackClip { get; }
            public float FilmWhiteClip { get; }
            
            public FilmSettings(
                bool overrideFilmSlope,
                bool overrideFilmToe,
                bool overrideFilmShoulder,
                bool overrideFilmBlackClip,
                bool overrideFilmWhiteClip,
                float filmSlope,
                float filmToe,
                float filmShoulder,
                float filmBlackClip,
                float filmWhiteClip
            )
            {
                OverrideFilmSlope = overrideFilmSlope;
                OverrideFilmToe = overrideFilmToe;
                OverrideFilmShoulder = overrideFilmShoulder;
                OverrideFilmBlackClip = overrideFilmBlackClip;
                OverrideFilmWhiteClip = overrideFilmWhiteClip;

                FilmSlope = filmSlope;
                FilmToe = filmToe;
                FilmShoulder = filmShoulder;
                FilmBlackClip = filmBlackClip;
                FilmWhiteClip = filmWhiteClip;
            }
        }

        public class LensSettings
        {
            public bool OverrideSceneFringeIntensity { get; }
            public bool OverrideChromaticAberrationStartOffset { get; }
            public bool OverrideBloomMethod { get; }
            public bool OverrideBloomIntensity { get; }
            public bool OverrideBloomThreshold { get; }
            public bool OverrideBloom1Tint { get; }
            public bool OverrideBloom1Size { get; }
            public bool OverrideBloom2Size { get; }
            public bool OverrideBloom2Tint { get; }
            public bool OverrideBloom3Tint { get; }
            public bool OverrideBloom3Size { get; }
            public bool OverrideBloom4Tint { get; }
            public bool OverrideBloom4Size { get; }
            public bool OverrideBloom5Tint { get; }
            public bool OverrideBloom5Size { get; }
            public bool OverrideBloom6Tint { get; }
            public bool OverrideBloom6Size { get; }
            public bool OverrideBloomSizeScale { get; }
            public bool OverrideBloomConvolutionTexture { get; }
            public bool OverrideBloomConvolutionSize { get; }
            public bool OverrideBloomConvolutionCenterUV { get; }
            public bool OverrideBloomConvolutionPreFilterMin { get; }
            public bool OverrideBloomConvolutionPreFilterMax { get; }
            public bool OverrideBloomConvolutionPreFilterMult { get; }
            public bool OverrideBloomConvolutionBufferScale { get; }
            public bool OverrideBloomDirtMaskIntensity { get; }
            public bool OverrideBloomDirtMaskTint { get; }
            public bool OverrideBloomDirtMask { get; }
            public bool OverrideCameraShutterSpeed { get; }
            public bool OverrideCameraISO { get; }
            public bool OverrideAutoExposureMethod { get; }
            public bool OverrideAutoExposureLowPercent { get; }
            public bool OverrideAutoExposureHighPercent { get; }
            public bool OverrideAutoExposureMinBrightness { get; }
            public bool OverrideAutoExposureMaxBrightness { get; }
            public bool OverrideAutoExposureSpeedUp { get; }
            public bool OverrideAutoExposureSpeedDown { get; }
            public bool OverrideAutoExposureBias { get; }
            public bool OverrideAutoExposureBiasCurve { get; }
            public bool OverrideAutoExposureMeterMask { get; }
            public bool OverrideAutoExposureApplyPhysicalCameraExposure { get; }
            public bool OverrideHistogramLogMin { get; }
            public bool OverrideHistogramLogMax { get; }
            public bool OverrideLensFlareIntensity { get; }
            public bool OverrideLensFlareTint { get; }
            public bool OverrideLensFlareBokehSize { get; }
            public bool OverrideLensFlareBokehShape { get; }
            public bool OverrideLensFlareThreshold { get; }
            public bool OverrideVignetteIntensity { get; }
            public bool OverrideGrainIntensity { get; }
            public bool OverrideGrainJitter { get; }
            public bool OverrideDepthOfFieldFocalDistance { get; }
            public bool OverrideDepthOfFieldFstop { get; }
            public bool OverrideDepthOfFieldMinFstop { get; }
            public bool OverrideDepthOfFieldBladeCount { get; }
            public bool OverrideDepthOfFieldDepthBlurRadius { get; }
            public bool OverrideDepthOfFieldDepthBlurAmount { get; }
            public bool OverrideDepthOfFieldFocalRegion { get; }
            public bool OverrideDepthOfFieldNearTransitionRegion { get; }
            public bool OverrideDepthOfFieldFarTransitionRegion { get; }
            public bool OverrideDepthOfFieldScale { get; }
            public bool OverrideDepthOfFieldNearBlurSize { get; }
            public bool OverrideDepthOfFieldFarBlurSize { get; }
            public bool OverrideDepthOfFieldOcclusion { get; }
            public bool OverrideDepthOfFieldSkyFocusDistance { get; }
            public bool OverrideDepthOfFieldVignetteSize { get; }
            public bool OverrideMobileHQGaussian { get; }

            public float SceneFringeIntensity { get; }
            public float ChromaticAberrationStartOffset { get; }
            public float BloomIntensity { get; }
            public float BloomThreshold { get; }
            public float BloomConvolutionSize { get; }
            public ResourceReference BloomConvolutionTexture { get; }
            public Vector2 BloomConvolutionCenterUV { get; }
            public float BloomConvolutionPreFilterMin { get; }
            public float BloomConvolutionPreFilterMax { get; }
            public float BloomConvolutionPreFilterMult { get; }
            public float BloomConvolutionBufferScale { get; }
            public ResourceReference BloomDirtMask { get; }
            public float BloomDirtMaskIntensity { get; }
            public Vector4 BloomDirtMaskTint { get; }
            public BloomMethod BloomMethod { get; }
            public float DepthOfFieldFstop { get; }
            public float DepthOfFieldMinFstop { get; }
            public int DepthOfFieldBladeCount { get; }
            public float DepthOfFieldOcclusion { get; }
            public float DepthOfFieldFocalDistance { get; }
            public float DepthOfFieldDepthBlurAmount { get; }
            public float DepthOfFieldDepthBlurRadius { get; }
            public float DepthOfFieldFocalRegion { get; }
            public float DepthOfFieldNearTransitionRegion { get; }
            public float DepthOfFieldFarTransitionRegion { get; }
            public AutoExposureMethod AutoExposureMethod { get; }
            public float AutoExposureBias { get; }
            public float AutoExposureBiasBackup { get; }
            public bool AutoExposureApplyPhysicalCameraExposure { get; }
            public ResourceReference AutoExposureBiasCurve { get; }
            public ResourceReference AutoExposureMeterMask { get; }
            public float AutoExposureMinBrightness { get; }
            public float AutoExposureMaxBrightness { get; }
            public float AutoExposureSpeedUp { get; }
            public float AutoExposureSpeedDown { get; }
            public float LensFlareIntensity { get; }
            public Vector4 LensFlareTint { get; }
            public float LensFlareBokehSize { get; }
            public float LensFlareThreshold { get; }
            public ResourceReference LensFlareBokehShape { get; }
            public float VignetteIntensity { get; }
            public float GrainJitter { get; }
            public float GrainIntensity { get; }
            public bool MobileHQGaussian { get; }

            public LensSettings(
                bool overrideSceneFringeIntensity,
                bool overrideChromaticAberrationStartOffset,
                bool overrideBloomMethod,
                bool overrideBloomIntensity,
                bool overrideBloomThreshold,
                bool overrideBloom1Tint,
                bool overrideBloom1Size,
                bool overrideBloom2Size,
                bool overrideBloom2Tint,
                bool overrideBloom3Tint,
                bool overrideBloom3Size,
                bool overrideBloom4Tint,
                bool overrideBloom4Size,
                bool overrideBloom5Tint,
                bool overrideBloom5Size,
                bool overrideBloom6Tint,
                bool overrideBloom6Size,
                bool overrideBloomSizeScale,
                bool overrideBloomConvolutionTexture,
                bool overrideBloomConvolutionSize,
                bool overrideBloomConvolutionCenterUV,
                bool overrideBloomConvolutionPreFilterMin,
                bool overrideBloomConvolutionPreFilterMax,
                bool overrideBloomConvolutionPreFilterMult,
                bool overrideBloomConvolutionBufferScale,
                bool overrideBloomDirtMaskIntensity,
                bool overrideBloomDirtMaskTint,
                bool overrideBloomDirtMask,
                bool overrideCameraShutterSpeed,
                bool overrideCameraISO,
                bool overrideAutoExposureMethod,
                bool overrideAutoExposureLowPercent,
                bool overrideAutoExposureHighPercent,
                bool overrideAutoExposureMinBrightness,
                bool overrideAutoExposureMaxBrightness,
                bool overrideAutoExposureSpeedUp,
                bool overrideAutoExposureSpeedDown,
                bool overrideAutoExposureBias,
                bool overrideAutoExposureBiasCurve,
                bool overrideAutoExposureMeterMask,
                bool overrideAutoExposureApplyPhysicalCameraExposure,
                bool overrideHistogramLogMin,
                bool overrideHistogramLogMax,
                bool overrideLensFlareIntensity,
                bool overrideLensFlareTint,
                bool overrideLensFlareBokehSize,
                bool overrideLensFlareBokehShape,
                bool overrideLensFlareThreshold,
                bool overrideVignetteIntensity,
                bool overrideGrainIntensity,
                bool overrideGrainJitter,
                bool overrideDepthOfFieldFocalDistance,
                bool overrideDepthOfFieldFstop,
                bool overrideDepthOfFieldMinFstop,
                bool overrideDepthOfFieldBladeCount,
                bool overrideDepthOfFieldDepthBlurRadius,
                bool overrideDepthOfFieldDepthBlurAmount,
                bool overrideDepthOfFieldFocalRegion,
                bool overrideDepthOfFieldNearTransitionRegion,
                bool overrideDepthOfFieldFarTransitionRegion,
                bool overrideDepthOfFieldScale,
                bool overrideDepthOfFieldNearBlurSize,
                bool overrideDepthOfFieldFarBlurSize,
                bool overrideDepthOfFieldOcclusion,
                bool overrideDepthOfFieldSkyFocusDistance,
                bool overrideDepthOfFieldVignetteSize,
                bool overrideMobileHQGaussian,
                float sceneFringeIntensity,
                float chromaticAberrationStartOffset,
                BloomMethod bloomMethod,
                float bloomIntensity,
                float bloomThreshold,
                float bloomConvolutionSize,
                ResourceReference bloomConvolutionTexture,
                Vector2 bloomConvolutionCenterUV,
                float bloomConvolutionPreFilterMin,
                float bloomConvolutionPreFilterMax,
                float bloomConvolutionPreFilterMult,
                float bloomConvolutionBufferScale,
                ResourceReference bloomDirtMask,
                float bloomDirtMaskIntensity,
                Vector4 bloomDirtMaskTint,
                float depthOfFieldFstop,
                float depthOfFieldMinFstop,
                int depthOfFieldBladeCount,
                float depthOfFieldOcclusion,
                float depthOfFieldFocalDistance,
                float depthOfFieldDepthBlurAmount,
                float depthOfFieldDepthBlurRadius,
                float depthOfFieldFocalRegion,
                float depthOfFieldNearTransitionRegion,
                float depthOfFieldFarTransitionRegion,
                AutoExposureMethod autoExposureMethod,
                float autoExposureBias,
                float autoExposureBiasBackup,
                bool autoExposureApplyPhysicalCameraExposure,
                ResourceReference autoExposureBiasCurve,
                ResourceReference autoExposureMeterMask,
                float autoExposureMinBrightness,
                float autoExposureMaxBrightness,
                float autoExposureSpeedUp,
                float autoExposureSpeedDown,
                float lensFlareIntensity,
                Vector4 lensFlareTint,
                float lensFlareBokehSize,
                float lensFlareThreshold,
                ResourceReference lensFlareBokehShape,
                float vignetteIntensity,
                float grainJitter,
                float grainIntensity,
                bool mobileHQGaussian
            )
            {
                OverrideSceneFringeIntensity = overrideSceneFringeIntensity;
                OverrideChromaticAberrationStartOffset = overrideChromaticAberrationStartOffset;
                OverrideBloomMethod = overrideBloomMethod;
                OverrideBloomIntensity = overrideBloomIntensity;
                OverrideBloomThreshold = overrideBloomThreshold;
                OverrideBloom1Tint = overrideBloom1Tint;
                OverrideBloom1Size = overrideBloom1Size;
                OverrideBloom2Size = overrideBloom2Size;
                OverrideBloom2Tint = overrideBloom2Tint;
                OverrideBloom3Tint = overrideBloom3Tint;
                OverrideBloom3Size = overrideBloom3Size;
                OverrideBloom4Tint = overrideBloom4Tint;
                OverrideBloom4Size = overrideBloom4Size;
                OverrideBloom5Tint = overrideBloom5Tint;
                OverrideBloom5Size = overrideBloom5Size;
                OverrideBloom6Tint = overrideBloom6Tint;
                OverrideBloom6Size = overrideBloom6Size;
                OverrideBloomSizeScale = overrideBloomSizeScale;
                OverrideBloomConvolutionTexture = overrideBloomConvolutionTexture;
                OverrideBloomConvolutionSize = overrideBloomConvolutionSize;
                OverrideBloomConvolutionCenterUV = overrideBloomConvolutionCenterUV;
                OverrideBloomConvolutionPreFilterMin = overrideBloomConvolutionPreFilterMin;
                OverrideBloomConvolutionPreFilterMax = overrideBloomConvolutionPreFilterMax;
                OverrideBloomConvolutionPreFilterMult = overrideBloomConvolutionPreFilterMult;
                OverrideBloomConvolutionBufferScale = overrideBloomConvolutionBufferScale;
                OverrideBloomDirtMaskIntensity = overrideBloomDirtMaskIntensity;
                OverrideBloomDirtMaskTint = overrideBloomDirtMaskTint;
                OverrideBloomDirtMask = overrideBloomDirtMask;
                OverrideCameraShutterSpeed = overrideCameraShutterSpeed;
                OverrideCameraISO = overrideCameraISO;
                OverrideAutoExposureMethod = overrideAutoExposureMethod;
                OverrideAutoExposureLowPercent = overrideAutoExposureLowPercent;
                OverrideAutoExposureHighPercent = overrideAutoExposureHighPercent;
                OverrideAutoExposureMinBrightness = overrideAutoExposureMinBrightness;
                OverrideAutoExposureMaxBrightness = overrideAutoExposureMaxBrightness;
                OverrideAutoExposureSpeedUp = overrideAutoExposureSpeedUp;
                OverrideAutoExposureSpeedDown = overrideAutoExposureSpeedDown;
                OverrideAutoExposureBias = overrideAutoExposureBias;
                OverrideAutoExposureBiasCurve = overrideAutoExposureBiasCurve;
                OverrideAutoExposureMeterMask = overrideAutoExposureMeterMask;
                OverrideAutoExposureApplyPhysicalCameraExposure = overrideAutoExposureApplyPhysicalCameraExposure;
                OverrideHistogramLogMin = overrideHistogramLogMin;
                OverrideHistogramLogMax = overrideHistogramLogMax;
                OverrideLensFlareIntensity = overrideLensFlareIntensity;
                OverrideLensFlareTint = overrideLensFlareTint;
                OverrideLensFlareBokehSize = overrideLensFlareBokehSize;
                OverrideLensFlareBokehShape = overrideLensFlareBokehShape;
                OverrideLensFlareThreshold = overrideLensFlareThreshold;
                OverrideVignetteIntensity = overrideVignetteIntensity;
                OverrideGrainIntensity = overrideGrainIntensity;
                OverrideGrainJitter = overrideGrainJitter;
                OverrideDepthOfFieldFocalDistance = overrideDepthOfFieldFocalDistance;
                OverrideDepthOfFieldFstop = overrideDepthOfFieldFstop;
                OverrideDepthOfFieldMinFstop = overrideDepthOfFieldMinFstop;
                OverrideDepthOfFieldBladeCount = overrideDepthOfFieldBladeCount;
                OverrideDepthOfFieldDepthBlurRadius = overrideDepthOfFieldDepthBlurRadius;
                OverrideDepthOfFieldDepthBlurAmount = overrideDepthOfFieldDepthBlurAmount;
                OverrideDepthOfFieldFocalRegion = overrideDepthOfFieldFocalRegion;
                OverrideDepthOfFieldNearTransitionRegion = overrideDepthOfFieldNearTransitionRegion;
                OverrideDepthOfFieldFarTransitionRegion = overrideDepthOfFieldFarTransitionRegion;
                OverrideDepthOfFieldScale = overrideDepthOfFieldScale;
                OverrideDepthOfFieldNearBlurSize = overrideDepthOfFieldNearBlurSize;
                OverrideDepthOfFieldFarBlurSize = overrideDepthOfFieldFarBlurSize;
                OverrideDepthOfFieldOcclusion = overrideDepthOfFieldOcclusion;
                OverrideDepthOfFieldSkyFocusDistance = overrideDepthOfFieldSkyFocusDistance;
                OverrideDepthOfFieldVignetteSize = overrideDepthOfFieldVignetteSize;
                OverrideMobileHQGaussian = overrideMobileHQGaussian;

                SceneFringeIntensity = sceneFringeIntensity;
                ChromaticAberrationStartOffset = chromaticAberrationStartOffset;
                BloomMethod = bloomMethod;
                BloomIntensity = bloomIntensity;
                BloomThreshold = bloomThreshold;
                BloomConvolutionSize = bloomConvolutionSize;
                BloomConvolutionTexture = bloomConvolutionTexture;
                BloomConvolutionCenterUV = bloomConvolutionCenterUV;
                BloomConvolutionPreFilterMin = bloomConvolutionPreFilterMin;
                BloomConvolutionPreFilterMax = bloomConvolutionPreFilterMax;
                BloomConvolutionPreFilterMult = bloomConvolutionPreFilterMult;
                BloomConvolutionBufferScale = bloomConvolutionBufferScale;
                BloomDirtMask = bloomDirtMask;
                BloomDirtMaskIntensity = bloomDirtMaskIntensity;
                BloomDirtMaskTint = bloomDirtMaskTint;
                DepthOfFieldFstop = depthOfFieldFstop;
                DepthOfFieldMinFstop = depthOfFieldMinFstop;
                DepthOfFieldBladeCount = depthOfFieldBladeCount;
                DepthOfFieldOcclusion = depthOfFieldOcclusion;
                DepthOfFieldFocalDistance = depthOfFieldFocalDistance;
                DepthOfFieldDepthBlurAmount = depthOfFieldDepthBlurAmount;
                DepthOfFieldDepthBlurRadius = depthOfFieldDepthBlurRadius;
                DepthOfFieldFocalRegion = depthOfFieldFocalRegion;
                DepthOfFieldNearTransitionRegion = depthOfFieldNearTransitionRegion;
                DepthOfFieldFarTransitionRegion = depthOfFieldFarTransitionRegion;
                AutoExposureMethod = autoExposureMethod;
                AutoExposureBias = autoExposureBias;
                AutoExposureBiasBackup = autoExposureBiasBackup;
                AutoExposureApplyPhysicalCameraExposure = autoExposureApplyPhysicalCameraExposure;
                AutoExposureBiasCurve = autoExposureBiasCurve;
                AutoExposureMeterMask = autoExposureMeterMask;
                AutoExposureMinBrightness = autoExposureMinBrightness;
                AutoExposureMaxBrightness = autoExposureMaxBrightness;
                AutoExposureSpeedUp = autoExposureSpeedUp;
                AutoExposureSpeedDown = autoExposureSpeedDown;
                LensFlareIntensity = lensFlareIntensity;
                LensFlareTint = lensFlareTint;
                LensFlareBokehSize = lensFlareBokehSize;
                LensFlareThreshold = lensFlareThreshold;
                LensFlareBokehShape = lensFlareBokehShape;
                VignetteIntensity = vignetteIntensity;
                GrainJitter = grainJitter;
                GrainIntensity = grainIntensity;
                MobileHQGaussian = mobileHQGaussian;
            }
        }

        public class RenderingFeaturesSettings
        {
            public bool OverrideAmbientCubemapTint { get; }
            public bool OverrideAmbientCubemapIntensity { get; }
            public bool OverrideAmbientOcclusionIntensity { get; }
            public bool OverrideAmbientOcclusionStaticFraction { get; }
            public bool OverrideAmbientOcclusionRadius { get; }
            public bool OverrideAmbientOcclusionFadeDistance { get; }
            public bool OverrideAmbientOcclusionFadeRadius { get; }
            public bool OverrideAmbientOcclusionRadiusInWS { get; }
            public bool OverrideAmbientOcclusionPower { get; }
            public bool OverrideAmbientOcclusionBias { get; }
            public bool OverrideAmbientOcclusionQuality { get; }
            public bool OverrideAmbientOcclusionMipBlend { get; }
            public bool OverrideAmbientOcclusionMipScale { get; }
            public bool OverrideAmbientOcclusionMipThreshold { get; }
            public bool OverrideAmbientOcclusionTemporalBlendWeight { get; }
            public bool OverrideScreenPercentage { get; }
            public bool OverrideReflectionsType { get; }
            public bool OverrideTranslucencyType { get; }
            public bool OverrideScreenSpaceReflectionIntensity { get; }
            public bool OverrideScreenSpaceReflectionQuality { get; }
            public bool OverrideScreenSpaceReflectionMaxRoughness { get; }

            public Vector4 AmbientCubemapTint { get; }
            public float AmbientCubemapIntensity { get; }
            public ResourceReference AmbientCubemap { get; }
            public float AmbientOcclusionIntensity { get; }
            public float AmbientOcclusionStaticFraction { get; }
            public float AmbientOcclusionRadius { get; }
            public float AmbientOcclusionRadiusInWS { get; }
            public float AmbientOcclusionFadeDistance { get; }
            public float AmbientOcclusionFadeRadius { get; }
            public float AmbientOcclusionPower { get; }
            public float AmbientOcclusionBias { get; }
            public float AmbientOcclusionQuality { get; }
            public float AmbientOcclusionMipBlend { get; }
            public float AmbientOcclusionMipScale { get; }
            public float AmbientOcclusionMipThreshold { get; }
            public float AmbientOcclusionTemporalBlendWeight { get; }
            public float ScreenPercentage { get; }
            public ReflectionsType ReflectionsType { get; }
            public TranslucencyType TranslucencyType { get; }
            public float ScreenSpaceReflectionIntensity { get; }
            public float ScreenSpaceReflectionQuality { get; }
            public float ScreenSpaceReflectionMaxRoughness { get; }

            public RenderingFeaturesSettings(
                bool overrideAmbientCubemapTint,
                bool overrideAmbientCubemapIntensity,
                bool overrideAmbientOcclusionIntensity,
                bool overrideAmbientOcclusionStaticFraction,
                bool overrideAmbientOcclusionRadius,
                bool overrideAmbientOcclusionFadeDistance,
                bool overrideAmbientOcclusionFadeRadius,
                bool overrideAmbientOcclusionRadiusInWS,
                bool overrideAmbientOcclusionPower,
                bool overrideAmbientOcclusionBias,
                bool overrideAmbientOcclusionQuality,
                bool overrideAmbientOcclusionMipBlend,
                bool overrideAmbientOcclusionMipScale,
                bool overrideAmbientOcclusionMipThreshold,
                bool overrideAmbientOcclusionTemporalBlendWeight,
                bool overrideScreenPercentage,
                bool overrideReflectionsType,
                bool overrideTranslucencyType,
                bool overrideScreenSpaceReflectionIntensity,
                bool overrideScreenSpaceReflectionQuality,
                bool overrideScreenSpaceReflectionMaxRoughness,
                Vector4 ambientCubemapTint,
                float ambientCubemapIntensity,
                ResourceReference ambientCubemap,
                float ambientOcclusionIntensity,
                float ambientOcclusionStaticFraction,
                float ambientOcclusionRadius,
                float ambientOcclusionRadiusInWS,
                float ambientOcclusionFadeDistance,
                float ambientOcclusionFadeRadius,
                float ambientOcclusionPower,
                float ambientOcclusionBias,
                float ambientOcclusionQuality,
                float ambientOcclusionMipBlend,
                float ambientOcclusionMipScale,
                float ambientOcclusionMipThreshold,
                float ambientOcclusionTemporalBlendWeight,
                float screenPercentage,
                ReflectionsType reflectionsType,
                TranslucencyType translucencyType,
                float screenSpaceReflectionIntensity,
                float screenSpaceReflectionQuality,
                float screenSpaceReflectionMaxRoughness
            )
            {
                
                OverrideAmbientCubemapTint = overrideAmbientCubemapTint;
                OverrideAmbientCubemapIntensity = overrideAmbientCubemapIntensity;
                OverrideAmbientOcclusionIntensity = overrideAmbientOcclusionIntensity;
                OverrideAmbientOcclusionStaticFraction = overrideAmbientOcclusionStaticFraction;
                OverrideAmbientOcclusionRadius = overrideAmbientOcclusionRadius;
                OverrideAmbientOcclusionFadeDistance = overrideAmbientOcclusionFadeDistance;
                OverrideAmbientOcclusionFadeRadius = overrideAmbientOcclusionFadeRadius;
                OverrideAmbientOcclusionRadiusInWS = overrideAmbientOcclusionRadiusInWS;
                OverrideAmbientOcclusionPower = overrideAmbientOcclusionPower;
                OverrideAmbientOcclusionBias = overrideAmbientOcclusionBias;
                OverrideAmbientOcclusionQuality = overrideAmbientOcclusionQuality;
                OverrideAmbientOcclusionMipBlend = overrideAmbientOcclusionMipBlend;
                OverrideAmbientOcclusionMipScale = overrideAmbientOcclusionMipScale;
                OverrideAmbientOcclusionMipThreshold = overrideAmbientOcclusionMipThreshold;
                OverrideAmbientOcclusionTemporalBlendWeight = overrideAmbientOcclusionTemporalBlendWeight;
                OverrideScreenPercentage = overrideScreenPercentage;
                OverrideReflectionsType = overrideReflectionsType;
                OverrideTranslucencyType = overrideTranslucencyType;
                OverrideScreenSpaceReflectionIntensity = overrideScreenSpaceReflectionIntensity;
                OverrideScreenSpaceReflectionQuality = overrideScreenSpaceReflectionQuality;
                OverrideScreenSpaceReflectionMaxRoughness = overrideScreenSpaceReflectionMaxRoughness;

                AmbientCubemapTint = ambientCubemapTint;
                AmbientCubemapIntensity = ambientCubemapIntensity;
                AmbientCubemap = ambientCubemap;
                AmbientOcclusionIntensity = ambientOcclusionIntensity;
                AmbientOcclusionStaticFraction = ambientOcclusionStaticFraction;
                AmbientOcclusionRadius = ambientOcclusionRadius;
                AmbientOcclusionRadiusInWS = ambientOcclusionRadiusInWS;
                AmbientOcclusionFadeDistance = ambientOcclusionFadeDistance;
                AmbientOcclusionFadeRadius = ambientOcclusionFadeRadius;
                AmbientOcclusionPower = ambientOcclusionPower;
                AmbientOcclusionBias = ambientOcclusionBias;
                AmbientOcclusionQuality = ambientOcclusionQuality;
                AmbientOcclusionMipBlend = ambientOcclusionMipBlend;
                AmbientOcclusionMipScale = ambientOcclusionMipScale;
                AmbientOcclusionMipThreshold = ambientOcclusionMipThreshold;
                AmbientOcclusionTemporalBlendWeight = ambientOcclusionTemporalBlendWeight;
                ScreenPercentage = screenPercentage;
                ReflectionsType = reflectionsType;
                TranslucencyType = translucencyType;
                ScreenSpaceReflectionIntensity = screenSpaceReflectionIntensity;
                ScreenSpaceReflectionQuality = screenSpaceReflectionQuality;
                ScreenSpaceReflectionMaxRoughness = screenSpaceReflectionMaxRoughness;
            }
        }

        public class RayTracingSettings
        {
            public bool OverrideRayTracingAO { get; }
            public bool OverrideRayTracingAOSamplesPerPixel { get; }
            public bool OverrideRayTracingAOIntensity { get; }
            public bool OverrideRayTracingAORadius { get; }
            public bool OverrideRayTracingReflectionsMaxRoughness { get; }
            public bool OverrideRayTracingReflectionsMaxBounces { get; }
            public bool OverrideRayTracingReflectionsSamplesPerPixel { get; }
            public bool OverrideRayTracingReflectionsShadows { get; }
            public bool OverrideRayTracingReflectionsTranslucency { get; }
            public bool OverrideRayTracingTranslucencyMaxRoughness { get; }
            public bool OverrideRayTracingTranslucencyRefractionRays { get; }
            public bool OverrideRayTracingTranslucencySamplesPerPixel { get; }
            public bool OverrideRayTracingTranslucencyShadows { get; }
            public bool OverrideRayTracingTranslucencyRefraction { get; }
            public bool OverrideRayTracingGI { get; }
            public bool OverrideRayTracingGIMaxBounces { get; }
            public bool OverrideRayTracingGISamplesPerPixel { get; }
            public bool RayTracingAO { get; }
            public int RayTracingAOSamplesPerPixel { get; }
            public float RayTracingAOIntensity { get; }
            public float RayTracingAORadius { get; }
            public RayTracingGlobalIlluminationType RayTracingGIType { get; }
            public int RayTracingGIMaxBounces { get; }
            public int RayTracingGISamplesPerPixel { get; }
            public float RayTracingReflectionsMaxRoughness { get; }
            public int RayTracingReflectionsMaxBounces { get; }
            public int RayTracingReflectionsSamplesPerPixel { get; }
            public RayTracingShadows RayTracingReflectionsShadows { get; }
            public bool RayTracingReflectionsTranslucency { get; }
            public float RayTracingTranslucencyMaxRoughness { get; }
            public int RayTracingTranslucencyRefractionRays { get; }
            public int RayTracingTranslucencySamplesPerPixel { get; }
            public RayTracingShadows RayTracingTranslucencyShadows { get; }
            public bool RayTracingTranslucencyRefraction { get; }
            
            public RayTracingSettings(
                bool overrideRayTracingAO,
                bool overrideRayTracingAOSamplesPerPixel,
                bool overrideRayTracingAOIntensity,
                bool overrideRayTracingAORadius,
                bool overrideRayTracingReflectionsMaxRoughness,
                bool overrideRayTracingReflectionsMaxBounces,
                bool overrideRayTracingReflectionsSamplesPerPixel,
                bool overrideRayTracingReflectionsShadows,
                bool overrideRayTracingReflectionsTranslucency,
                bool overrideRayTracingTranslucencyMaxRoughness,
                bool overrideRayTracingTranslucencyRefractionRays,
                bool overrideRayTracingTranslucencySamplesPerPixel,
                bool overrideRayTracingTranslucencyShadows,
                bool overrideRayTracingTranslucencyRefraction,
                bool overrideRayTracingGI,
                bool overrideRayTracingGIMaxBounces,
                bool overrideRayTracingGISamplesPerPixel,
                bool rayTracingAO,
                int rayTracingAOSamplesPerPixel,
                float rayTracingAOIntensity,
                float rayTracingAORadius,
                RayTracingGlobalIlluminationType rayTracingGIType,
                int rayTracingGIMaxBounces,
                int rayTracingGISamplesPerPixel,
                float rayTracingReflectionsMaxRoughness,
                int rayTracingReflectionsMaxBounces,
                int rayTracingReflectionsSamplesPerPixel,
                RayTracingShadows rayTracingReflectionsShadows,
                bool rayTracingReflectionsTranslucency,
                float rayTracingTranslucencyMaxRoughness,
                int rayTracingTranslucencyRefractionRays,
                int rayTracingTranslucencySamplesPerPixel,
                RayTracingShadows rayTracingTranslucencyShadows,
                bool rayTracingTranslucencyRefraction
            )
            {
                OverrideRayTracingAO = overrideRayTracingAO;
                OverrideRayTracingAOSamplesPerPixel = overrideRayTracingAOSamplesPerPixel;
                OverrideRayTracingAOIntensity = overrideRayTracingAOIntensity;
                OverrideRayTracingAORadius = overrideRayTracingAORadius;
                OverrideRayTracingReflectionsMaxRoughness = overrideRayTracingReflectionsMaxRoughness;
                OverrideRayTracingReflectionsMaxBounces = overrideRayTracingReflectionsMaxBounces;
                OverrideRayTracingReflectionsSamplesPerPixel = overrideRayTracingReflectionsSamplesPerPixel;
                OverrideRayTracingReflectionsShadows = overrideRayTracingReflectionsShadows;
                OverrideRayTracingReflectionsTranslucency = overrideRayTracingReflectionsTranslucency;
                OverrideRayTracingTranslucencyMaxRoughness = overrideRayTracingTranslucencyMaxRoughness;
                OverrideRayTracingTranslucencyRefractionRays = overrideRayTracingTranslucencyRefractionRays;
                OverrideRayTracingTranslucencySamplesPerPixel = overrideRayTracingTranslucencySamplesPerPixel;
                OverrideRayTracingTranslucencyShadows = overrideRayTracingTranslucencyShadows;
                OverrideRayTracingTranslucencyRefraction = overrideRayTracingTranslucencyRefraction;
                OverrideRayTracingGI = overrideRayTracingGI;
                OverrideRayTracingGIMaxBounces = overrideRayTracingGIMaxBounces;
                OverrideRayTracingGISamplesPerPixel = overrideRayTracingGISamplesPerPixel;
                RayTracingAO = rayTracingAO;
                RayTracingAOSamplesPerPixel = rayTracingAOSamplesPerPixel;
                RayTracingAOIntensity = rayTracingAOIntensity;
                RayTracingAORadius = rayTracingAORadius;
                RayTracingGIType = rayTracingGIType;
                RayTracingGIMaxBounces = rayTracingGIMaxBounces;
                RayTracingGISamplesPerPixel = rayTracingGISamplesPerPixel;
                RayTracingReflectionsMaxRoughness = rayTracingReflectionsMaxRoughness;
                RayTracingReflectionsMaxBounces = rayTracingReflectionsMaxBounces;
                RayTracingReflectionsSamplesPerPixel = rayTracingReflectionsSamplesPerPixel;
                RayTracingReflectionsShadows = rayTracingReflectionsShadows;
                RayTracingReflectionsTranslucency = rayTracingReflectionsTranslucency;
                RayTracingTranslucencyMaxRoughness = rayTracingTranslucencyMaxRoughness;
                RayTracingTranslucencyRefractionRays = rayTracingTranslucencyRefractionRays;
                RayTracingTranslucencySamplesPerPixel = rayTracingTranslucencySamplesPerPixel;
                RayTracingTranslucencyShadows = rayTracingTranslucencyShadows;
                RayTracingTranslucencyRefraction = rayTracingTranslucencyRefraction;
            }
        }

        public class PathTracingSettings
        {
            public bool OverridePathTracingMaxBounces { get; }
            public bool OverridePathTracingSamplesPerPixel { get; }
            public bool OverridePathTracingFilterWidth { get; }
            public bool OverridePathTracingEnableEmissive { get; }
            public bool OverridePathTracingMaxPathExposure { get; }
            public bool OverridePathTracingEnableDenoiser { get; }
            
            public int PathTracingMaxBounces { get; }
            public int PathTracingSamplesPerPixel { get; }
            public float PathTracingFilterWidth { get; }
            public bool PathTracingEnableEmissive { get; }
            public float PathTracingMaxPathExposure { get; }
            public bool PathTracingEnableDenoiser { get; }
            
            public PathTracingSettings(
                bool overridePathTracingMaxBounces,
                bool overridePathTracingSamplesPerPixel,
                bool overridePathTracingFilterWidth,
                bool overridePathTracingEnableEmissive,
                bool overridePathTracingMaxPathExposure,
                bool overridePathTracingEnableDenoiser,
                int pathTracingMaxBounces,
                int pathTracingSamplesPerPixel,
                float pathTracingFilterWidth,
                bool pathTracingEnableEmissive,
                float pathTracingMaxPathExposure,
                bool pathTracingEnableDenoiser
            )
            {
                
                OverridePathTracingMaxBounces = overridePathTracingMaxBounces;
                OverridePathTracingSamplesPerPixel = overridePathTracingSamplesPerPixel;
                OverridePathTracingFilterWidth = overridePathTracingFilterWidth;
                OverridePathTracingEnableEmissive = overridePathTracingEnableEmissive;
                OverridePathTracingMaxPathExposure = overridePathTracingMaxPathExposure;
                OverridePathTracingEnableDenoiser = overridePathTracingEnableDenoiser;

                PathTracingMaxBounces = pathTracingMaxBounces;
                PathTracingSamplesPerPixel = pathTracingSamplesPerPixel;
                PathTracingFilterWidth = pathTracingFilterWidth;
                PathTracingEnableEmissive = pathTracingEnableEmissive;
                PathTracingMaxPathExposure = pathTracingMaxPathExposure;
                PathTracingEnableDenoiser = pathTracingEnableDenoiser;
            }
        }

        public class MotionBlurSettings
        {
            public bool OverrideMotionBlurAmount { get; }
            public bool OverrideMotionBlurMax { get; }
            public bool OverrideMotionBlurTargetFPS { get; }
            public bool OverrideMotionBlurPerObjectSize { get; }
            public float MotionBlurAmount { get; }
            public float MotionBlurMax { get; }
            public float MotionBlurTargetFPS { get; }
            public float MotionBlurPerObjectSize { get; }
            
            public MotionBlurSettings(
                bool overrideMotionBlurAmount,
                bool overrideMotionBlurMax,
                bool overrideMotionBlurTargetFPS,
                bool overrideMotionBlurPerObjectSize,
                float motionBlurAmount,
                float motionBlurMax,
                float motionBlurTargetFPS,
                float motionBlurPerObjectSize
            )
            {
                OverrideMotionBlurAmount = overrideMotionBlurAmount;
                OverrideMotionBlurMax = overrideMotionBlurMax;
                OverrideMotionBlurTargetFPS = overrideMotionBlurTargetFPS;
                OverrideMotionBlurPerObjectSize = overrideMotionBlurPerObjectSize;

                MotionBlurAmount = motionBlurAmount;
                MotionBlurMax = motionBlurMax;
                MotionBlurTargetFPS = motionBlurTargetFPS;
                MotionBlurPerObjectSize = motionBlurPerObjectSize;
            }
        }

        public class LightPropagationVolumeSettings
        {
            public bool OverrideLPVIntensity { get; }
            public bool OverrideLPVDirectionalOcclusionIntensity { get; }
            public bool OverrideLPVDirectionalOcclusionRadius { get; }
            public bool OverrideLPVDiffuseOcclusionExponent { get; }
            public bool OverrideLPVSpecularOcclusionExponent { get; }
            public bool OverrideLPVDiffuseOcclusionIntensity { get; }
            public bool OverrideLPVSpecularOcclusionIntensity { get; }
            public bool OverrideLPVSize { get; }
            public bool OverrideLPVSecondaryOcclusionIntensity { get; }
            public bool OverrideLPVSecondaryBounceIntensity { get; }
            public bool OverrideLPVGeometryVolumeBias { get; }
            public bool OverrideLPVVplInjectionBias { get; }
            public bool OverrideLPVEmissiveInjectionIntensity { get; }
            public bool OverrideLPVFadeRange { get; }
            public bool OverrideLPVDirectionalOcclusionFadeRange { get; }
            public float LPVIntensity { get; }
            public float LPVVplInjectionBias { get; }
            public float LPVSize { get; }
            public float LPVSecondaryOcclusionIntensity { get; }
            public float LPVSecondaryBounceIntensity { get; }
            public float LPVGeometryVolumeBias { get; }
            public float LPVEmissiveInjectionIntensity { get; }
            public float LPVDirectionalOcclusionIntensity { get; }
            public float LPVDirectionalOcclusionRadius { get; }
            public float LPVDiffuseOcclusionExponent { get; }
            public float LPVSpecularOcclusionExponent { get; }
            public float LPVDiffuseOcclusionIntensity { get; }
            public float LPVSpecularOcclusionIntensity { get; }
            public float LPVFadeRange { get; }
            public float LPVDirectionalOcclusionFadeRange { get; }
            
            public LightPropagationVolumeSettings(
                bool overrideLPVIntensity,
                bool overrideLPVDirectionalOcclusionIntensity,
                bool overrideLPVDirectionalOcclusionRadius,
                bool overrideLPVDiffuseOcclusionExponent,
                bool overrideLPVSpecularOcclusionExponent,
                bool overrideLPVDiffuseOcclusionIntensity,
                bool overrideLPVSpecularOcclusionIntensity,
                bool overrideLPVSize,
                bool overrideLPVSecondaryOcclusionIntensity,
                bool overrideLPVSecondaryBounceIntensity,
                bool overrideLPVGeometryVolumeBias,
                bool overrideLPVVplInjectionBias,
                bool overrideLPVEmissiveInjectionIntensity,
                bool overrideLPVFadeRange,
                bool overrideLPVDirectionalOcclusionFadeRange,
                float lpvIntensity,
                float lpvVplInjectionBias,
                float lpvSize,
                float lpvSecondaryOcclusionIntensity,
                float lpvSecondaryBounceIntensity,
                float lpvGeometryVolumeBias,
                float lpvEmissiveInjectionIntensity,
                float lpvDirectionalOcclusionIntensity,
                float lpvDirectionalOcclusionRadius,
                float lpvDiffuseOcclusionExponent,
                float lpvSpecularOcclusionExponent,
                float lpvDiffuseOcclusionIntensity,
                float lpvSpecularOcclusionIntensity,
                float lpvFadeRange,
                float lpvDirectionalOcclusionFadeRange
            )
            {
                OverrideLPVIntensity = overrideLPVIntensity;
                OverrideLPVDirectionalOcclusionIntensity = overrideLPVDirectionalOcclusionIntensity;
                OverrideLPVDirectionalOcclusionRadius = overrideLPVDirectionalOcclusionRadius;
                OverrideLPVDiffuseOcclusionExponent = overrideLPVDiffuseOcclusionExponent;
                OverrideLPVSpecularOcclusionExponent = overrideLPVSpecularOcclusionExponent;
                OverrideLPVDiffuseOcclusionIntensity = overrideLPVDiffuseOcclusionIntensity;
                OverrideLPVSpecularOcclusionIntensity = overrideLPVSpecularOcclusionIntensity;
                OverrideLPVSize = overrideLPVSize;
                OverrideLPVSecondaryOcclusionIntensity = overrideLPVSecondaryOcclusionIntensity;
                OverrideLPVSecondaryBounceIntensity = overrideLPVSecondaryBounceIntensity;
                OverrideLPVGeometryVolumeBias = overrideLPVGeometryVolumeBias;
                OverrideLPVVplInjectionBias = overrideLPVVplInjectionBias;
                OverrideLPVEmissiveInjectionIntensity = overrideLPVEmissiveInjectionIntensity;
                OverrideLPVFadeRange = overrideLPVFadeRange;
                OverrideLPVDirectionalOcclusionFadeRange = overrideLPVDirectionalOcclusionFadeRange;

                LPVIntensity = lpvIntensity;
                LPVVplInjectionBias = lpvVplInjectionBias;
                LPVSize = lpvSize;
                LPVSecondaryOcclusionIntensity = lpvSecondaryOcclusionIntensity;
                LPVSecondaryBounceIntensity = lpvSecondaryBounceIntensity;
                LPVGeometryVolumeBias = lpvGeometryVolumeBias;
                LPVEmissiveInjectionIntensity = lpvEmissiveInjectionIntensity;
                LPVDirectionalOcclusionIntensity = lpvDirectionalOcclusionIntensity;
                LPVDirectionalOcclusionRadius = lpvDirectionalOcclusionRadius;
                LPVDiffuseOcclusionExponent = lpvDiffuseOcclusionExponent;
                LPVSpecularOcclusionExponent = lpvSpecularOcclusionExponent;
                LPVDiffuseOcclusionIntensity = lpvDiffuseOcclusionIntensity;
                LPVSpecularOcclusionIntensity = lpvSpecularOcclusionIntensity;
                LPVFadeRange = lpvFadeRange;
                LPVDirectionalOcclusionFadeRange = lpvDirectionalOcclusionFadeRange;
            }
        }

        public class GlobalIlluminationSettings
        {
            public bool OverrideIndirectLightingColor { get; }
            public bool OverrideIndirectLightingIntensity { get; }
            public Vector4 IndirectLightingColor { get; }
            public float IndirectLightingIntensity { get; }

            public GlobalIlluminationSettings(
                bool overrideIndirectLightingColor,
                bool overrideIndirectLightingIntensity,
                Vector4 indirectLightingColor,
                float indirectLightingIntensity
            )
            {
                OverrideIndirectLightingColor = overrideIndirectLightingColor;
                OverrideIndirectLightingIntensity = overrideIndirectLightingIntensity;
                IndirectLightingColor = indirectLightingColor;
                IndirectLightingIntensity = indirectLightingIntensity;
            }
        }
    }
}
