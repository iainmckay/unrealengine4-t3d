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

        public MaterialInstance(Node[] children, string name, ParsedPropertyBag[] scalarParameters, ParsedPropertyBag[] textureParameters, ParsedPropertyBag[] vectorParameters)
            : base(name, -1, -1, children)
        {
            ScalarParameters = scalarParameters;
            TextureParameters = textureParameters;
            VectorParameters = vectorParameters;
        }
    }

    public class ObjectInstanceProcessor : ObjectNodeProcessor
    {
        public override string Class {
            get { return "/Script/Engine.MaterialInstanceConstant"; }
        }

        public ObjectInstanceProcessor() : base()
        {
            AddRequiredProperty("Parent", PropertyDataType.String);

            AddOptionalProperty("ScalarParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);
            AddOptionalProperty("TextureParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);
            AddOptionalProperty("VectorParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);

            AddIgnoredProperty("LightingGuid");
            AddIgnoredProperty("ParameterStateId");
            AddIgnoredProperty("ReferencedTextureGuids");
            AddIgnoredProperty("ThumbnailInfo");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            ParsedProperty expressionList = node.FindProperty("Expressions");

            return new MaterialInstance(
                children,
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeListArray(node.FindProperty("ScalarParameterValues")?.Elements),
                ValueUtil.ParseAttributeListArray(node.FindProperty("TextureParameterValues")?.Elements),
                ValueUtil.ParseAttributeListArray(node.FindProperty("VectorParameterValues")?.Elements)
            );
        }
    }
}
