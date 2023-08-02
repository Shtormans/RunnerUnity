public static class StatsContainer
{
    private static int _score = 0;
    private static int _collectedBonuses = 0;
    private static int _timeInSeconds = 0;

    public static int Score => _score;
    public static int CollectedBonuses => _collectedBonuses;
    public static int TimeInSeconds => _timeInSeconds;

    public static void AddToScore(int amount)
    {
        _score += amount;
    }

    public static void IncrementBonusAmount()
    {
        _collectedBonuses += 1;
    }

    public static void AddSecondsToTimer(int seconds)
    {
        _timeInSeconds += seconds;
    }

    public static void ResetStats()
    {
        _score = 0;
        _collectedBonuses = 0;
        _timeInSeconds = 0;
    }
}
