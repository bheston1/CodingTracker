namespace CodingTracker
{
    internal class CodingSession
    {
        internal int Id { get; set; }
        internal DateTime Date { get; set; }
        internal DateTime Start { get; set; }
        internal DateTime End { get; set; }
        internal TimeSpan Duration { get; set; }
    }
}