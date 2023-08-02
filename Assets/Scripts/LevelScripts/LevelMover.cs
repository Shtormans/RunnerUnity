using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelMover
{
    private static string[] _levels = { "MirrorLevel", "GravityLevel" };

    public static void MoveToNewLevel(string currentLevelName)
    {
        var levelNames = GetLevels();

        int index = new System.Random().Next(levelNames.Length);
        if (levelNames[index] == currentLevelName)
        {
            index = (index + 1) % levelNames.Length;
        }

        SceneManager.LoadScene(levelNames[index]);
    }

    public static void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void StartNewGame()
    {
        StatsContainer.ResetStats();

        var levelNames = GetLevels();
        int index = new System.Random().Next(levelNames.Length);
        SceneManager.LoadScene(levelNames[index]);
    }

    private static string[] GetLevels()
    {
        return _levels;
        return Resources.LoadAll("Levels").Select(x => x.name).ToArray();
    }
}
