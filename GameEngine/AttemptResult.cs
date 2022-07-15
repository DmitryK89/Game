namespace Game
{
    public sealed class AttemptResult
    {
        public bool IsWin { get; set; }
        public bool IsRetryCounterFull { get; set; }
        public int AttemptsLeft { get; set; }
        public string MatchNumbers { get; set; }
        public bool InputValid { get; set; }
    }
}