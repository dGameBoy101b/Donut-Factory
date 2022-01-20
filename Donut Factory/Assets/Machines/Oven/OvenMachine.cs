using UnityEngine;

[RequireComponent(typeof(MachineBase))]
public sealed class OvenMachine : MonoBehaviour
{
	public void OnTick(float t)
	{
		Attribute baked = Physics2D.OverlapPoint(this.transform.position, LayerMask.GetMask("Product"))
			?.GetComponent<ProductIcon>()?.GetAttribute("Baked");
		if (baked != null && baked.index < baked.Values.Count - 1)
		{
			baked.Value = baked.Values[baked.index + 1];
		}
	}
}
