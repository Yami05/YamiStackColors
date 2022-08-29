using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class FinalPart : MonoBehaviour, IInteract, IInteractExit
{

	public List<Multiplier> multipliers = new List<Multiplier>();

	private CollectableColor[] collectableColors;
	private Rigidbody rb;
	private int countOfColors;
	private StackManager stackManager;
	private Multiplier[] multi;

	private bool isFinalPartStart;

	private GameEvents gameEvents;
	[SerializeField] private float powerBar;



	void Start()
	{
		isFinalPartStart = false;
		multi = transform.GetComponentsInChildren<Multiplier>();
		stackManager = StackManager.instance;
		gameEvents = GameEvents.instance;
		gameEvents.Kick += KickIt;
		SortList();
	}

	private void Update()
	{
		SpeedCal();
	}

	public void Interact(Transform tr, ColorDecider colorDecider)
	{

		gameEvents.FinalPartSet?.Invoke(FinalPartStatus.Active);
	}


	private void SortList()
	{
		multipliers.AddRange(multi);
		multipliers = multipliers.OrderBy((x => x.transform.position.z)).ToList();
		for (int i = 0; i < multipliers.Count; i++)
		{
			multipliers[i].GetComponent<Multiplier>().b = i + 1;
			multipliers[i].transform.GetComponentInChildren<TextMesh>().text = ("X") + (multipliers[i].GetComponent<Multiplier>().b).ToString();
		}
	}

	public void Exit(Transform tr)
	{

		gameEvents.FinalPartSet?.Invoke(FinalPartStatus.Disable);

	}




	private void KickIt(Transform tr)
	{
		countOfColors = stackManager.collectableColorsList.Count;

		collectableColors = tr.GetComponentsInChildren<CollectableColor>();
		//Debug.Log(countOfColors);
		for (int i = 0; i < collectableColors.Length; i++)
		{
			rb = collectableColors[i].gameObject.AddComponent<Rigidbody>();
			rb.transform.SetParent(null);
			rb.gameObject.GetComponent<BoxCollider>().isTrigger = false;
			rb.AddForce(Vector3.forward * (countOfColors * 0.001f) * (powerBar * 0.4f) * (i * 0.1f) * 200);
			rb.gameObject.transform.DORotate(new Vector3(45, 0, 0), 1f, RotateMode.Fast);
			rb.gameObject.layer = LayerMask.NameToLayer("ColorLayer");

		}
		StartCoroutine(isCalActive());
		gameEvents.CameraFollow?.Invoke();
	}


	IEnumerator isCalActive()
	{
		yield return new WaitForSeconds(1);
		isFinalPartStart = true;
	}

	private void SpeedCal()
	{
		if (isFinalPartStart == true)
		{

			float speed = stackManager.collectableColorsList[stackManager.collectableColorsList.Count - 1].GetComponent<Rigidbody>().velocity.magnitude;
			if (speed == 0)
			{
				gameEvents.NextLevel?.Invoke();
			}
		}

	}

}
