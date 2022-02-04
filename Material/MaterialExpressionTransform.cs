using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTransform : MaterialNode
    {
        public ParsedPropertyBag Input { get; }
        public MaterialVectorCoordTransformSource TransformSourceType { get; }
        public MaterialVectorCoordTransform TransformType { get; }

        public MaterialExpressionTransform(string name, int editorX, int editorY, ParsedPropertyBag input, MaterialVectorCoordTransformSource transformSourceType, MaterialVectorCoordTransform transformType)
            : base(name, editorX, editorY)
        {
            Input = input;
            TransformSourceType = transformSourceType;
            TransformType = transformType;
        }
    }

    public class MaterialExpressionTransformProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTransform";

        public MaterialExpressionTransformProcessor()
        {
            AddOptionalProperty("Input", PropertyDataType.AttributeList);
            AddOptionalProperty("TransformSourceType", PropertyDataType.MaterialVectorCoordTransformSource);
            AddOptionalProperty("TransformType", PropertyDataType.MaterialVectorCoordTransform);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTransform(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseMaterialVectorCoordTransformSource(node.FindPropertyValue("TransformSourceType")),
                ValueUtil.ParseMaterialVectorCoordTransform(node.FindPropertyValue("TransformType"))
            );
        }
    }
}
