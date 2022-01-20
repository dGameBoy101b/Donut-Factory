using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public sealed class Toolbar : MonoBehaviour
{
	public static Toolbar Instance {get; private set;} = null;

	[Header("Cursor")]

	[Tooltip("The cursor texture to use while no tool is equiped.")]
	public Texture2D cursorTexture;

	[Tooltip("Where the cursor hotspot is on the above cursor texture.")]
	public Vector2 hotspotOffset;
	
	[Header("Contents")]
	
	[Tooltip("The enabled tools in this toolbar.")]
	public List<ToolBase> tools;

	/**
	 * Ensure only one toolbar exists.
	 */
	private void CheckInstance()
	{
		if (Toolbar.Instance == null)
		{
			Toolbar.Instance = this;
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	/**
	 * Equip only the given tool in this toolbar.
	 * @param tool The tool to equip.
	 */
	public void SetEquippedTool(ToolBase tool)
	{
		foreach (ToolBase t in this.tools)
		{
			t.Equipped = t == tool;
		}
		if (tool == null)
		{
			Cursor.SetCursor(this.cursorTexture, this.hotspotOffset, CursorMode.Auto);
		}
		else
		{
			Cursor.SetCursor(tool.cursorTexture, tool.hotspotOffset, CursorMode.Auto);
		}
	}

	private void Start()
	{
		this.SetEquippedTool(null);
	}

	private void Awake()
	{
		this.CheckInstance();
	}
}
