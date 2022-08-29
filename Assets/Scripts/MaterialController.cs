using UnityEngine;

public class MaterialController : MonoBehaviour
{
	public static MaterialController instance;


	private void Awake()
	{
		instance = this;
	}

	public void ChangeColorStatus(ColorDecider colorDecider, Material material)
	{

		switch (colorDecider)
		{
			case ColorDecider.Blue:
				material.color = Color.blue;
				break;
			case ColorDecider.Red:
				material.color = Color.red;
				break;
			case ColorDecider.Purple:
				material.color = Color.magenta;
				break;
			case ColorDecider.Green:
				material.color = Color.green;
				break;

			default:
				break;
		}
	}
}
