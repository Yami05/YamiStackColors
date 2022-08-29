using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
	[SerializeField] private GameObject startPanel;
	[SerializeField] TextMeshProUGUI collectableText;
	[SerializeField] private GameObject finalPartImage;
	private GameEvents gameEvents;
	private StackManager stackManager;
	private void Start()
	{
		stackManager = StackManager.instance;
		gameEvents = GameEvents.instance;
		gameEvents.CountOfCollectable += Counter;
		gameEvents.StartTheGame += DeleteButton;
	}

	private void DeleteButton()
	{
		startPanel.SetActive(false);
	}

	private void Counter()
	{
		collectableText.text = stackManager.collectableColorsList.Count.ToString();
	}

}
