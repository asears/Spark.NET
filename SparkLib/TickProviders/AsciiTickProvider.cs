namespace SparkNet.TickProviders
{
    /// <summary>
    /// Provides ascii characters for chart, shell-friendly.
    /// </summary>
    public class AsciiTickProvider : ITickProvider
    {
        public char[] Ticks { get; } = new char[] { '_', '.', 'ı', 'l', '|' };
    }
}