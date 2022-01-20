using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public sealed class ControlBase : MonoBehaviour
{
	[Header("Player Input")]

	[Tooltip("The input manager name of the button used to activate this control.")]
	public string activateButton;

	[Header("Dependencies")]

	[Tooltip("The other controls to activate and deactive before this one when it is activated or deactivated respectively.")]
	public List<ControlBase> predecessors;
	
	[Tooltip("The other controls to deactivate and activate before this one when it is activated or deactivated respectively.")]
	public List<ControlBase> invertedPredecessors;

	[Header("Animation")]

	[Tooltip("The animator to use.")]
	public Animator animator;

	[Tooltip("The animator name of the boolean used to track whether this control is active.")]
	public string activeBool;
	
	[Header("Events")]

	[Tooltip("The functions to call when this control becomes active.")]
	public UnityEvent onActivate;

	[Tooltip("The functions to call when this control becomes inactive.")]
	public UnityEvent onDeactivate;

	public bool Active {get; private set;} = false;
	
	public void Activate()
	{
		foreach (ControlBase pre in this.predecessors)
		{
			pre.Activate();
		}
		foreach (ControlBase inv in this.invertedPredecessors)
		{
			inv.Deactivate();
		}
		this.Active = true;
		this.animator.SetBool(this.activeBool, true);
		this.onActivate.Invoke();
	}
	
	public void Deactivate()
	{
		foreach (ControlBase pre in this.predecessors)
		{
			pre.Deactivate();
		}
		foreach (ControlBase inv in this.invertedPredecessors)
		{
			inv.Activate();
		}
		this.Active = false;
		this.animator.SetBool(this.activeBool, false);
		this.onDeactivate.Invoke();
	}

	/**
	 * Toggle this control.
	 */
	public void Toggle()
	{
		if (this.Active)
		{
			this.Deactivate();
		}
		else
		{
			this.Activate();
		}
	}
}
