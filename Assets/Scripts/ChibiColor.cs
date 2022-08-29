using UnityEngine;

public class ChibiColor : MonoBehaviour
{
	private ColorDecider colorDeciderOfChibi;
	public Color playerColor;
	private MaterialController materialController;
	private Material materialOfChibi;
	private SkinnedMeshRenderer skinnedMeshRenderer;
	void Start()
	{
		colorDeciderOfChibi = transform.GetComponentInParent<CollectorController>().colorDeciderOfPlayer;
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
		materialController = MaterialController.instance;
		materialOfChibi = skinnedMeshRenderer.material;
		materialOfChibi.SetColor("_Color", playerColor);
		materialController.ChangeColorStatus(colorDeciderOfChibi, materialOfChibi);
	}

}
