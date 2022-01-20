using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class MachineBase : MonoBehaviour
{
	[Header("Player Editing")]
	
	[Tooltip("Whether the player can delete this machine.")]
	public bool deleteable;
	
	[Tooltip("Whether the player can rotate this machine.")]
	public bool rotateable;
	
	[Tooltip("Whether the player can build copies of this machine.")]
	public bool buildable;
	
	[Header("Animation")]
	
	[Tooltip("The animator used to control this machine.")]
	public Animator animator;
	
	[Tooltip("The animator name of the trigger to set when the simulation begins.")]
	public string playTrigger;
	
	[Tooltip("The animator name of the trigger to set when the simulation ends.")]
	public string stopTrigger;
	
	[Header("Events")]
	
	[Tooltip("The functions called when the simulation starts.")]
	public UnityEvent onPlay;
	
	[Tooltip("The functions called when the simulation stops.")]
	public UnityEvent onStop;
	
	[Tooltip("The functions called when the simulation is advanced one tick.\nThe first argument is the duration of this tick in seconds.")]
	public UnityEvent<float> onTick;

	/**
	 * Build this machine at the given position.
	 * @param pos The world position at which to build.
	 * @return The GameObject that was built.
	 * @return Null if building failed.
	 */
	public GameObject Build(Vector2 pos)
	{
		if (!MachineManager.Instance.Simulate && this.buildable
		    && MachineManager.Instance.FindMachine(pos) == null)
		{
			GameObject machine = GameObject.Instantiate(
				this.gameObject, 
				MachineManager.Instance.SnapPoint(pos),
				this.transform.rotation,
				MachineManager.Instance.transform);
			return machine;
		}
		return null;
	}

	/**
	 * Delete this machine.
	 * @return True if the deletion succeeded.
	 * @return False if the deletion failed.
	 */
	public bool Delete()
	{
		if (!MachineManager.Instance.Simulate && this.deleteable)
		{
			GameObject.Destroy(this.gameObject);
			return true;
		}
		return false;
	}
	
	/**
	 * Rotate this machine by the given number of quarter turns.
	 * @param turns The number of quarter turns to make.
	 * @return True if the rotation succeeded.
	 * @return False if the rotation failed.
	 */
	private bool Rotate(int turns)
	{
		if (!MachineManager.Instance.Simulate && this.rotateable)
		{
			transform.Rotate(0, 0, turns * 90);
			return true;
		}
		return false;
	}

	/**
	 * Rotate this machine clockwise.
	 * @return True if the rotation succeeded.
	 * @return False if the rotation failed.
	 */
	public bool RotateCW()
	{
		return this.Rotate(-1);
	}
	
	/**
	 * Rotate this machine counter-clockwise.
	 * @return True if the rotation succeeded.
	 * @return False if the rotation failed.
	 */
	public bool RotateCCW()
	{
		return this.Rotate(1);
	}
	
	/**
	 * Rotate this machine to face upwards.
	 * @return True if the rotation succeeded.
	 * @return False if the rotation failed.
	 */
	public bool RotateUp()
	{
		return this.Rotate((int)(Vector3.SignedAngle(this.transform.right, Vector3.up, Vector3.forward) / 90));
	}
	
	/**
	 * Rotate this machine to face upwards.
	 * @return True if the rotation succeeded.
	 * @return False if the rotation failed.
	 */
	public bool RotateLeft()
	{
		return this.Rotate((int)(Vector3.SignedAngle(this.transform.right, Vector3.left, Vector3.forward) / 90));
	}
	
	/**
	 * Rotate this machine to face upwards.
	 * @return True if the rotation succeeded.
	 * @return False if the rotation failed.
	 */
	public bool RotateDown()
	{
		return this.Rotate((int)(Vector3.SignedAngle(this.transform.right, Vector3.down, Vector3.forward) / 90));
	}
	
	/**
	 * Rotate this machine to face upwards.
	 * @return True if the rotation succeeded.
	 * @return False if the rotation failed.
	 */
	public bool RotateRight()
	{
		return this.Rotate((int)(Vector3.SignedAngle(this.transform.right, Vector3.right, Vector3.forward) / 90));
	}

	/**
	 * Play the simulation for this machine.
	 */
	public void Play()
	{
		this.animator.SetTrigger(this.playTrigger);
		this.onPlay.Invoke();
	}
	
	/**
	 * Stop the simulation for this machine.
	 */
	public void Stop()
	{
		this.animator.SetTrigger(this.stopTrigger);
		this.onStop.Invoke();
	}
	
	/**
	 * Advance this machine in the simulation by one tick.
	 * @param t The float duration of this tick in seconds.
	 */
	public void Tick(float t)
	{
		this.onTick.Invoke(t);
	}
}
