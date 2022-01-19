using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class SkyLightComponent : BaseComponent, IMobility
    {
        public Mobility Mobility { get; }
        public SkyLightSourceType SourceType { get; }
        public ResourceReference Cubemap { get; }
        public float SourceCubemapAngle { get; }
        public int CubemapResolution { get; }
        public float SkyDistanceThreshold { get; }
        public bool CaptureEmissiveOnly { get; }
        public bool LowerHemisphereIsBlack { get; }
        public bool CloudAmbientOcclusion { get; }
        public float CloudAmbientOcclusionStrength { get; }
        public float CloudAmbientOcclusionExtent { get; }
        public float CloudAmbientOcclusionMapResolutionScale { get; }
        public float CloudAmbientOcclusionApertureScale { get; }
        public float Intensity { get; }
        public Vector4 LightColor { get; }
        public bool AffectsWorld { get; }
        public bool CastShadows { get; }
        public bool CastStaticShadows { get; }
        public bool CastDynamicShadows { get; }
        public bool AffectTranslucentLighting { get; }
        public bool Transmission { get; }
        public bool CastVolumetricShadow { get; }
        public bool CastDeepShadow { get; }
        public bool CastRaytracedShadow { get; }
        public bool AffectReflection { get; }
        public bool AffectGlobalIllumination { get; }
        public float DeepShadowLayerDistribution { get; }
        public float IndirectLightingIntensity { get; }
        public float VolumetricScatteringIntensity { get; }
        public int SamplesPerPixel { get; }

        public SkyLightComponent(
            string name,
            ResourceReference archetype,
            Vector3 relativeLocation,
            Rotator relativeRotation,
            Vector3 relativeScale3D,
            Node[] children,
            Mobility mobility,
            SkyLightSourceType sourceType,
            ResourceReference cubemap,
            float sourceCubemapAngle,
            int cubemapResolution,
            float skyDistanceThreshold,
            bool captureEmissiveOnly,
            bool lowerHemisphereIsBlack,
            bool cloudAmbientOcclusion,
            float cloudAmbientOcclusionStrength,
            float cloudAmbientOcclusionExtent,
            float cloudAmbientOcclusionMapResolutionScale,
            float cloudAmbientOcclusionApertureScale,
            float intensity,
            Vector4 lightColor,
            bool affectsWorld,
            bool castShadows,
            bool castStaticShadows,
            bool castDynamicShadows,
            bool affectTranslucentLighting,
            bool transmission,
            bool castVolumetricShadow,
            bool castDeepShadow,
            bool castRaytracedShadow,
            bool affectReflection,
            bool affectGlobalIllumination,
            float deepShadowLayerDistribution,
            float indirectLightingIntensity,
            float volumetricScatteringIntensity,
            int samplesPerPixel
        )
            : base(name, archetype, relativeLocation, relativeScale3D, relativeRotation, children)
        {
            Mobility = mobility;
            SourceType = sourceType;
            Cubemap = cubemap;
            SourceCubemapAngle = sourceCubemapAngle;
            CubemapResolution = cubemapResolution;
            SkyDistanceThreshold = skyDistanceThreshold;
            CaptureEmissiveOnly = captureEmissiveOnly;
            LowerHemisphereIsBlack = lowerHemisphereIsBlack;
            CloudAmbientOcclusion = cloudAmbientOcclusion;
            CloudAmbientOcclusionStrength = cloudAmbientOcclusionStrength;
            CloudAmbientOcclusionExtent = cloudAmbientOcclusionExtent;
            CloudAmbientOcclusionMapResolutionScale = cloudAmbientOcclusionMapResolutionScale;
            CloudAmbientOcclusionApertureScale = cloudAmbientOcclusionApertureScale;
            Intensity = intensity;
            LightColor = lightColor;
            AffectsWorld = affectsWorld;
            CastShadows = castShadows;
            CastStaticShadows = castStaticShadows;
            CastDynamicShadows = castDynamicShadows;
            AffectTranslucentLighting = affectTranslucentLighting;
            Transmission = transmission;
            CastVolumetricShadow = castVolumetricShadow;
            CastDeepShadow = castDeepShadow;
            CastRaytracedShadow = castRaytracedShadow;
            AffectReflection = affectReflection;
            AffectGlobalIllumination = affectGlobalIllumination;
            DeepShadowLayerDistribution = deepShadowLayerDistribution;
            IndirectLightingIntensity = indirectLightingIntensity;
            VolumetricScatteringIntensity = volumetricScatteringIntensity;
            SamplesPerPixel = samplesPerPixel;
        }
    }

    public class SkyLightComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.SkyLightComponent";

        public SkyLightComponentProcessor()
        {
            AddOptionalProperty("Mobility", PropertyDataType.Mobility);
            AddOptionalProperty("SourceType", PropertyDataType.String);
            AddOptionalProperty("Cubemap", PropertyDataType.ResourceReference);
            AddOptionalProperty("SourceCubemapAngle", PropertyDataType.Float);
            AddOptionalProperty("CubemapResolution", PropertyDataType.Integer);
            AddOptionalProperty("SkyDistanceThreshold", PropertyDataType.Float);
            AddOptionalProperty("bCaptureEmissiveOnly", PropertyDataType.Boolean);
            AddOptionalProperty("bLowerHemisphereIsBlack", PropertyDataType.Boolean);
            AddOptionalProperty("bCloudAmbientOcclusion", PropertyDataType.Boolean);
            AddOptionalProperty("CloudAmbientOcclusionStrength", PropertyDataType.Float);
            AddOptionalProperty("CloudAmbientOcclusionExtent", PropertyDataType.Float);
            AddOptionalProperty("CloudAmbientOcclusionMapResolutionScale", PropertyDataType.Float);
            AddOptionalProperty("CloudAmbientOcclusionApertureScale", PropertyDataType.Float);
            AddOptionalProperty("Intensity", PropertyDataType.Float);
            AddOptionalProperty("LightColor", PropertyDataType.Vector4);
            AddOptionalProperty("bAffectsWorld", PropertyDataType.Boolean);
            AddOptionalProperty("CastShadows", PropertyDataType.Boolean);
            AddOptionalProperty("CastStaticShadows", PropertyDataType.Boolean);
            AddOptionalProperty("CastDynamicShadows", PropertyDataType.Boolean);
            AddOptionalProperty("bAffectTranslucentLighting", PropertyDataType.Boolean);
            AddOptionalProperty("bTransmission", PropertyDataType.Boolean);
            AddOptionalProperty("bCastVolumetricShadow", PropertyDataType.Boolean);
            AddOptionalProperty("bCastDeepShadow", PropertyDataType.Boolean);
            AddOptionalProperty("bCastRaytracedShadow", PropertyDataType.Boolean);
            AddOptionalProperty("bAffectReflection", PropertyDataType.Boolean);
            AddOptionalProperty("bAffectGlobalIllumination", PropertyDataType.Boolean);
            AddOptionalProperty("DeepShadowLayerDistribution", PropertyDataType.Float);
            AddOptionalProperty("IndirectLightingIntensity", PropertyDataType.Float);
            AddOptionalProperty("VolumetricScatteringIntensity", PropertyDataType.Float);
            AddOptionalProperty("SamplesPerPixel", PropertyDataType.Integer);

            AddIgnoredProperty("LightGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new SkyLightComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                children,
                ValueUtil.ParseMobility(node.FindPropertyValue("Mobility")),
                ValueUtil.ParseSkyLightSourceType(node.FindPropertyValue("SourceType")),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Cubemap")),
                ValueUtil.ParseFloat(node.FindPropertyValue("SourceCubemapAngle")),
                ValueUtil.ParseInteger(node.FindPropertyValue("CubemapResolution") ?? "128"),
                ValueUtil.ParseFloat(node.FindPropertyValue("SkyDistanceThreshold") ?? "150000.0"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCaptureEmissiveOnly")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bLowerHemisphereIsBlack") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCloudAmbientOcclusion") ),
                ValueUtil.ParseFloat(node.FindPropertyValue("CloudAmbientOcclusionStrength") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("CloudAmbientOcclusionExtent") ?? "150.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("CloudAmbientOcclusionMapResolutionScale") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("CloudAmbientOcclusionApertureScale") ?? "0.05"),
                ValueUtil.ParseFloat(node.FindPropertyValue("Intensity") ?? "1.0"),
                ValueUtil.ParseVector4(node.FindPropertyValue("LightColor") ?? "(R=255,G=255,B=255,A=255)"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bAffectsWorld") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("CastShadows") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("CastStaticShadows") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("CastDynamicShadows") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bAffectTranslucentLighting")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bTransmission")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCastVolumetricShadow") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCastDeepShadow")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCastRaytracedShadow")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bAffectReflection") ?? "True"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bAffectGlobalIllumination") ?? "True"),
                ValueUtil.ParseFloat(node.FindPropertyValue("DeepShadowLayerDistribution") ?? "0.5"),
                ValueUtil.ParseFloat(node.FindPropertyValue("IndirectLightingIntensity") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("VolumetricScatteringIntensity") ?? "1.0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("SamplesPerPixel") ?? "4")
            );
        }
    }
}
