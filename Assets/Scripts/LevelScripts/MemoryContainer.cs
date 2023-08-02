using UnityEngine;

public static class MemoryContainer
{
    public static void SetLastScore(int score)
    {
        PlayerPrefs.SetInt(ConstStrings.PlayerPrefsKeys.LastScore, score);
    }

    public static int GetLastScore()
    {
        if (PlayerPrefs.HasKey(ConstStrings.PlayerPrefsKeys.LastScore))
        {
            return PlayerPrefs.GetInt(ConstStrings.PlayerPrefsKeys.LastScore);
        }

        SetLastScore(0);
        return 0;
    }

    public static void SetBestScore(int score)
    {
        PlayerPrefs.SetInt(ConstStrings.PlayerPrefsKeys.BestScore, score);
    }

    public static int GetBestScore()
    {
        if (PlayerPrefs.HasKey(ConstStrings.PlayerPrefsKeys.BestScore))
        {
            return PlayerPrefs.GetInt(ConstStrings.PlayerPrefsKeys.BestScore);
        }

        SetBestScore(0);
        return 0;
    }
}
