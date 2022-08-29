using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour, IInteract
{
	private MaterialController materialController;
	[SerializeField] private ColorDecider colorDecider1;


	private Material material;
	private CollectableColor[] collectableColors;
	private GameEvents gameEvents;
	private void Start()
	{
		gameEvents = GameEvents.instance;

		gameEvents.FeverModeStatusDecider += OnFeverMode;
		//colorDecider1 = (ColorDecider)(Random.Range(0, 3));
		materialController = MaterialController.instance;
		material = GetComponent<MeshRenderer>().material;
		materialController.ChangeColorStatus(colorDecider1, material);
	}
	public void Interact(Transform tr, ColorDecider colorDecider)
	{

		tr.GetComponent<CollectorController>().colorDeciderOfPlayer = colorDecider1;
		materialController.ChangeColorStatus(colorDecider1, tr.GetChild(0).GetComponent<MeshRenderer>().material);
		materialController.ChangeColorStatus(colorDecider1, tr.GetComponentInChildren<ChibiColor>().GetComponent<SkinnedMeshRenderer>().material);
		collectableColors = tr.parent.GetComponentsInChildren<CollectableColor>();
		for (int i = 0; i < collectableColors.Length; i++)
		{
			collectableColors[i].colorDeciderOfCol = colorDecider1;
			materialController.ChangeColorStatus(colorDecider1, collectableColors[i].GetComponent<CollectableColor>().materialOfCol);

		}


	}
	private void OnFeverMode(FeverModeStatus feverMode, GameObject go)
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
