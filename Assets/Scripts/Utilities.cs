using UnityEngine;

public static class Utilities
{

	public const string LevelIndex = "LevelIndex";
	public static void SetLevelPref(int levelCount = 1)
	{
		PlayerPrefs.SetInt(LevelIndex, PlayerPrefs.GetInt(LevelIndex, 1) + levelCount);

	}

}
