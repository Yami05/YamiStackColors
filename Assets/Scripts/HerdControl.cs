using UnityEngine;

public class HerdControl : MonoBehaviour
{
	public ColorDecider currentColorOfHerd;
	public ColorDecider StandartColorOfHerd;
	private GameEvents gameEvents;

	private void Awake()
	{
		currentColorOfHerd = StandartColorOfHerd;
	}
	private void Start()
	{


		gameEvents = GameEvents.instance;

		gameEvents.FeverModeStatusDecider += FeverModeActiveWhatAmIGonnaDo;


	}

	private void FeverModeActiveWhatAmIGonnaDo(FeverModeStatus feverModeStatus, GameObject go)
	{

		if (feverModeStatus == FeverModeStatus.Activated)
		{
			currentColorOfHerd = go.GetComponent<CollectorController>().colorDeciderOfPlayer;
		}
		else
		{
			currentColorOfHerd = StandartColorOfHerd;
		}
	}


}
