using UnityEngine;

public sealed class RotateTool : MonoBehaviour
{
	/**
	 * Rotate the machine at the given position.
	 * @param pos The 2D world position of the machine to rotate.
	 */
	public void Rotate(Vector2 pos)
	{
		MachineBase machine = MachineManager.Instance.FindMachine(pos);
		if (machine != null)
		{
			machine.RotateCW();
		}
	}
}
