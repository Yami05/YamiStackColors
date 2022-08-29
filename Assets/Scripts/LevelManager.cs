using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private Levels[] level;

	private void Awake()
	{


		level = Resources.LoadAll<Levels>("Levelss");

		Instantiate(level[PlayerPrefs.GetInt(Utilities.LevelIndex) % level.Length].LevelPrefab);
	}
}
//void Awake()
//{
//	Instantiate(Levels[PlayerPrefs.GetInt(Utilities.LevelIndex) % Levels.Count]);
//}
