﻿using System;
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
        public static readonly Regex Vector2Regex = new Regex(@"\(X=(\-{0,}[0-9]+\.[0-9]+),Y=(\-{0,}[0-9]+\.[0-9]+)\)", RegexOptions.Compiled);
        public static readonly Regex Vector3Regex = new Regex(@"\(X=(\-{0,}[0-9]+\.[0-9]+),Y=(\-{0,}[0-9]+\.[0-9]+),Z=(\-{0,}[0-9]+\.[0-9]+)\)", RegexOptions.Compiled);
        public static readonly Regex Vector4Regex = new Regex(@"\(([RGBAX])=([\-0-9\.]+),([RGBAY])=([\-0-9\.]+),([RGBAZ])=([\-0-9\.]+),([RGBAW])=([\-0-9\.]+)\)", RegexOptions.Compiled);
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
            if (value == null || value == "MSM_DefaultLit") {
                return ShadingModel.DefaultLit;
            } else if (value == "MSM_Unlit") {
                return ShadingModel.Unlit;
            } else if (value == "MSM_Subsurface") {
                return ShadingModel.Subsurface;
            } else if (value == "MSM_PreintegratedSkin") {
                return ShadingModel.PreIntegratedSkin;
            } else if (value == "MSM_ClearCoat") {
                return ShadingModel.ClearCoat;
            } else if (value == "MSM_SubsurfaceProfile") {
                return ShadingModel.SubsurfaceProfile;
            } else if (value == "MSM_TwoSidedFoliage") {
                return ShadingModel.TwoSidedFoliage;
            } else if (value == "MSM_Hair") {
                return ShadingModel.Hair;
            } else if (value == "MSM_Cloth") {
                return ShadingModel.Cloth;
            } else if (value == "MSM_Eye") {
                return ShadingModel.Eye;
            } else if (value == "MSM_SingleLayerWater") {
                return ShadingModel.SingleLayerWater;
            } else if (value == "MSM_ThinTranslucent") {
                return ShadingModel.ThinTranslucent;
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

        public static BloomMethod TryParseBloomMethod(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseBloomMethod(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return BloomMethod.SumOfGuassian;
        }

        public static BloomMethod ParseBloomMethod(string value)
        {
            if (value == null || value == "BM_SOG") {
                return BloomMethod.SumOfGuassian;
            } else if (value == "BM_FFT") {
                return BloomMethod.FastFourierTransform;
            }

            throw new ValueException("Unexpected bloom method: " + value);
        }

        public static AutoExposureMethod TryParseAutoExposureMethod(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseAutoExposureMethod(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return AutoExposureMethod.Histogram;
        }

        public static AutoExposureMethod ParseAutoExposureMethod(string value)
        {
            if (value == null || value == "AEM_Histogram") {
                return AutoExposureMethod.Histogram;
            } else if (value == "AEM_Basic") {
                return AutoExposureMethod.Basic;
            } else if (value == "AEM_Manual") {
                return AutoExposureMethod.Manual;
            }

            throw new ValueException("Unexpected auto exposure method: " + value);
        }

        public static TemperatureType TryParseTemperatureType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseTemperatureType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return TemperatureType.WhiteBalance;
        }

        public static TemperatureType ParseTemperatureType(string value)
        {
            if (value == null || value == "TEMP_WhiteBalance") {
                return TemperatureType.WhiteBalance;
            } else if (value == "TEMP_ColorTemperature") {
                return TemperatureType.ColorTemperature;
            }

            throw new ValueException("Unexpected temperature type: " + value);
        }

        public static RayTracingGlobalIlluminationType TryParseRayTracingGlobalIlluminationType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseRayTracingGlobalIlluminationType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return RayTracingGlobalIlluminationType.Disabled;
        }

        public static RayTracingGlobalIlluminationType ParseRayTracingGlobalIlluminationType(string value)
        {
            if (value == null || value == "Disabled") {
                return RayTracingGlobalIlluminationType.Disabled;
            } else if (value == "BruteForce") {
                return RayTracingGlobalIlluminationType.BruteForce;
            } else if (value == "FinalGather") {
                return RayTracingGlobalIlluminationType.FinalGather;
            }

            throw new ValueException("Unexpected ray tracing global illumination type: " + value);
        }

        public static ReflectionsType TryParseReflectionsType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseReflectionsType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return ReflectionsType.ScreenSpace;
        }

        public static ReflectionsType ParseReflectionsType(string value)
        {
            if (value == null || value == "ScreenSpace") {
                return ReflectionsType.ScreenSpace;
            } else if (value == "RayTracing") {
                return ReflectionsType.RayTracing;
            }

            throw new ValueException("Unexpected reflections type: " + value);
        }

        public static RayTracingShadows TryParseRayTracingShadows(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseRayTracingShadows(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return RayTracingShadows.HardShadows;
        }

        public static RayTracingShadows ParseRayTracingShadows(string value)
        {
            if (value == null || value == "Hard_shadows") {
                return RayTracingShadows.HardShadows;
            } else if (value == "Area_shadows") {
                return RayTracingShadows.AreaShadows;
            } else if (value == "Disabled") {
                return RayTracingShadows.Disabled;
            }

            throw new ValueException("Unexpected ray tracing shadows: " + value);
        }

        public static TranslucencyType TryParseTranslucencyType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseTranslucencyType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return TranslucencyType.Raster;
        }

        public static TranslucencyType ParseTranslucencyType(string value)
        {
            if (value == null || value == "Raster") {
                return TranslucencyType.RayTracing;
            } else if (value == "RayTracing") {
                return TranslucencyType.RayTracing;
            }

            throw new ValueException("Unexpected translucency type: " + value);
        }

        public static SkyLightSourceType TryParseSkyLightSourceType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseSkyLightSourceType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return SkyLightSourceType.CapturedScene;
        }

        public static SkyLightSourceType ParseSkyLightSourceType(string value)
        {
            if (value == null || value == "SLS_CapturedScene") {
                return SkyLightSourceType.CapturedScene;
            } else if (value == "SLS_SpecifiedCubemap") {
                return SkyLightSourceType.SpecifiedCubemap;
            }

            throw new ValueException("Unexpected sky light source type: " + value);
        }

        public static DecalBlendMode TryParseDecalBlendMode(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseDecalBlendMode(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return DecalBlendMode.Translucent;
        }

        public static DecalBlendMode ParseDecalBlendMode(string value)
        {
            if (value == null || value == "DBM_Translucent") {
                return DecalBlendMode.Translucent;
            } else if (value == "DBM_Stain") {
                return DecalBlendMode.Stain;
            } else if (value == "DBM_Normal") {
                return DecalBlendMode.Normal;
            } else if (value == "DBM_DBuffer_ColorNormalRoughness") {
                return DecalBlendMode.DBufferColorNormalRoughness;
            } else if (value == "DBM_DBuffer_Color") {
                return DecalBlendMode.DBufferColor;
            } else if (value == "DBM_DBuffer_ColorNormal") {
                return DecalBlendMode.DBufferColorNormal;
            } else if (value == "DBM_DBuffer_ColorRoughness") {
                return DecalBlendMode.DBufferColorRoughness;
            } else if (value == "DBM_DBuffer_Normal") {
                return DecalBlendMode.DBufferNormal;
            } else if (value == "DBM_DBuffer_NormalRoughness") {
                return DecalBlendMode.DBufferNormalRoughness;
            } else if (value == "DBM_DBuffer_Roughness") {
                return DecalBlendMode.DBufferRoughness;
            } else if (value == "DBM_DBuffer_Emissive") {
                return DecalBlendMode.DBufferEmissive;
            } else if (value == "DBM_DBuffer_AlphaComposite") {
                return DecalBlendMode.DBufferAlphaComposite;
            } else if (value == "DBM_DBuffer_EmissiveAlphaComposite") {
                return DecalBlendMode.DBufferEmissiveAlphaComposite;
            } else if (value == "DBM_Volumetric_DistanceFunction") {
                return DecalBlendMode.VolumetricDistanceFunction;
            } else if (value == "DBM_AlphaComposite") {
                return DecalBlendMode.AlphaComposite;
            } else if (value == "DBM_AmbientOcclusion") {
                return DecalBlendMode.AmbientOcclusion;
            }

            throw new ValueException("Unexpected decal blend mode: " + value);
        }

        public static WorldPositionIncludedOffsets TryParseWorldPositionIncludedOffsets(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseWorldPositionIncludedOffsets(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return WorldPositionIncludedOffsets.Default;
        }

        public static WorldPositionIncludedOffsets ParseWorldPositionIncludedOffsets(string value)
        {
            if (value == null || value == "WPT_Default") {
                return WorldPositionIncludedOffsets.Default;
            } else if (value == "WPT_ExcludeAllShaderOffsets") {
                return WorldPositionIncludedOffsets.ExcludeAllShaderOffsets;
            } else if (value == "WPT_CameraRelative") {
                return WorldPositionIncludedOffsets.CameraRelative;
            } else if (value == "WPT_CameraRelativeNoOffsets") {
                return WorldPositionIncludedOffsets.CameraRelativeNoOffsets;
            }

            throw new ValueException("Unexpected world position included offsets: " + value);
        }

        public static MaterialSceneAttributeInputMode TryParseMaterialSceneAttributeInputMode(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseMaterialSceneAttributeInputMode(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return MaterialSceneAttributeInputMode.Coordinates;
        }

        public static MaterialSceneAttributeInputMode ParseMaterialSceneAttributeInputMode(string value)
        {
            if (value == null || value == "Coordinates") {
                return MaterialSceneAttributeInputMode.Coordinates;
            } else if (value == "OffsetFraction") {
                return MaterialSceneAttributeInputMode.OffsetFraction;
            }

            throw new ValueException("Unexpected material scene attribute input mode: " + value);
        }

        public static LandscapeLayerBlendType TryParseLandscapeLayerBlendType(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseLandscapeLayerBlendType(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return LandscapeLayerBlendType.WeightBlend;
        }

        public static LandscapeLayerBlendType ParseLandscapeLayerBlendType(string value)
        {
            if (value == null || value == "LB_WeightBlend") {
                return LandscapeLayerBlendType.WeightBlend;
            } else if (value == "LB_AlphaBlend") {
                return LandscapeLayerBlendType.AlphaBlend;
            } else if (value == "LB_HeightBlend") {
                return LandscapeLayerBlendType.HeightBlend;
            }

            throw new ValueException("Unexpected material scene attribute input mode: " + value);
        }

        public static MaterialVectorCoordTransformSource TryParseMaterialVectorCoordTransformSource(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseMaterialVectorCoordTransformSource(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return MaterialVectorCoordTransformSource.Tangent;
        }

        public static MaterialVectorCoordTransformSource ParseMaterialVectorCoordTransformSource(string value)
        {
            if (value == null || value == "TRANSFORMSOURCE_Tangent") {
                return MaterialVectorCoordTransformSource.Tangent;
            } else if (value == "TRANSFORMSOURCE_Local") {
                return MaterialVectorCoordTransformSource.Local;
            } else if (value == "TRANSFORMSOURCE_World") {
                return MaterialVectorCoordTransformSource.World;
            } else if (value == "TRANSFORMSOURCE_View") {
                return MaterialVectorCoordTransformSource.View;
            } else if (value == "TRANSFORMSOURCE_Camera") {
                return MaterialVectorCoordTransformSource.Camera;
            } else if (value == "TRANSFORMSOURCE_ParticleWorld") {
                return MaterialVectorCoordTransformSource.ParticleWorld;
            }

            throw new ValueException("Unexpected material vector coord transform source: " + value);
        }

        public static MaterialVectorCoordTransform TryParseMaterialVectorCoordTransform(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseMaterialVectorCoordTransform(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return MaterialVectorCoordTransform.Tangent;
        }

        public static MaterialVectorCoordTransform ParseMaterialVectorCoordTransform(string value)
        {
            if (value == null || value == "TRANSFORM_World") {
                return MaterialVectorCoordTransform.World;
            } else if (value == "TRANSFORM_Tangent") {
                return MaterialVectorCoordTransform.Tangent;
            } else if (value == "TRANSFORM_Local") {
                return MaterialVectorCoordTransform.Local;
            } else if (value == "TRANSFORM_View") {
                return MaterialVectorCoordTransform.View;
            } else if (value == "TRANSFORM_Camera") {
                return MaterialVectorCoordTransform.Camera;
            } else if (value == "TRANSFORM_ParticleWorld") {
                return MaterialVectorCoordTransform.ParticleWorld;
            }

            throw new ValueException("Unexpected material vector coord transform: " + value);
        }

        public static SceneTextureId TryParseSceneTextureId(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseSceneTextureId(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return SceneTextureId.SceneColor;
        }

        public static SceneTextureId ParseSceneTextureId(string value)
        {
            if (value == null || value == "PPI_SceneColor") {
                return SceneTextureId.SceneColor;
            } else if (value == "PPI_SceneDepth") {
                return SceneTextureId.SceneDepth;
            } else if (value == "PPI_DiffuseColor") {
                return SceneTextureId.DiffuseColor;
            } else if (value == "PPI_SpecularColor") {
                return SceneTextureId.SpecularColor;
            } else if (value == "PPI_SubsurfaceColor") {
                return SceneTextureId.SubsurfaceColor;
            } else if (value == "PPI_BaseColor") {
                return SceneTextureId.BaseColor;
            } else if (value == "PPI_Specular") {
                return SceneTextureId.Specular;
            } else if (value == "PPI_Metallic") {
                return SceneTextureId.Metallic;
            } else if (value == "PPI_WorldNormal") {
                return SceneTextureId.WorldNormal;
            } else if (value == "PPI_SeparateTranslucency") {
                return SceneTextureId.SeparateTranslucency;
            } else if (value == "PPI_Opacity") {
                return SceneTextureId.Opacity;
            } else if (value == "PPI_Roughness") {
                return SceneTextureId.Roughness;
            } else if (value == "PPI_MaterialAO") {
                return SceneTextureId.MaterialAo;
            } else if (value == "PPI_CustomDepth") {
                return SceneTextureId.CustomDepth;
            } else if (value == "PPI_PostProcessInput0") {
                return SceneTextureId.PostProcessInput0;
            } else if (value == "PPI_PostProcessInput1") {
                return SceneTextureId.PostProcessInput1;
            } else if (value == "PPI_PostProcessInput2") {
                return SceneTextureId.PostProcessInput2;
            } else if (value == "PPI_PostProcessInput3") {
                return SceneTextureId.PostProcessInput3;
            } else if (value == "PPI_PostProcessInput4") {
                return SceneTextureId.PostProcessInput4;
            } else if (value == "PPI_PostProcessInput5") {
                return SceneTextureId.PostProcessInput5;
            } else if (value == "PPI_PostProcessInput6") {
                return SceneTextureId.PostProcessInput6;
            } else if (value == "PPI_DecalMask") {
                return SceneTextureId.DecalMask;
            } else if (value == "PPI_ShadingModelColor") {
                return SceneTextureId.ShadingModelColor;
            } else if (value == "PPI_ShadingModelID") {
                return SceneTextureId.ShadingModelId;
            } else if (value == "PPI_AmbientOcclusion") {
                return SceneTextureId.AmbientOcclusion;
            } else if (value == "PPI_CustomStencil") {
                return SceneTextureId.CustomStencil;
            } else if (value == "PPI_StoredBaseColor") {
                return SceneTextureId.StoredBaseColor;
            } else if (value == "PPI_StoredSpecular") {
                return SceneTextureId.StoredSpecular;
            } else if (value == "PPI_Velocity") {
                return SceneTextureId.Velocity;
            } else if (value == "PPI_WorldTangent") {
                return SceneTextureId.WorldTangent;
            } else if (value == "PPI_Anisotropy") {
                return SceneTextureId.Anisotropy;
            }

            throw new ValueException("Unexpected scene texture ID: " + value);
        }

        public static Vector2 TryParseVector2(string value, out bool successOrFailure)
        {
            try {
                successOrFailure = true;

                return ParseVector2(value);
            } catch (ValueException) {
                successOrFailure = false;
            }

            return default(Vector2);
        }

        public static Vector2 ParseVector2(string value)
        {
            if (null == value) {
                return default(Vector2);
            }

            Match match = Vector2Regex.Match(value);

            if (! match.Success) {
                throw new ValueException("Failed to parse Vector2");
            }

            return new Vector2(ParseFloat(match.Groups[1].Value), ParseFloat(match.Groups[2].Value));
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

            var r = 0.0f;
            var g = 0.0f;
            var b = 0.0f;
            var a = 0.0f;
            
            for (int i = 1; i < 8; i += 2) {
                var component = match.Groups[i].Value;

                switch (component) {
                    case "R":
                    case "X":
                        r = ParseFloat(match.Groups[i + 1].Value);
                        break;
                    case "G":
                    case "Y":
                        g = ParseFloat(match.Groups[i + 1].Value);
                        break;
                    case "B":
                    case "Z":
                        b = ParseFloat(match.Groups[i + 1].Value);
                        break;
                    case "A":
                    case "W":
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
