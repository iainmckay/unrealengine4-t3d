using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionComponentMask : MaterialNode
    {
        public ExpressionReference Input { get; }
        public bool R { get; }
        public bool G { get; }
        public bool B { get; }
        public bool A { get; }

        public MaterialExpressionComponentMask(string name,int editorX, int editorY, ExpressionReference input, bool r, bool g, bool b, bool a)
            : base(name, editorX, editorY)
        {
            Input = input;
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    public class MaterialExpressionComponentMaskProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionComponentMask";

        public MaterialExpressionComponentMaskProcessor()
        {
            AddOptionalProperty("Input", PropertyDataType.ExpressionReference);
            AddOptionalProperty("R", PropertyDataType.Boolean);
            AddOptionalProperty("G", PropertyDataType.Boolean);
            AddOptionalProperty("B", PropertyDataType.Boolean);
            AddOptionalProperty("A", PropertyDataType.Boolean);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionComponentMask(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseExpressionReference(node.FindPropertyValue("Input")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("R")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("G")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("B")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("A"))
            );
        }
    }
}
