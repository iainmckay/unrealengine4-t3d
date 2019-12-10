namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    public class Problem
    {
        public ProblemSeverity Severity { get; }
        public string Message { get; }

        public Problem(ProblemSeverity severity, string message)
        {
            Severity = severity;
            Message = message;
        }
    }

    public enum ProblemSeverity
    {
        Error,
        Warning
    }
}
