using UnityEngine;

[RequireComponent(typeof(MachineBase))]
public sealed class SpawnerMachine : MonoBehaviour
{
	[Header("Product Spawning")]
	
	[Tooltip("The number of ticks between spawns.")]
	[Min(1)]
	public int spawnRate;
	
	[Tooltip("The number of ticks before the first spawn.")]
	[Min(0)]
	public int startTime;
	
	[Tooltip("The product to spawn.")]
	public Product spawn;

	/**
	 * The number of ticks until the next spawn.
	 */
	public int Timer {get; private set;}

	/**
	 * Reset the spawn timer to its start time.
	 */
	public void ResetTimer()
	{
		this.Timer = startTime;
	}

	/**
	 * Spawn a clone of the source product.
	 */
	public void Spawn()
	{
		this.Timer = this.spawnRate;
		GameObject prod = GameObject.Instantiate(
			this.spawn.gameObject,
			this.transform.position + this.transform.right,
			Quaternion.identity,
			ProductManager.Instance.transform);
		prod.SetActive(true);
		Debug.Break();
	}

	public void OnTick(float t)
	{
		this.Timer--;
		if (this.Timer <= 0)
		{
			this.Spawn();
		}
	}
}
