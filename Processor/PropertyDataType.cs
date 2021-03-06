﻿using System;

namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    [Flags]
    public enum PropertyDataType
    {
        Array = 1,
        BlendMode = 2,
        Boolean = 4,
        ExpressionReference = 8,
        Float = 16,
        FunctionReference = 32,
        Integer = 64,
        AttributeList = 128,
        SamplerType = 256,
        ShadingModel = 512,
        String = 1024,
        ResourceReference = 2048,
        Vector4 = 4096,
        MaterialDomain = 8192,
        TranslucencyLightingMode = 16384,
        Vector3 = 32768,
        Rotator = 65536,
        Mobility = 131072,
    }
}
