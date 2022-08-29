using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IInteractForColor
{
	GameEvents gameEvents;


	[SerializeField] Vector3 offset;
	private void Start()
	{
		gameEvents = GameEvents.instance;
	}

	public void InteractForColor(Transform trans)
	{
		StartCoroutine(StartCountDownForEnd());
		gameEvents.WallCamera?.Invoke(transform);

	}


	IEnumerator StartCountDownForEnd()
	{
		yield return new WaitForSeconds(4);
		gameEvents.NextLevel?.Invoke();
	}

}
