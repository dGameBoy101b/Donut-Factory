using UnityEngine;

[RequireComponent(typeof(ControlBase))]
public sealed class PauseControl : MonoBehaviour
{
	public void Pause()
	{
		Controlbar.Instance.isTicking = false;
	}
	
	public void Unpause()
	{
		Controlbar.Instance.isTicking = true;
	}
}
