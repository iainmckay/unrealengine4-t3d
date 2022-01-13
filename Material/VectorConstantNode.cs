using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public abstract class VectorConstantNode : MaterialNode
    {
        public Vector4 Constant { get; }

        public VectorConstantNode(string name, int editorX, int editorY, Vector4 constant)
            : base(name, editorX, editorY)
        {
            Constant = constant;
        }
    }

    public abstract class VectorConstantProcessor : MaterialNodeProcessor
    {
        public VectorConstantProcessor()
        {
            AddOptionalProperty("Constant", PropertyDataType.Vector4);
        }
    }
}
