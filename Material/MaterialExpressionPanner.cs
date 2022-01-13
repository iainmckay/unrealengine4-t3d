using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionPanner : MaterialNode
    {
        public ParsedPropertyBag Coordinate { get; }
        public ParsedPropertyBag Speed { get; }
        public ParsedPropertyBag Time { get; }

        public float SpeedX { get; }
        public float SpeedY { get; }
        public int ConstCoordinate { get; }
        public bool FractionalPart { get; }

        public MaterialExpressionPanner(string name, int editorX, int editorY, ParsedPropertyBag speed, ParsedPropertyBag coordinate, ParsedPropertyBag time, float speedX, float speedY, int constCoordinate, bool fractionalPart)
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

    public class MaterialExpressionPannerProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionPanner";

        public MaterialExpressionPannerProcessor()
        {
            AddOptionalProperty("bFractionalPart", PropertyDataType.Boolean);
            AddOptionalProperty("ConstCoordinate", PropertyDataType.Integer);
            AddOptionalProperty("Coordinate", PropertyDataType.AttributeList);
            AddOptionalProperty("Speed", PropertyDataType.AttributeList);
            AddOptionalProperty("SpeedX", PropertyDataType.Float);
            AddOptionalProperty("SpeedY", PropertyDataType.Float);
            AddOptionalProperty("Time", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionPanner(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Speed")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinate")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Time")),
                ValueUtil.ParseFloat(node.FindPropertyValue("SpeedX")),
                ValueUtil.ParseFloat(node.FindPropertyValue("SpeedY")),
                ValueUtil.ParseInteger(node.FindPropertyValue("ConstCoordinate")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bFractionalPart"))
            );
        }
    }
}
