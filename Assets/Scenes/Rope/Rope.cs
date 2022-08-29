using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

	[SerializeField] List<GameObject> ropeList = new List<GameObject>();

	[SerializeField] private GameObject game;
	void Update()
	{
		ropeList[0].gameObject.transform.position = transform.position;

		for (int i = 1; i < ropeList.Count; i++)
		{
			ropeList[i].transform.position = Vector3.Lerp(ropeList[i].transform.position,
			new Vector3(ropeList[i - 1].transform.position.x, ropeList[i].transform.position.y,
			ropeList[i - 1].transform.position.z), 0.2f);
		}
	}
}
