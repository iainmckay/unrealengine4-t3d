using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class ExponentialHeightFogComponent : BaseComponent, IMobility
    {
        public Mobility Mobility { get; }
        public float FogDensity { get; }
        public float FogHeightFalloff { get; }
        public Vector4 FogInscatteringColor { get; }
        public ResourceReference InscatteringColorCubemap { get; }
        public float InscatteringColorCubemapAngle { get; }
        public Vector4 InscatteringTextureTint { get; }
        public float FullyDirectionalInscatteringColorDistance { get; }
        public float FogMaxOpacity { get; }
        public float StartDistance { get; }
        public float FogCutoffDistance { get; }
        public bool EnableVolumetricFog { get; }
        public float VolumetricFogScatteringDistribution { get; }
        public Vector4 VolumetricFogAlbedo { get; }
        public Vector4 VolumetricFogEmissive { get; }
        public float VolumetricFogExtinctionScale { get; }
        public float VolumetricFogDistance { get; }
        public float VolumetricFogStaticLightingScatteringIntensity { get; }
        public bool OverrideLightColorsWithFogInscatteringColors { get; }

        public ExponentialHeightFogComponent(
            string name,
            ResourceReference archetype,
            Vector3 relativeLocation,
            Rotator relativeRotation,
            Vector3 relativeScale3D,
            Node[] children,
            Mobility mobility,
            float fogDensity,
            float fogHeightFalloff,
            Vector4 fogInscatteringColor,
            ResourceReference inscatteringColorCubemap,
            float inscatteringColorCubemapAngle,
            Vector4 inscatteringTextureTint,
            float fullyDirectionalInscatteringColorDistance,
            float fogMaxOpacity,
            float startDistance,
            float fogCutoffDistance,
            bool enableVolumetricFog,
            float volumetricFogScatteringDistribution,
            Vector4 volumetricFogAlbedo,
            Vector4 volumetricFogEmissive,
            float volumetricFogExtinctionScale,
            float volumetricFogDistance,
            float volumetricFogStaticLightingScatteringIntensity,
            bool overrideLightColorsWithFogInscatteringColors
        )
            : base(name, archetype, relativeLocation, relativeScale3D, relativeRotation, children)
        {
            Mobility = mobility;
            FogDensity = fogDensity;
            FogHeightFalloff = fogHeightFalloff;
            FogInscatteringColor = fogInscatteringColor;
            InscatteringColorCubemap = inscatteringColorCubemap;
            InscatteringColorCubemapAngle = inscatteringColorCubemapAngle;
            InscatteringTextureTint = inscatteringTextureTint;
            FullyDirectionalInscatteringColorDistance = fullyDirectionalInscatteringColorDistance;
            FogMaxOpacity = fogMaxOpacity;
            StartDistance = startDistance;
            FogCutoffDistance = fogCutoffDistance;
            EnableVolumetricFog = enableVolumetricFog;
            VolumetricFogScatteringDistribution = volumetricFogScatteringDistribution;
            VolumetricFogAlbedo = volumetricFogAlbedo;
            VolumetricFogEmissive = volumetricFogEmissive;
            VolumetricFogExtinctionScale = volumetricFogExtinctionScale;
            VolumetricFogDistance = volumetricFogDistance;
            VolumetricFogStaticLightingScatteringIntensity = volumetricFogStaticLightingScatteringIntensity;
            OverrideLightColorsWithFogInscatteringColors = overrideLightColorsWithFogInscatteringColors;
        }
    }

    public class ExponentialHeightFogComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.ExponentialHeightFogComponent";

        public ExponentialHeightFogComponentProcessor()
        {
            AddOptionalProperty("Mobility", PropertyDataType.Mobility);
            AddOptionalProperty("FogDensity", PropertyDataType.Float);
            AddOptionalProperty("FogHeightFalloff", PropertyDataType.Float);
            AddOptionalProperty("FogInscatteringColor", PropertyDataType.Vector4);
            AddOptionalProperty("InscatteringColorCubemap", PropertyDataType.ResourceReference);
            AddOptionalProperty("InscatteringColorCubemapAngle", PropertyDataType.Float);
            AddOptionalProperty("InscatteringTextureTint", PropertyDataType.Vector4);
            AddOptionalProperty("FullyDirectionalInscatteringColorDistance", PropertyDataType.Float);
            AddOptionalProperty("FogMaxOpacity", PropertyDataType.Float);
            AddOptionalProperty("StartDistance", PropertyDataType.Float);
            AddOptionalProperty("FogCutoffDistance", PropertyDataType.Float);
            AddOptionalProperty("bEnableVolumetricFog", PropertyDataType.Boolean);
            AddOptionalProperty("VolumetricFogScatteringDistribution", PropertyDataType.Float);
            AddOptionalProperty("VolumetricFogAlbedo", PropertyDataType.Vector4);
            AddOptionalProperty("VolumetricFogEmissive", PropertyDataType.Vector4);
            AddOptionalProperty("VolumetricFogExtinctionScale", PropertyDataType.Float);
            AddOptionalProperty("VolumetricFogDistance", PropertyDataType.Float);
            AddOptionalProperty("VolumetricFogStaticLightingScatteringIntensity", PropertyDataType.Float);
            AddOptionalProperty("bOverrideLightColorsWithFogInscatteringColors", PropertyDataType.Boolean);

            AddIgnoredProperty("LightGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new ExponentialHeightFogComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                children,
                ValueUtil.ParseMobility(node.FindPropertyValue("Mobility")),
                ValueUtil.ParseFloat(node.FindPropertyValue("FogDensity") ?? "0.02"),
                ValueUtil.ParseFloat(node.FindPropertyValue("FogHeightFalloff") ?? "0.2"),
                ValueUtil.ParseVector4(node.FindPropertyValue("FogInscatteringColor") ?? "(R=0.447,G=0.638,B=1.0,A=1.0)"),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("InscatteringColorCubemap")),
                ValueUtil.ParseFloat(node.FindPropertyValue("InscatteringColorCubemapAngle")),
                ValueUtil.ParseVector4(node.FindPropertyValue("InscatteringTextureTint") ?? "(R=1.354348,G=1.0,B=1.0,A=1.0)"),
                ValueUtil.ParseFloat(node.FindPropertyValue("FullyDirectionalInscatteringColorDistance") ?? "670857.125"),
                ValueUtil.ParseFloat(node.FindPropertyValue("FogMaxOpacity") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("StartDistance")),
                ValueUtil.ParseFloat(node.FindPropertyValue("FogCutoffDistance")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bEnableVolumetricFog")),
                ValueUtil.ParseFloat(node.FindPropertyValue("VolumetricFogScatteringDistribution") ?? "0.2"),
                ValueUtil.ParseVector4(node.FindPropertyValue("VolumetricFogAlbedo") ?? "(R=255,G=255,B=255,A=255)"),
                ValueUtil.ParseVector4(node.FindPropertyValue("VolumetricFogEmissive")),
                ValueUtil.ParseFloat(node.FindPropertyValue("VolumetricFogExtinctionScale") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("VolumetricFogDistance") ?? "6000.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("VolumetricFogStaticLightingScatteringIntensity") ?? "1.0"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bOverrideLightColorsWithFogInscatteringColors"))
            );
        }
    }
}
