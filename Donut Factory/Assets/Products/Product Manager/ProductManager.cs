using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ProductManager : MonoBehaviour
{
	/**
	 * The singleton instance of the product manager for this level.
	 */
	public static ProductManager Instance {get; private set;} = null;
	
	[Tooltip("The grid all products snap to.")]
	public Grid productGrid;
	
	[Tooltip("All products managed by this product manager.")]
	public List<Product> products;
	
	/**
	 * Ensure there is only one instance.
	 */
	private void CheckInstance()
	{
		if (ProductManager.Instance == null)
		{
			ProductManager.Instance = this;
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
	}
	
	/**
	 * Destroy all the live products.
	 */
	public void ClearProducts()
	{
		foreach (Product prod in this.products)
		{
			Object.Destroy(prod.gameObject);
		}
	}
	
	/**
	 * Snap the given point to the product grid.
	 * @param pos The 2D world point to snap to the product grid.
	 * @return The center of the product cell that encompasses the given point.
	 */
	public Vector2 SnapPoint(Vector2 pos)
	{
		return this.productGrid.CellToWorld(this.productGrid.WorldToCell(pos)) 
			+ this.productGrid.GetLayoutCellCenter();
	}
	
	private void Awake()
	{
		this.CheckInstance();
	}
}
