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
        AttributeList = 64,
        SamplerType = 128,
        ShadingModel = 256,
        String = 512,
        TextureReference = 1024,
        Vector4 = 2048,
    }
}
