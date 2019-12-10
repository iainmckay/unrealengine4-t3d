namespace JollySamurai.UnrealEngine4.T3D.Exception
{
    public class ParserException : T3DException
    {
        public int Line { get; }
        public int Column { get; }

        public ParserException(string message, int line, int column)
            : base(message)
        {
            Line = line;
            Column = column;
        }
    }
}
