﻿using System.Linq;
 using JollySamurai.UnrealEngine4.T3D.Common;
 using JollySamurai.UnrealEngine4.T3D.Material;
 using JollySamurai.UnrealEngine4.T3D.Parser;
 using JollySamurai.UnrealEngine4.T3D.Processor;

 namespace JollySamurai.UnrealEngine4.T3D.MaterialInstance
{
    public class MaterialInstance : Node
    {
        public ParsedPropertyBag[] ScalarParameters { get; }
        public ParsedPropertyBag[] TextureParameters { get; }
        public ParsedPropertyBag[] VectorParameters { get; }

        public MaterialInstance(string name, Node[] children, ParsedPropertyBag[] scalarParameters, ParsedPropertyBag[] textureParameters, ParsedPropertyBag[] vectorParameters)
            : base(name, children)
        {
            ScalarParameters = scalarParameters;
            TextureParameters = textureParameters;
            VectorParameters = vectorParameters;
        }
    }

    public class MaterialInstanceProcessor : ObjectNodeProcessor
    {
        public override string Class {
            get { return "/Script/Engine.MaterialInstanceConstant"; }
        }

        public MaterialInstanceProcessor()
        {
            AddRequiredProperty("Parent", PropertyDataType.String);

            AddOptionalProperty("ScalarParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);
            AddOptionalProperty("TextureParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);
            AddOptionalProperty("VectorParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);

            AddIgnoredProperty("bHasStaticPermutationResource");
            AddIgnoredProperty("CachedReferencedTextures");
            AddIgnoredProperty("LightingGuid");
            AddIgnoredProperty("ParameterStateId");
            AddIgnoredProperty("ReferencedTextureGuids");
            AddIgnoredProperty("TextureStreamingData");
            AddIgnoredProperty("TextureStreamingDataVersion");
            AddIgnoredProperty("ThumbnailInfo");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            ParsedProperty expressionList = node.FindProperty("Expressions");

            return new MaterialInstance(
                node.FindAttributeValue("Name"),
                children,
                ValueUtil.ParseAttributeListArray(node.FindProperty("ScalarParameterValues")?.Elements),
                ValueUtil.ParseAttributeListArray(node.FindProperty("TextureParameterValues")?.Elements),
                ValueUtil.ParseAttributeListArray(node.FindProperty("VectorParameterValues")?.Elements)
            );
        }
    }
}
