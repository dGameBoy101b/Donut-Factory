using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

public sealed class ToolBase : MonoBehaviour
{
	[Header("Player Input")]
	
	[Tooltip("The input manager name of the button used to used this tool on a location.")]
	public string useButton;
	
	[Tooltip("The input manager name of the button used to equip this tool.")]
	public string equipButton;
	
	[Header("Cursor")]
	
	[Tooltip("The cursor texture to use while this is equiped.")]
	public Texture2D cursorTexture;
	
	[Tooltip("Where the cursor hotspot is on the above cursor texture.")]
	public Vector2 hotspotOffset;
	
	[Header("Animation")]
	
	[Tooltip("The animator to use when this tool is equipped or unequipped.")]
	public Animator animator;
	
	[Tooltip("The animator name of the boolean variable used to equip this tool.")]
	public string equipBool;
	
	[Header("Events")]
	
	[Tooltip("The functions called when this tool is unequipped.")]
	public UnityEvent onUnequip;
	
	[Tooltip("The functions called when this tool is equipped.")]
	public UnityEvent onEquip;
	
	[Tooltip("The functions called when this tool is used.\nFirst argument is the 2D world position on which to use this tool.")]
	public UnityEvent<Vector2> onUse;

	/**
	 * Whether this tool is currently equipped.
	 */
	public bool Equipped
	{
		get
		{
			return this.animator.GetBool(this.equipBool);
		}
		set
		{
			this.animator.SetBool(this.equipBool, value);
			if (value != this.Equipped)
			{
				if (value)
				{
					this.onEquip.Invoke();
				}
				else
				{
					this.onUnequip.Invoke();
				}
			}
		}
	}

	/**
	 * Toggle this tool between equipped and unequipped.
	 */
	public void Toggle()
	{
		if (this.Equipped)
		{
			Toolbar.Instance.SetEquippedTool(null);
		}
		else
		{
			Toolbar.Instance.SetEquippedTool(this);
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown(this.equipButton))
		{
			this.Toggle();
		}
		if (this.Equipped && Input.GetButtonDown(this.useButton))
		{
			Vector3 world_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Physics2D.OverlapPoint(world_point, LayerMask.GetMask("Background")) != null
				&& Physics2D.OverlapPoint(Input.mousePosition, LayerMask.GetMask("UI")) == null)
			{
				this.Use(world_point);
			}
			
		}
	}

	/**
	 * Use this tool on the given location.
	 * @param pos The 2D world position to use this tool on.
	 */
	public void Use(Vector2 pos)
	{
		this.onUse.Invoke(pos);
	}
}
