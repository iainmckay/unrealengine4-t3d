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
