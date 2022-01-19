namespace JollySamurai.UnrealEngine4.T3D.Parser
{
    public class ParsedNodeBag
    {
        public ParsedNode[] Nodes { get; }

        public ParsedNodeBag(ParsedNode[] nodes)
        {
            Nodes = nodes;
        }

        public static ParsedNodeBag Empty =>
            new ParsedNodeBag(new ParsedNode[] {
            });
    }
}
