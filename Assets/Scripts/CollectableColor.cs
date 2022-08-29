using UnityEngine;
public class CollectableColor : MonoBehaviour, IInteract
{

	private HerdControl herdControl;
	private StackManager stackManager;
	public ColorDecider colorDeciderOfCol;



	private GameEvents gameEvents;

	public int score;

	[SerializeField] private Color colorOfThis;
	public Material materialOfCol;
	private MaterialPropertyBlock materialPropertyBlock;
	public MaterialController materialControllerOfCol;
	private MeshRenderer mRenderer;


	private void Start()
	{
		stackManager = StackManager.instance;
		score = 1;
		gameEvents = GameEvents.instance;
		herdControl = transform.GetComponentInParent<HerdControl>();
		colorDeciderOfCol = herdControl.currentColorOfHerd;
		materialControllerOfCol = MaterialController.instance;
		mRenderer = GetComponent<MeshRenderer>();
		materialPropertyBlock = new MaterialPropertyBlock();
		mRenderer.SetPropertyBlock(materialPropertyBlock);
		materialOfCol = mRenderer.material;



		gameEvents.FeverModeStatusDecider += FeverModeActive;


		colorDeciderOfCol = herdControl.currentColorOfHerd;
		materialControllerOfCol.ChangeColorStatus(colorDeciderOfCol, materialOfCol);
		materialPropertyBlock.SetColor("_Color", colorOfThis);

	}
	public void Interact(Transform tr, ColorDecider cDOfPlayer)
	{

		if (cDOfPlayer == colorDeciderOfCol)
		{
			transform.SetParent(tr.root.GetComponentInChildren<StackManager>().gameObject.transform);

			stackManager.collectableColorsList.Add(this.gameObject);

			colorDeciderOfCol = tr.GetComponentInParent<CollectorController>().colorDeciderOfPlayer;

			gameEvents.FeverModeCounter?.Invoke(FeverModeCount.Increase);

			gameEvents.CameraControl?.Invoke(CameraPositionStatus.ZoomOut);

		}
		else
		{
			stackManager.GameEndControl();

			//gameEvents.CamShake?.Invoke(0.1f);
			if (stackManager.collectableColorsList.Count >= 1)
			{

				Destroy(gameObject);
				GameObject go = stackManager.collectableColorsList[stackManager.collectableColorsList.Count - 1].gameObject;
				if (stackManager.collectableColorsList.Count >= 1)
				{
					Destroy(go);
				}
				stackManager.collectableColorsList.Remove(go);
			}
			gameEvents.FeverModeCounter?.Invoke(FeverModeCount.Decrease);
			gameEvents.CameraControl?.Invoke(CameraPositionStatus.ZoomIn);


		}
		gameEvents.CountOfCollectable?.Invoke();



	}

	private void FeverModeActive(FeverModeStatus feverMode, GameObject gameObject)
	{

		switch (feverMode)
		{

			case FeverModeStatus.Activated:
				colorDeciderOfCol = herdControl.currentColorOfHerd;
				materialControllerOfCol.ChangeColorStatus(herdControl.currentColorOfHerd, materialOfCol);
				break;

			case FeverModeStatus.Disabled:


				if (stackManager.collectableColorsList.Contains(this.gameObject))
				{
					ChangeColor(gameObject);
				}
				else
				{
					colorDeciderOfCol = herdControl.currentColorOfHerd;
					materialControllerOfCol.ChangeColorStatus(herdControl.currentColorOfHerd, materialOfCol);
				}
				break;
			default:
				break;
		}
	}
	private void ChangeColor(GameObject go)
	{
		colorDeciderOfCol = go.GetComponent<CollectorController>().colorDeciderOfPlayer;
		materialControllerOfCol.ChangeColorStatus(go.GetComponent<CollectorController>().colorDeciderOfPlayer,
	  go.GetComponent<CollectorController>().materialOfPlayer);
	}

	private void OnDestroy()
	{
		gameEvents.FeverModeStatusDecider -= FeverModeActive;


	}

	private void OnTriggerEnter(Collider other)
	{

		other.gameObject.GetComponent<IInteractForColor>()?.InteractForColor(stackManager.collectableColorsList[stackManager.collectableColorsList.Count - 1].transform);


	}

	private void OnCollisionEnter(Collision collision)
	{
		collision.gameObject.GetComponent<IInteractForColor>()?.InteractForColor(transform);
	}

}
