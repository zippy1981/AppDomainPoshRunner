namespace JustAProgrammer.ADPR
{
    /// <summary>State information for <seealso cref="ADPRHost"/>.</summary>
    public class ADPRState
    {
        /// <summary>If <seealso cref="ShouldExit"/> is true then this is the exit code the process to exit with it..</summary>
        public int ExitCode { get; set; }
        /// <summary>If the process hosting the RunSpace should exit.</summary>
        public bool ShouldExit { get; set; }
    }
}