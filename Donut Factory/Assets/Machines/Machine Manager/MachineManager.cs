using System.Collections.Generic;
using UnityEngine;

public sealed class MachineManager : MonoBehaviour
{
	/**
	 * The singleton instance for the current scene.
	 */
	public static MachineManager Instance {get; private set;} = null;

	[Tooltip("The grid machines should be built on.")]
	public Grid buildGrid;

	[Tooltip("The list of machines in the scene.")]
	public List<MachineBase> machines;

	/**
	 * Whether the simulation is running.
	 */
	public bool Simulate {get; private set;} = false;

	/**
	 * Ensure only one instance exists.
	 */
	private void CheckInstance()
	{
		if (MachineManager.Instance == null)
		{
			MachineManager.Instance = this;
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	/**
	 * Scan all children for machines and add them to the list.
	 */
	public void FindAllMachines()
	{
		this.machines.Clear();
		for (int i = 0; i < this.transform.childCount; ++i)
		{
			this.machines.Add(this.transform.GetChild(i).gameObject.GetComponent<MachineBase>());
		}
	}

	/**
	 * Search for a machine at the given position.
	 * @param pos The 2D position to search.
	 * @return Null if no machine is found at the given position.
	 * @return The MachineBase machine at the given position.
	 */
	public MachineBase FindMachine(Vector2 pos)
	{
		return Physics2D.OverlapPoint(
			pos,
			LayerMask.GetMask("Floor Machine", "Wall Machine")
		)?.GetComponent<MachineBase>();
	}
	
	/**
	 * Snap the given point to the machine build grid.
	 * @param pos The 2D world point to snap to the build grid.
	 * @return The center of the build cell that encompasses the given point.
	 */
	public Vector2 SnapPoint(Vector2 pos)
	{
		return this.buildGrid.CellToWorld(this.buildGrid.WorldToCell(pos)) 
			+ this.buildGrid.GetLayoutCellCenter();
	}

	/**
	 * Stop the simulation of all machines.
	 */
	public void StopAllMachines()
	{
		this.Simulate = false;
		foreach (MachineBase machine in this.machines)
		{
			machine.Stop();
		}
	}
	
	/**
	 * Play the simulation of all machines.
	 */
	public void PlayAllMachines()
	{
		this.Simulate = true;
		foreach (MachineBase machine in this.machines)
		{
			machine.Play();
		}
	}
	
	/**
	 * Advance all machines in the simulation by one tick.
	 * @param t The duration of this tick in seconds.
	 */
	public void TickAllMachines(float t)
	{
		if (this.Simulate)
		{
			foreach (MachineBase machine in this.machines)
			{
				machine.Tick(t);
			}
		}
	}

	/**
	 * Check if the current level is complete.
	 * @return True if the current level is complete.
	 * @return False if the current level is not complete.
	 */
	public bool LevelComplete()
	{
		CollectorMachine c;
		for (int i = 0; i < this.transform.childCount; ++i)
		{
			if (this.transform.GetChild(i).gameObject.TryGetComponent<CollectorMachine>(out c) && c.productLeft > 0)
			{
				return false;
			}
		}
		return true;
	}

	private void Awake()
	{
		this.CheckInstance();
	}
}
