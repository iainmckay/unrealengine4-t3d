using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionRotateAboutAxis : MaterialNode
    {
        public ParsedPropertyBag NormalizedRotationAxis { get; }
        public ParsedPropertyBag RotationAngle { get; }
        public ParsedPropertyBag PivotPoint { get; }
        public ParsedPropertyBag Position { get; }
        public float Period { get; }

        public MaterialExpressionRotateAboutAxis(string name, int editorX, int editorY, ParsedPropertyBag normalizedRotationAxis, ParsedPropertyBag rotationAngle, ParsedPropertyBag pivotPoint, ParsedPropertyBag position, float period)
            : base(name, editorX, editorY)
        {
            NormalizedRotationAxis = normalizedRotationAxis;
            RotationAngle = rotationAngle;
            PivotPoint = pivotPoint;
            Position = position;
            Period = period;
        }
    }

    public class MaterialExpressionRotateAboutAxisProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionRotateAboutAxis";

        public MaterialExpressionRotateAboutAxisProcessor()
        {
            AddOptionalProperty("NormalizedRotationAxis", PropertyDataType.AttributeList);
            AddOptionalProperty("Period", PropertyDataType.Float);
            AddOptionalProperty("PivotPoint", PropertyDataType.AttributeList);
            AddOptionalProperty("Position", PropertyDataType.AttributeList);
            AddOptionalProperty("RotationAngle", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionRotateAboutAxis(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("NormalizedRotationAxis")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("RotationAngle")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("PivotPoint")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Position")),
                ValueUtil.ParseFloat(node.FindPropertyValue("Period") ?? "1.0")
            );
        }
    }
}
