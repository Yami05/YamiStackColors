using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
	private GameEvents gameEvents;
	[SerializeField] private GameObject gameOverPanelUI;
	[SerializeField] private GameObject NextLevelPanelUI;
	[SerializeField] private TextMeshProUGUI textOfScore;
	private ScoreController scoreController;
	[SerializeField] private Button nextLevelButton;
	[SerializeField] private TextMeshProUGUI textOfTotalScore;
	void Start()
	{


		scoreController = ScoreController.instance;
		gameEvents = GameEvents.instance;
		gameEvents.GameOver = () => gameOverPanelUI.SetActive(true);
		gameEvents.NextLevel += () => textOfTotalScore.text = scoreController.totalScore.ToString();
		gameEvents.Score += () => textOfScore.text = scoreController.score.ToString();
		gameEvents.NextLevel += () => NextLevelPanelUI.SetActive(true);
	}





}
