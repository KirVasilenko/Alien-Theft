using UnityEngine;

public static class LevelCountManager
{
    private const string _completedLevelsKey = "CompletedLevels";

    public static int GetCompletedLevels()
    {
        return PlayerPrefs.GetInt(_completedLevelsKey, 0);
    }

    public static void CompleteLevel()
    {
        int completedLevels = GetCompletedLevels();
        completedLevels++;
        PlayerPrefs.SetInt(_completedLevelsKey, completedLevels);
    }
}
