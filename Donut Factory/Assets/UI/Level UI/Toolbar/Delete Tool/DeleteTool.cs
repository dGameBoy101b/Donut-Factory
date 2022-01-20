using UnityEngine;

public sealed class DeleteTool : MonoBehaviour
{
	/**
	 * Delete the machine at the given location.
	 * @param pos The 2D world position of the machine to delete.
	 */
	public void Delete(Vector2 pos)
	{
		MachineBase machine = MachineManager.Instance.FindMachine(pos);
		if (machine != null)
		{
			machine.Delete();
		}
	}
}
