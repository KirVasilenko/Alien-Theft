using UnityEngine;

public static class GemsCountManager
{
    private const string _totalGemsKey = "";

    public static int GetTotalGems()
    {
        return PlayerPrefs.GetInt(_totalGemsKey, 0);
    }

    public static void AddGems(int amount)
    {
        int totalGems = GetTotalGems();
        totalGems += amount;
        PlayerPrefs.SetInt(_totalGemsKey, totalGems);
    }
}
