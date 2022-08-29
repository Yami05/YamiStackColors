using UnityEngine;

public class ScoreController : MonoBehaviour
{
	public int score;
	public int totalScore;
	public static ScoreController instance;

	private GameEvents gameEvents;
	private StackManager stackManager;
	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		stackManager = StackManager.instance;
		gameEvents = GameEvents.instance;
		gameEvents.NextLevel += TotalScoreCalculator;
	}

	private void TotalScoreCalculator()
	{
		totalScore = stackManager.collectableColorsList.Count * score;
	}


}
