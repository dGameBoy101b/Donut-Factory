using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute : MonoBehaviour, System.IEquatable<Attribute>
{
	[Tooltip("The name of this attribute.")]
	public string Name;
	
	[Tooltip("The list of valid values.")]
	public List<string> Values;
	
	[Tooltip("The current index value of this attribute.")]
	[Min(0)]
	[SerializeField]
	public int index;
	
	[Tooltip("The functions to call when the value of this attribute is changed.\nFirst parameter is passed the old value and the second parameter is passed the new value.")]
	public List<System.Action<string, string>> OnChange = new List<System.Action<string, string>>();
	
	/**
	 * The current value of this attribute.
	 */
	public string Value
	{
		get
		{
			return this.Values[this.index];
		}
		set
		{
			int index = this.Values.IndexOf(value);
			if (index < 0)
			{
				throw new System.Collections.Generic.KeyNotFoundException();
			}
			string old = this.Value;
			this.index = index;
			foreach (System.Action<string, string> listener in this.OnChange)
			{
				listener(old, value);
			}
		}
	}
	
	public bool Equals(Attribute other)
	{
		if (other == null
		    || !this.Name.Equals(other.Name)
		    || !this.Value.Equals(other.Value)
		    || this.Values.Count != other.Values.Count)
		{
			return false;
		}
		foreach (string val in this.Values)
		{
			if (!other.Values.Contains(val))
			{
				return false;
			}
		}
		return true;
	}
	
	public void OnValidate()
	{
		if (this.index >= this.Values.Count)
		{
			Debug.LogWarning("Index exceeds number of valid values.\nIndex reduced to valid maximum.");
			this.index = this.Values.Count - 1;
		}
	}
}
