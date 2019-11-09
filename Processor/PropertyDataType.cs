using System;

namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    [Flags]
    public enum PropertyDataType
    {
        Array = 1,
        Boolean = 2,
        ExpressionReference = 4,
        Float = 8,
        FunctionReference = 16,
        Integer = 32,
        SamplerType = 64,
        ShadingModel = 128,
        String = 256,
        TextureReference = 512,
        Vector4 = 1024,
    }
}
