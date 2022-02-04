namespace JollySamurai.UnrealEngine4.T3D
{
    public enum RayTracingGlobalIlluminationType
    {
        Unknown,
        Disabled,
        BruteForce,
        FinalGather
    }

    public enum ReflectionsType
    {
        Unknown,
        ScreenSpace,
        RayTracing
    }

    public enum RayTracingShadows
    {
        Unknown,
        HardShadows,
        AreaShadows,
        Disabled
    }

    public enum TranslucencyType
    {
        Unknown,
        RayTracing,
        Raster
    }

    public enum BloomMethod
    {
        Unknown,
        SumOfGuassian,
        FastFourierTransform
    }

    public enum AutoExposureMethod
    {
        Unknown,
        Histogram,
        Basic,
        Manual
    }

    public enum TemperatureType
    {
        Unknown,
        WhiteBalance,
        ColorTemperature
    }

    public enum SkyLightSourceType
    {
        Unknown,
        CapturedScene,
        SpecifiedCubemap
    }

    public enum DecalBlendMode
    {
        Translucent,
        Stain,
        Normal,
        Emissive,
        DBufferColorNormalRoughness,
        DBufferColor,
        DBufferColorNormal,
        DBufferColorRoughness,
        DBufferNormal,
        DBufferNormalRoughness,
        DBufferRoughness,
        DBufferEmissive,
        DBufferAlphaComposite,
        DBufferEmissiveAlphaComposite,
        VolumetricDistanceFunction,
        AlphaComposite,
        AmbientOcclusion
    }

    public enum WorldPositionIncludedOffsets
    {
        Default,
        ExcludeAllShaderOffsets,
        CameraRelative,
        CameraRelativeNoOffsets
    }

    public enum MaterialSceneAttributeInputMode
    {
        Coordinates,
        OffsetFraction
    }

    public enum SceneTextureId
    {
        SceneColor,
        SceneDepth,
        DiffuseColor,
        SpecularColor,
        SubsurfaceColor,
        BaseColor,
        Specular,
        Metallic,
        WorldNormal,
        SeparateTranslucency,
        Opacity,
        Roughness,
        MaterialAo,
        CustomDepth,
        PostProcessInput0,
        PostProcessInput1,
        PostProcessInput2,
        PostProcessInput3,
        PostProcessInput4,
        PostProcessInput5,
        PostProcessInput6,
        DecalMask,
        ShadingModelColor,
        ShadingModelId,
        AmbientOcclusion,
        CustomStencil,
        StoredBaseColor,
        StoredSpecular,
        Velocity,
        WorldTangent,
        Anisotropy,
    }

    public enum LandscapeLayerBlendType
    {
        WeightBlend,
        AlphaBlend,
        HeightBlend
    }

    public enum MaterialVectorCoordTransformSource
    {
        Tangent,
        Local,
        World,
        View,
        Camera,
        ParticleWorld
    }

    public enum MaterialVectorCoordTransform
    {
        Tangent,
        Local,
        World,
        View,
        Camera,
        ParticleWorld
    }

    public struct Vector2
    {
        public float X { get; }
        public float Y { get; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
