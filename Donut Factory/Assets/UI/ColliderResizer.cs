using UnityEngine;

[ExecuteAlways]
public sealed class ColliderResizer : MonoBehaviour
{
	[Tooltip("The box collider to resize.")]
	public BoxCollider2D box;

	[Tooltip("The rect transform to copy.")]
	public RectTransform rect;
	
	public void Resize()
	{
		this.box.size = this.rect.rect.size;
		this.box.offset = Vector2.Scale(this.rect.rect.size, Vector2.one * .5f - this.rect.pivot);
	}
	
	private void Start()
	{
		this.Resize();
		RectTransform.reapplyDrivenProperties += delegate(RectTransform rect)
		{
			if (rect == this.rect) 
			{
				this.Resize();
			}
		};
	}
	
	private void Update()
	{
		this.Resize();
	}
}
