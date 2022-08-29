using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
	public static GameEvents instance;
	void Awake()
	{
		instance = this;
	}

	public Action GameOver;
	public Action StartTheGame;
	public Action<FeverModeCount> FeverModeCounter;
	public Action<FeverModeStatus, GameObject> FeverModeStatusDecider;
	public Action<FinalPartStatus> FinalPartSet;
	public Action CameraFollow;
	public Action CountOfCollectable;
	public Action<int> multiplier;
	public Action NextLevel;
	public Action Score;
	public Action<Transform> Kick;
	public Action<CameraPositionStatus> CameraControl;
	public Action<Transform> WallCamera;
	public Action<float> CamShake;
}
