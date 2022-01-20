using UnityEngine;

[RequireComponent(typeof(ControlBase))]
public sealed class StepControl : MonoBehaviour
{
	public void Step()
	{
		MachineManager.Instance.TickAllMachines(Controlbar.Instance.tickDuration);
	}
}
