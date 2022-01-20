using UnityEngine;

[ExecuteAlways]
public class FixedCamera : MonoBehaviour
{
	[Header("Room Size")]
	
	[Tooltip("The collider that encompasses the whole room.")]
	[Min(0)]
	public Collider2D roomCollider;
	
	[Tooltip("The amount of additional white space to have around the room in units.")]
	public float padding;
	
	/**
	 * The camera component attached to this camera.
	 */
	protected Camera cam;
	
	/**
	 * The bounds of the room.
	 */
	protected Bounds RoomBounds
	{
		get
		{
			if (this.roomCollider == null)
			{
				return new Bounds();
			}
			Bounds bounds = this.roomCollider.bounds;
			bounds.Expand(this.padding);
			return bounds;
		}
	}

	/**
	 * Resize the view of this camera to fit the given bounds.
	 * @param width The width of the bounds in units.
	 * @param height The height of the bound sin units.
	 */
	protected void FitViewBounds(float width, float height)
	{
		float h = height / 2;
		float w = width / 2 / this.cam.aspect;
		this.cam.orthographicSize = System.Math.Max(w, h);
	}
	
	private void Awake()
	{
		this.cam = this.GetComponent<Camera>();
		this.FitViewBounds(this.RoomBounds.size.x, this.RoomBounds.size.y);
		this.transform.position = this.RoomBounds.center + Vector3.back;
	}
	
	private void Update()
	{
		if (!Application.IsPlaying(this))
		{
			this.FitViewBounds(this.RoomBounds.size.x, this.RoomBounds.size.y);
			this.transform.position = this.RoomBounds.center + Vector3.back;
		}
	}
	
	protected virtual void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(this.RoomBounds.center, this.RoomBounds.size);
	}
}
