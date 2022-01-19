using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    public class GenericDocumentProcessor<T> : DocumentProcessor<T>
        where T : Node
    {
        protected override bool IsIgnoredNode(ParsedNode parsedNode)
        {
            return false;
        }
    }
}
