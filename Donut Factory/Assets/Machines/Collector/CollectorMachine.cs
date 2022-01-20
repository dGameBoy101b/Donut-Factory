using UnityEngine;

[RequireComponent(typeof(MachineBase))]
public class CollectorMachine : MonoBehaviour
{
	[Header("Production Target")]
	
	[Tooltip("The product accepted by this collector.")]
	public ProductIcon target;
	
	[Tooltip("The number of required products before this collector is satisfied.")]
	[Min(1)]
	public int requiredCount;
	
	[Header("Animation")]
	
	[Tooltip("The animator to use.")]
	public Animator animator;
	
	[Tooltip("The animator name for the float which stores the proportion of product to recieve remaining.")]
	public string remainingFloat;
	
	/**
	 * The number of products left to submit.
	 */
	public int productLeft {get; private set;}

	public void OnTick(float t)
	{
		GameObject other = Physics2D.OverlapPoint(this.transform.position, LayerMask.GetMask("Product"))?.gameObject;
		if (other != null)
		{
			if (this.Acceptable(other))
			{
				this.Accept(other);
			}
			else
			{
				this.Reject(other);
			}
		}
	}
	
	/**
	 * Reset the accepted product counter.
	 */
	public void ResetCount()
	{
		this.productLeft = this.requiredCount;
		this.animator.SetFloat(this.remainingFloat, 1f);
	}

	/**
	 * Test if the given product is acceptable.
	 * @param other The product to test.
	 * @return True if the product is acceptable.
	 * @return False if the product is unacceptable.
	 */
	private bool Acceptable(GameObject other)
	{
		return this.target.Equals(other.GetComponent<ProductIcon>());
	}

	/**
	 * Accept the given product.
	 */
	private void Accept(GameObject other)
	{
		Debug.Log("Product Accepted");
		if (this.productLeft > 0)
		{
			this.productLeft--;
			this.animator.SetFloat(this.remainingFloat, (float)this.productLeft / (float)this.requiredCount);
		}
		GameObject.Destroy(other);
		if (MachineManager.Instance.LevelComplete())
		{
			Debug.Log("Level Complete.");
		}
	}

	/**
	 * Reject the given product.
	 */
	private void Reject(GameObject other)
	{
		Debug.Log("Product Rejected");
		MachineManager.Instance.StopAllMachines();
	}

	private void Awake()
	{
		this.ResetCount();
	}
}
