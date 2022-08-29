using UnityEngine;

public class Multiplier : MonoBehaviour, IInteractForColor
{

	[SerializeField] private GameObject finishLine;

	public int b;
	private ScoreController scoreController;
	private GameEvents gameEvents;
	private void Start()
	{
		gameEvents = GameEvents.instance;
		scoreController = ScoreController.instance;

	}





	public void InteractForColor(Transform tr)
	{
		if (b > scoreController.score)
		{
			Debug.Log(b);
			scoreController.score = b;
		}
		gameEvents.Score?.Invoke();

	}
}
