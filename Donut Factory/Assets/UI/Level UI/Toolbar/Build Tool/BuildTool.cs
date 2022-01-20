using UnityEngine;

public sealed class BuildTool : MonoBehaviour
{
	[Tooltip("The machine this build tool builds.")]
	public MachineBase machine;
	
	/**
	 * Build the linked machine at the given position.
	 * @param pos The 2D world position to buuild the new machine.
	 */
	public void Build(Vector2 pos)
	{
		MachineBase machine = MachineManager.Instance.FindMachine(pos);
		if (machine == null || machine.Delete())
		{
			this.machine.Build(pos);
		}
	}
}
