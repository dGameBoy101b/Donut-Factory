using UnityEngine;
using System.Collections.Generic;

public sealed class Controlbar : MonoBehaviour
{
	public static Controlbar Instance {get; private set;} = null;

	[Header("Contents")]

	[Tooltip("The controls in this control bar.")]
	public List<ControlBase> controls;

	[Header("Time Control")]

	[Tooltip("Whether the next tick is automatically triggered after the current tick has ended.")]
	public bool isTicking;

	[Tooltip("The duriation of a tick in seconds.")]
	public float tickDuration;
	
	/**
	 * The time until the next auto tick in seconds.
	 */
	public float Timer {get; private set;} = 0f;

	/**
	 * Ensure only one controlbar exists.
	 */
	private void CheckInstance()
	{
		if (Controlbar.Instance == null)
		{
			Controlbar.Instance = this;
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	private void Awake()
	{
		this.CheckInstance();
	}
	
	private void Update()
	{
		if (MachineManager.Instance.Simulate && this.isTicking)
		{
			this.Timer -= Time.deltaTime;
			if (this.Timer <= 0f)
			{
				this.Timer = this.tickDuration;
				MachineManager.Instance.TickAllMachines(this.tickDuration);
			}
		}
	}
}
