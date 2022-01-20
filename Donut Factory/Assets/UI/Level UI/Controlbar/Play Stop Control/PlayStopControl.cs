using UnityEngine;

[RequireComponent(typeof(ControlBase))]
public sealed class PlayStopControl : MonoBehaviour
{
	public void Stop()
	{
		Controlbar.Instance.isTicking = false;
		Controlbar.Instance.tickDuration = 0f;
		MachineManager.Instance.StopAllMachines();
		ProductManager.Instance.ClearProducts();
	}

	public void Play()
	{
		Controlbar.Instance.isTicking = true;
		Controlbar.Instance.tickDuration = 1f;
		MachineManager.Instance.PlayAllMachines();
	}
}
