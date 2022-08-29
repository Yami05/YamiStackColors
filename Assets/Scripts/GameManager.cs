using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			NextLevel();
		}
	}
	public void Retry()
	{
		SceneManager.LoadScene("SampleScene");
	}
	public void StartTheGame()
	{
		GameEvents.instance.StartTheGame?.Invoke();
	}
	public void NextLevel()
	{
		Utilities.SetLevelPref();
		SceneManager.LoadScene("SampleScene");
	}
}
