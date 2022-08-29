using UnityEngine;



public interface IInteract
{
	public void Interact(Transform tr, ColorDecider colorDecider);
}

public interface IInteractForColor
{
	public void InteractForColor(Transform trans);
}
public interface IInteractExit
{
	public void Exit(Transform transform);
}

public class Interfaces : MonoBehaviour
{



}
