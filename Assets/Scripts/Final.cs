using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
	private bool isFinalPart = false;
	[SerializeField] private Image barImage;
	[SerializeField] private GameObject finalPart;



	private GameEvents gameEvents;
	[SerializeField] private float powerBar;
	private WaitForSeconds wait;
	[SerializeField] private float decreaseTime;
	[SerializeField] private float maxPowerBar;

	void Start()
	{
		wait = new WaitForSeconds(decreaseTime);
		Mathf.Clamp(powerBar, 0, maxPowerBar);
		finalPart.SetActive(false);
		gameEvents = GameEvents.instance;
		gameEvents.FinalPartSet += OnFinalPart;

	}
	private void Update()
	{
		if (isFinalPart == true)
		{
			MouseClick();
		}
	}
	private IEnumerator FinalPartCountdown()
	{
		while (isFinalPart == true)
		{

			yield return wait;
			powerBar--;
			barImage.fillAmount = powerBar / maxPowerBar;
		}

	}


	private void OnFinalPart(FinalPartStatus finalPartStatus)
	{
		if (finalPartStatus == FinalPartStatus.Active)
		{
			isFinalPart = true;
			finalPart.SetActive(true);
			StartCoroutine(FinalPartCountdown());
		}
		if (finalPartStatus == FinalPartStatus.Disable)
		{
			isFinalPart = false;
			finalPart.SetActive(false);
		}

	}

	private void MouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			powerBar += 3;

		}
	}
}
