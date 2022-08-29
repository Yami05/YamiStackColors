using UnityEngine;

public class Animations : MonoBehaviour
{
	[SerializeField] private Animator anim;

	private GameEvents gameEvents;

	private void Start()
	{
		gameEvents = GameEvents.instance;

		gameEvents.StartTheGame += Run;
		gameEvents.FinalPartSet += Kick;
		gameEvents.GameOver += Fail;
	}
	private void Run()
	{



		anim.SetBool("isRunning", true);
	}

	private void Kick(FinalPartStatus finalPartStatus)
	{
		if (finalPartStatus == FinalPartStatus.Disable)
		{
			anim.SetBool("isRunning", false);
			anim.SetBool("isKicking", true);

		}
	}
	private void StartKicking()
	{
		gameEvents.Kick?.Invoke(transform.root.GetComponentInChildren<StackManager>().transform);
	}

	private void Fail()
	{
		anim.SetBool("isRunning", false);
		anim.SetBool("fail", true);
	}
}
