using System;

public static class StringFormatter
{
    public static string FormatTimeFromSeconds(int timeInSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);
        return time.ToString(@"mm\:ss");
    }

    public static string FormatScore(int score)
    {
        return $"{(score / 1000).ToString("00")},{(score % 1000).ToString("000")}";
    }
}
