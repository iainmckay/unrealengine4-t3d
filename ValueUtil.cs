using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using JollySamurai.UnrealEngine4.T3D.Exception;
using JollySamurai.UnrealEngine4.T3D.Material;
using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D
{
    public static class ValueUtil
    {
        public static readonly Regex ExpressionReferenceRegex = new Regex(@"(?<type>[a-zA-Z0-9]+)'""(?<material>\w+:)?(?<object>.+)""'", RegexOptions.Compiled);
        public static readonly Regex ResourceReferenceRegex = new Regex(@"^(?<type>.+)'""(?<resource>.+)""'$|^(?<type>.+)'(?<resource>.+)'$", RegexOptions.Compiled);
        public static readonly Regex Vector3Regex = new Regex(@"\(X=(\-{0,}[0-9]+\.[0-9]+),Y=(\-{0,}[0-9]+\.[0-9]+),Z=(\-{0,}[0-9]+\.[0-9]+)\)", RegexOptions.Compiled);
        public static readonly Regex Vector4Regex = new Regex(@"\(([RGBA])=([0-9\.]+),([RGBA])=([0-9\.]+),([RGBA])=([0-9\.]+),([RGBA])=([0-9\.]+)\)", RegexOptions.Compiled);
        public static readonly Regex RotatorRegex = new Regex(@"\(Pitch=(\-{0,}[0-9]+\.[0-9]+),Yaw=(\-{0,}[0-9]+\.[0-9]+),Roll=(\-{0,}[0-9]+\.[0-9]+)\)", RegexOptions.Compiled);

        public static bool ParseBoolean(string value)
        {
            if (null == value) {
                return false;
            }

            return Boolean.Parse(value);
        }

        public static bool TryParseBoolean(string value, out bool successOrFailure)
        {
            successOrFailure = Boolean.TryParse(value, out var result);

            return result;
        }

        public static int TryParseInteger(string value, out bool successOrFailure)
        {
            successOrFailure = int.TryParse(value, out var result);

            return result;
        }

        public static int ParseInteger(string value)
        {
            if (null == value) {
                return 0;
            }

            return int.Parse(value);
        }

        public static float TryParseFloat(string value, out bool successOrFailure)
        {
            successOrFailure = float.TryParse(value, out var result);

            return result;
        }

        public static float ParseFloat(string value)
        {
            if (null == value) {
                return 0;
            }

            return float.Parse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
        }

        public static SamplerType TryParseSamplerType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseSamplerType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return SamplerType.Unknown;
        }

        public static SamplerType ParseSamplerType(string value)
        {
            if (value == null) {
                return SamplerType.Default;
            }

            switch (value) {
                case "SAMPLERTYPE_Color":
                    return SamplerType.Color;
                case "SAMPLERTYPE_Grayscale":
                    return SamplerType.Grayscale;
                case "SAMPLERTYPE_Alpha":
                    return SamplerType.Alpha;
                case "SAMPLERTYPE_Normal":
                    return SamplerType.Normal;
                case "SAMPLERTYPE_Masks":
                    return SamplerType.Masks;
                case "SAMPLERTYPE_DistanceFieldFont":
                    return SamplerType.DistanceFieldFont;
                case "SAMPLERTYPE_LinearColor":
                    return SamplerType.LinearColor;
                case "SAMPLERTYPE_LinearGrayscale":
                    return SamplerType.LinearGrayscale;
                case "SAMPLERTYPE_Data":
                    return SamplerType.Data;
                case "SAMPLERTYPE_External":
                    return SamplerType.External;
                case "SAMPLERTYPE_VirtualColor":
                    return SamplerType.VirtualColor;
                case "SAMPLERTYPE_VirtualGrayscale":
                    return SamplerType.VirtualGrayscale;
                case "SAMPLERTYPE_VirtualAlpha":
                    return SamplerType.VirtualAlpha;
                case "SAMPLERTYPE_VirtualNormal":
                    return SamplerType.VirtualNormal;
                case "SAMPLERTYPE_VirtualMasks":
                    return SamplerType.VirtualMasks;
                case "SAMPLERTYPE_VirtualLinearColor":
                    return SamplerType.VirtualLinearColor;
                case "SAMPLERTYPE_VirtualLinearGrayscale":
                    return SamplerType.VirtualLinearGrayscale;
            }

            throw new ValueException("Unexpected sampler type: " + value);
        }

        public static MaterialDomain TryParseMaterialDomain(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseMaterialDomain(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return MaterialDomain.Unknown;
        }

        public static MaterialDomain ParseMaterialDomain(string value)
        {
            if (value == null) {
                return MaterialDomain.Surface;
            }

            switch (value) {
                case "MD_Surface":
                    return MaterialDomain.Surface;
                case "MD_DeferredDecal":
                    return MaterialDomain.DeferredDecal;
                case "MD_LightFunction":
                    return MaterialDomain.LightFunction;
                case "MD_Volume":
                    return MaterialDomain.Volume;
                case "MD_PostProcess":
                    return MaterialDomain.PostProcess;
                case "MD_UI":
                    return MaterialDomain.Ui;
                case "MD_RuntimeVirtualTexture":
                    return MaterialDomain.RuntimeVirtualTexture;
            }

            throw new ValueException("Unexpected material domain: " + value);
        }

        public static TranslucencyLightingMode TryParseTranslucencyLightingMode(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseTranslucencyLightingMode(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return TranslucencyLightingMode.Unknown;
        }

        public static TranslucencyLightingMode ParseTranslucencyLightingMode(string value)
        {
            if (value == null) {
                return TranslucencyLightingMode.VolumetricNonDirectional;
            }

            switch (value) {
                case "TLM_VolumetricNonDirectional":
                    return TranslucencyLightingMode.VolumetricNonDirectional;
                case "TLM_VolumetricDirectional":
                    return TranslucencyLightingMode.VolumetricDirectional;
                case "TLM_VolumetricPerVertexNonDirectional":
                    return TranslucencyLightingMode.VolumetricPerVertexNonDirectional;
                case "TLM_VolumetricPerVertexDirectional":
                    return TranslucencyLightingMode.VolumetricPerVertexDirectional;
                case "TLM_Surface":
                    return TranslucencyLightingMode.Surface;
                case "TLM_SurfacePerPixelLighting":
                    return TranslucencyLightingMode.SurfacePerPixelLighting;
            }

            throw new ValueException("Unexpected translucency lighting mode: " + value);
        }

        public static BlendMode TryParseBlendMode(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseBlendMode(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return BlendMode.Unknown;
        }

        public static BlendMode ParseBlendMode(string value)
        {
            if (value == null || value == "BLEND_Opaque") {
                return BlendMode.Opaque;
            } else if (value == "BLEND_Additive") {
                return BlendMode.Additive;
            } else if (value == "BLEND_Masked") {
                return BlendMode.Masked;
            } else if (value == "BLEND_Modulate") {
                return BlendMode.Modulate;
            } else if (value == "BLEND_Translucent") {
                return BlendMode.Translucent;
            }

            throw new ValueException("Unexpected blend mode: " + value);
        }

        public static ShadingModel TryParseShadingModel(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseShadingModel(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return ShadingModel.Unknown;
        }

        public static ShadingModel ParseShadingModel(string value)
        {
            if (value == null) {
                return ShadingModel.DefaultLit;
            } else if (value == "MSM_Unlit") {
                return ShadingModel.Unlit;
            }

            throw new ValueException("Unexpected shading model: " + value);
        }

        public static ExpressionReference TryParseExpressionReference(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseExpressionReference(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return null;
        }

        public static ExpressionReference ParseExpressionReference(string value)
        {
            if (value == null) {
                return null;
            }

            Match match = ExpressionReferenceRegex.Match(value);

            if (! match.Success) {
                throw new ValueException("Failed to parse ExpressionReference");
            }

            return new ExpressionReference(match.Groups["type"].Value, match.Groups["object"].Value);
        }

        public static ParsedPropertyBag ParseAttributeList(string value)
        {
            if (value == null) {
                return null;
            }

            if (! value.StartsWith("(") || ! value.EndsWith(")")) {
                throw new ValueException("Attribute list should begin and end with parenthesis");
            }

            var parser = new DocumentParser(value.Substring(1, value.Length - 2));

            return parser.ReadAttributeList();
        }

        public static ParsedPropertyBag TryParseAttributeList(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseAttributeList(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return null;
        }

        public static ResourceReference TryParseResourceReference(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseResourceReference(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return null;
        }

        public static ResourceReference ParseResourceReference(string value)
        {
            if (null == value) {
                return null;
            }

            if ("None" == value) {
                return new ResourceReference("None", "None");
            }

            Match match = ResourceReferenceRegex.Match(value);

            if (! match.Success) {
                throw new ValueException("Failed to parse resource reference");
            }

            return new ResourceReference(match.Groups["type"].Value, match.Groups["resource"].Value);
        }

        public static FunctionReference TryParseFunctionReference(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseFunctionReference(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return null;
        }

        public static FunctionReference ParseFunctionReference(string value)
        {
            Match match = ResourceReferenceRegex.Match(value);

            if (! match.Success) {
                throw new ValueException("Failed to parse FunctionReference");
            }

            return new FunctionReference(match.Groups[1].Value, match.Groups[2].Value);
        }

        public static Rotator TryParseRotator(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseRotator(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return default(Rotator);
        }

        public static Rotator ParseRotator(string value)
        {
            if (null == value) {
                return default(Rotator);
            }

            Match match = RotatorRegex.Match(value);

            if (! match.Success) {
                throw new ValueException("Failed to parse Rotator");
            }

            return new Rotator(ParseFloat(match.Groups[1].Value), ParseFloat(match.Groups[2].Value), ParseFloat(match.Groups[3].Value));
        }

        public static Mobility TryParseMobility(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseMobility(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return Mobility.Stationary;
        }

        public static Mobility ParseMobility(string value)
        {
            if (value == "Stationary") {
                return Mobility.Stationary;
            } else if (value == "Movable") {
                return Mobility.Movable;
            } else if (value == null || value == "Static") {
                return Mobility.Static;
            }

            throw new ValueException("Unexpected mobility mode: " + value);
        }

        public static ReflectionSourceType TryParseReflectionSourceType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseReflectionSourceType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return ReflectionSourceType.CapturedScene;
        }

        public static ReflectionSourceType ParseReflectionSourceType(string value)
        {
            if (value == null || value == "CapturedScene") {
                return ReflectionSourceType.CapturedScene;
            } else if (value == "SpecifiedCubemap") {
                return ReflectionSourceType.SpecifiedCubemap;
            }

            throw new ValueException("Unexpected reflection source type: " + value);
        }

        public static SpawnCollisionHandlingMethod TryParseSpawnCollisionHandlingMethod(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseSpawnCollisionHandlingMethod(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return SpawnCollisionHandlingMethod.AlwaysSpawn;
        }

        public static SpawnCollisionHandlingMethod ParseSpawnCollisionHandlingMethod(string value)
        {
            if (value == null || value == "AlwaysSpawn") {
                return SpawnCollisionHandlingMethod.AlwaysSpawn;
            } else if (value == "AdjustIfPossibleButAlwaysSpawn") {
                return SpawnCollisionHandlingMethod.AdjustIfPossibleButAlwaysSpawn;
            } else if (value == "AdjustIfPossibleButDontSpawnIfColliding") {
                return SpawnCollisionHandlingMethod.AdjustIfPossibleButDontSpawnIfColliding;
            } else if (value == "DontSpawnIfColliding") {
                return SpawnCollisionHandlingMethod.DontSpawnIfColliding;
            }

            throw new ValueException("Unexpected spawn collision handling method: " + value);
        }

        public static Vector3 TryParseVector3(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseVector3(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return default(Vector3);
        }

        public static Vector3 ParseVector3(string value)
        {
            if (null == value) {
                return default(Vector3);
            }

            Match match = Vector3Regex.Match(value);

            if (! match.Success) {
                throw new ValueException("Failed to parse Vector3");
            }

            return new Vector3(ParseFloat(match.Groups[1].Value), ParseFloat(match.Groups[2].Value), ParseFloat(match.Groups[3].Value));
        }

        public static Vector4 TryParseVector4(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseVector4(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return default(Vector4);
        }

        public static Vector4 ParseVector4(string value)
        {
            if (null == value) {
                return default(Vector4);
            }

            Match match = Vector4Regex.Match(value);

            if (! match.Success) {
                throw new ValueException("Failed to parse Vector4");
            }

            var v1 = match.Groups[1].Value;
            var v2 = match.Groups[2].Value;
            var v3 = match.Groups[3].Value;
            var v4 = match.Groups[4].Value;
            var v5 = match.Groups[5].Value;
            var v6 = match.Groups[6].Value;
            var v7 = match.Groups[7].Value;

            var r = 0.0f;
            var g = 0.0f;
            var b = 0.0f;
            var a = 0.0f;
            
            for (int i = 1; i < 8; i += 2) {
                var component = match.Groups[i].Value;

                switch (component) {
                    case "R":
                        r = ParseFloat(match.Groups[i + 1].Value);
                        break;
                    case "G":
                        g = ParseFloat(match.Groups[i + 1].Value);
                        break;
                    case "B":
                        b = ParseFloat(match.Groups[i + 1].Value);
                        break;
                    case "A":
                        a = ParseFloat(match.Groups[i + 1].Value);
                        break;
                }
            }

            return new Vector4(r, g, b, a);
        }

        public static ResourceReference[] ParseResourceReferenceArray(ParsedProperty[] elements)
        {
            if (null == elements) {
                return new ResourceReference[] {
                };
            }

            List<ResourceReference> list = new List<ResourceReference>();

            foreach (var parsedProperty in elements) {
                list.Add(ParseResourceReference(parsedProperty.Value));
            }

            return list.ToArray();
        }

        public static ExpressionReference[] ParseExpressionReferenceArray(ParsedProperty[] elements)
        {
            if (null == elements) {
                return new ExpressionReference[] {
                };
            }

            List<ExpressionReference> list = new List<ExpressionReference>();

            foreach (var parsedProperty in elements) {
                list.Add(ParseExpressionReference(parsedProperty.Value));
            }

            return list.ToArray();
        }

        public static ParsedPropertyBag[] ParseAttributeListArray(ParsedProperty[] elements)
        {
            if (null == elements) {
                return new ParsedPropertyBag[] {
                };
            }

            List<ParsedPropertyBag> list = new List<ParsedPropertyBag>();

            foreach (var parsedProperty in elements) {
                list.Add(ParseAttributeList(parsedProperty.Value));
            }

            return list.ToArray();
        }
    }
}
