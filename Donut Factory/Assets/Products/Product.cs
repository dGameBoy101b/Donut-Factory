using System.Collections.Generic;
using UnityEngine;

public sealed class Product : MonoBehaviour
{
	/**
	 * Whether this product is currently moving.
	 */
	public bool IsMoving {get; private set;} = false;
	
	/**
	 * The rigidbody attached to this product.
	 */
	private Rigidbody2D rb;

	/**
	 * Move this product.
	 * @param velocity The velocity at which to move this product.
	 * @param time The float number of seconds to move for.
	 */
	public void Move(Vector2 velocity, float time)
	{
		if (!this.IsMoving)
		{
			this.IsMoving = true;
			this.rb.velocity = velocity;
			this.Invoke("Stop", time);
		}
	}
	
	/**
	 * Stop this product from moving.
	 */
	public void Stop()
	{
		this.IsMoving = false;
		this.rb.velocity = Vector3.zero;
		this.transform.position = ProductManager.Instance.SnapPoint(this.transform.position);
		Debug.Break();
	}

	/**
	 * Determine if this product should be destroyed.
	 * @return True if this product should be destroyed.
	 * @return False if this product should not be destroyed.
	 */
	private bool ShouldDie()
	{
		return Physics2D.OverlapPoint(this.transform.position, LayerMask.GetMask("Floor Machine")) == null;
	}

	private void FixedUpdate()
	{
		if (!this.IsMoving && this.ShouldDie())
		{
			Debug.Log(this.gameObject + " product destroyed at " + this.transform.position.ToString());
			Debug.Break();
			GameObject.Destroy(this.gameObject);
		}
	}
	
	private void Awake()
	{
		this.rb = this.GetComponent<Rigidbody2D>();
	}
	
	private void Start()
	{
		ProductManager.Instance.products.Add(this);
	}
	
	private void OnDestroy()
	{
		ProductManager.Instance.products.Remove(this);
	}
}
