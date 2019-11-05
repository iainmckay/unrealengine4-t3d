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
        String = 128,
        TextureReference = 256,
        Vector4 = 512,
    }
}
