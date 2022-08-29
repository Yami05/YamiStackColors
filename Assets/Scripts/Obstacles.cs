using UnityEngine;

public class Obstacles : MonoBehaviour, IInteract
{

	private GameEvents gameEvents;
	private Rigidbody rb;
	StackManager stackManager;
	private void Start()
	{
		stackManager = StackManager.instance;
		gameEvents = GameEvents.instance;
		gameEvents.FeverModeStatusDecider += OnFeverMode;
	}

	public void Interact(Transform tr, ColorDecider colorDecider)
	{


		int a = Random.Range(1, 7);

		Rigidbody rbOfP = tr.GetComponent<Rigidbody>();

		rbOfP.AddForce(Vector3.Lerp(tr.position, new Vector3(-tr.position.x * 1, tr.position.y, tr.position.z * 0.1f), 1f), ForceMode.VelocityChange);
		gameEvents.CamShake?.Invoke(0.2f);
		for (int i = 1; i <= a; i++)
		{


			if (a > stackManager.collectableColorsList.Count)
			{

				rb.gameObject.GetComponent<BoxCollider>().isTrigger = false;
				rb.AddRelativeForce(800, 800, 800);
				gameEvents.GameOver?.Invoke();
				return;
			}
			rb = stackManager.collectableColorsList[(stackManager.collectableColorsList.Count - 1)].gameObject.AddComponent<Rigidbody>();
			rb.AddRelativeForce(200 * i, 200 * i, 200 * i);
			rb.gameObject.layer = LayerMask.NameToLayer("DroppedLayer");
			rb.gameObject.GetComponent<BoxCollider>().isTrigger = false;
			rb.transform.SetParent(null);
			stackManager.collectableColorsList.Remove(rb.gameObject);
			Destroy(rb.gameObject, 3);

		}
	}


	private void OnFeverMode(FeverModeStatus feverMode, GameObject gameOb)
	{
		if (feverMode == FeverModeStatus.Activated)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
	}
}
