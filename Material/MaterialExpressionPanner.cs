using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionPanner : Node
    {
        public ParsedPropertyBag Coordinate { get; }
        public ParsedPropertyBag Speed { get; }
        public ParsedPropertyBag Time { get; }

        public float SpeedX { get; }
        public float SpeedY { get; }
        public int ConstCoordinate { get; }
        public bool FractionalPart { get; }

        public MaterialExpressionPanner(string name, ParsedPropertyBag speed, ParsedPropertyBag coordinate, ParsedPropertyBag time, float speedX, float speedY, int constCoordinate, bool fractionalPart, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            Speed = speed;
            Coordinate = coordinate;
            Time = time;
            SpeedX = speedX;
            SpeedY = speedY;
            ConstCoordinate = constCoordinate;
            FractionalPart = fractionalPart;
        }
    }

    public class MaterialExpressionPannerProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionPanner";

        public MaterialExpressionPannerProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("bFractionalPart", PropertyDataType.Boolean);
            AddOptionalProperty("ConstCoordinate", PropertyDataType.Integer);
            AddOptionalProperty("Coordinate", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("Speed", PropertyDataType.AttributeList);
            AddOptionalProperty("SpeedX", PropertyDataType.Float);
            AddOptionalProperty("SpeedY", PropertyDataType.Float);
            AddOptionalProperty("Time", PropertyDataType.AttributeList);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionPanner(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Speed")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinate")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Time")),
                ValueUtil.ParseFloat(node.FindPropertyValue("SpeedX") ?? "0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("SpeedY") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("ConstCoordinate") ?? "0"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bFractionalPart") ?? "false"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
