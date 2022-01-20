using UnityEngine;

[RequireComponent(typeof(MachineBase))]
public sealed class ConveyorMachine : MonoBehaviour
{
	public void OnTick(float t)
	{
		Physics2D.OverlapPoint(this.transform.position, LayerMask.GetMask("Product"))
			?.GetComponent<Product>().Move(this.transform.right / t, t);
	}
}
