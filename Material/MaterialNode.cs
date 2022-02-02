using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public abstract class MaterialNode : Node, IEditorPositionable
    {
        public int EditorX { get; }
        public int EditorY { get; }

        public MaterialNode(string name, int editorX, int editorY, Node[] children = null)
            : base(name, children)
        {
            EditorX = editorX;
            EditorY = editorY;
        }
    }
    
    public abstract class MaterialNodeProcessor : ObjectNodeProcessor
    {
        public MaterialNodeProcessor()
        {
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("Desc");
            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Material");
        }
    }
}
