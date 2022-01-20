using UnityEngine;

[ExecuteAlways]
public class BoundedCamera : FixedCamera
{
	[Header("View Size")]
	
	[Tooltip("The minimum width of the room visible at any one time in units.")]
	[Min(float.Epsilon)]
	public float viewWidth;
	
	[Tooltip("The minimum height of the room visible at any one time in units.")]
	[Min(float.Epsilon)]
	public float viewHeight;
	
	[Header("Camera Movement")]
	
	[Tooltip("The name of the horizontal axis in the input manager.")]
	public string horizontal;
	
	[Tooltip("The name of the vertical axis in the input manager.")]
	public string vertical;
	
	[Tooltip("The speed in units per second to move the camera at.")]
	[Min(float.Epsilon)]
	public float speed;
	
	/**
	 * The movement bounds of this camera.
	 */
	protected Bounds MoveBounds
	{
		get
		{
			Bounds bounds = this.RoomBounds;
			bounds.Expand(-this.padding);
			return bounds;
		}
	}
	
	/**
	 * Reposition the camera to ensure it remains within its movement bounds.
	 */
	protected void SnapToBounds()
	{
		this.transform.position = this.MoveBounds.ClosestPoint(this.transform.position) + Vector3.back;
	}
	
	/**
	 * Move this camera according to user input.
	 * @param t The number seconds since the last call to this method.
	 */
	protected void MoveCamera(float t)
	{
		this.transform.position += new Vector3(
			Input.GetAxis(this.horizontal) * this.speed * t,
			Input.GetAxis(this.vertical) * this.speed * t,
			0f);
	}
	
	private void Awake()
	{
		this.cam = this.GetComponent<Camera>();
		this.FitViewBounds(this.viewWidth, this.viewHeight);
		this.SnapToBounds();
	}
	
	protected virtual void Update()
	{
		if (Application.IsPlaying(this))
		{
			this.MoveCamera(Time.deltaTime);
		}
		else
		{
			this.FitViewBounds(this.viewWidth, this.viewHeight);
		}
		this.SnapToBounds();
	}
	
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		Gizmos.color = Color.green;
		this.cam = this.GetComponent<Camera>();
		Gizmos.DrawWireCube(this.MoveBounds.center, this.MoveBounds.size);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(this.transform.position, new Vector3(this.viewWidth, this.viewHeight, 0));
	}
}
