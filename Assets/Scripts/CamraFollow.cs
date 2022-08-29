using UnityEngine;
using DG.Tweening;
public class CamraFollow : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offsetOfColor;
	private GameEvents gameEvents;
	private StackManager stackManager;
	[SerializeField] private Vector3 normalOffset;
	private Vector3 feverModeCamera;
	private bool cameraMov;
	private bool isFinalPart;
	private Vector3 cameraPosition;
	[SerializeField] private Vector3 finalPartPos;

	public static CamraFollow instance;
	private bool happened;

	public void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		cameraMov = true;
		stackManager = StackManager.instance;
		gameEvents = GameEvents.instance;
		gameEvents.FinalPartSet += InFinalPart;
		gameEvents.CameraFollow += Follow;
		gameEvents.CameraControl += CameraFollow;
		gameEvents.FeverModeStatusDecider += FeverCamera;

		feverModeCamera = new Vector3(normalOffset.x, normalOffset.y - 2, normalOffset.z + 4);
		gameEvents.WallCamera += WallCamera;
		gameEvents.CamShake += CamShake;
	}

	private void FixedUpdate()
	{
		cameraPosition = target.position + normalOffset;

		transform.position = Vector3.Lerp(transform.position, cameraPosition, 0.3f);
	}

	private void InFinalPart(FinalPartStatus finalPart)
	{
		if (finalPart == FinalPartStatus.Active)
		{
			cameraPosition = target.position + finalPartPos;
			isFinalPart = true;
		}
	}


	private void Follow()
	{
		target = stackManager.collectableColorsList[stackManager.collectableColorsList.Count - 1].gameObject.transform;
		normalOffset = offsetOfColor;

	}
	private void CameraFollow(CameraPositionStatus cameraPositionStatus)
	{
		if (cameraMov == true)
		{
			if (cameraPositionStatus == CameraPositionStatus.ZoomOut)
			{
				normalOffset = new Vector3(normalOffset.x, normalOffset.y + normalOffset.y * 0.006f, normalOffset.z + (normalOffset.z * 0.006f));

			}
			else
			{
				normalOffset = new Vector3(normalOffset.x, normalOffset.y - normalOffset.y * 0.006f, normalOffset.z - (normalOffset.z * 0.006f));

			}

		}
	}

	private void FeverCamera(FeverModeStatus feverModeStatus, GameObject gameObject)
	{
		if (feverModeStatus == FeverModeStatus.Activated && isFinalPart == false)
		{
			cameraMov = false;
			normalOffset = feverModeCamera;
		}
		else
		{
			normalOffset = new Vector3(normalOffset.x, normalOffset.y + 10, normalOffset.z - 18);
			cameraMov = true;
		}

	}
	private void WallCamera(Transform transform)
	{
		target = transform;
		if (happened == true)
			return;
		normalOffset = new Vector3(normalOffset.x, normalOffset.y - 5, normalOffset.z - 20);
		cameraPosition = target.position + normalOffset;
		happened = true;
	}


	private void CamShake(float a)
	{
		Camera.main.DOShakePosition(0.5f, a, fadeOut: true);
	}
}
