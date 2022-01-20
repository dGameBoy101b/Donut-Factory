using UnityEngine;

[RequireComponent(typeof(ControlBase))]
public sealed class FastForwardControl : MonoBehaviour
{
	[Tooltip("The duration of a tick while this is active.")]
	[Min(float.Epsilon)]
	public float tickDuration;
	
	/**
	 * The duration of a tick in seconds before the simulation was fast forwarded.
	 */
	public float LastTickDuration {get; private set;}
	
	public void FastForward()
	{
		this.LastTickDuration = Controlbar.Instance.tickDuration;
		Controlbar.Instance.tickDuration = this.tickDuration;
	}
	
	public void UnFastForward()
	{
		Controlbar.Instance.tickDuration = this.LastTickDuration;
	}
}
