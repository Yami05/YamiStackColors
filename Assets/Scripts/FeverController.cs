using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FeverController : MonoBehaviour
{
	private GameEvents gameEvents;
	private bool isFeverActivated;
	private WaitForSeconds wait;
	[SerializeField] private float feverTime;
	[SerializeField] private Image fillImage;

	[SerializeField] private float count;
	[SerializeField] private float maxCount;

	[SerializeField] private float countDownOfFever;
	public GameObject player;
	void Start()
	{

		countDownOfFever = feverTime;
		wait = new WaitForSeconds(feverTime);
		gameEvents = GameEvents.instance;
		gameEvents.FeverModeStatusDecider += OnFeverStatus;
		gameEvents.FeverModeCounter += ForIncreasing;
	}

	private IEnumerator WaitTime()
	{

		yield return wait;
		gameEvents.FeverModeStatusDecider?.Invoke(FeverModeStatus.Disabled, player);
	}

	IEnumerator CountDown()
	{
		while (isFeverActivated == true)
		{
			//Debug.Log(countDownOfFever);
			yield return new WaitForSeconds(1);
			countDownOfFever--;
			fillImage.fillAmount = countDownOfFever / feverTime;


		}

	}

	private void OnFeverStatus(FeverModeStatus feverModeStatus, GameObject go)
	{
		if (feverModeStatus == FeverModeStatus.Activated)
		{
			countDownOfFever = feverTime;
			isFeverActivated = true;
			StartCoroutine(WaitTime());
			StartCoroutine(CountDown());
		}
		else
		{
			count = 0;
			//Debug.Log("AHMET");
			isFeverActivated = false;

		}
	}

	private void ForIncreasing(FeverModeCount feverModeCount)
	{
		if (isFeverActivated == false)
		{
			fillImage.fillAmount = count / maxCount;

		}
		if (isFeverActivated == true)
			return;

		if (count >= maxCount)

		{
			gameEvents.FeverModeStatusDecider?.Invoke(FeverModeStatus.Activated, player);

		}


		if (feverModeCount == FeverModeCount.Increase)
			count++;
		else
		{
			if (count > 0)
			{

				count--;
			}

		}



	}



}
