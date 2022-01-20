using System.Collections.Generic;
using UnityEngine;

public class ProductIcon : MonoBehaviour, System.IEquatable<ProductIcon>
{
	/**
	 * The attributes attacted to this icon.
	 */
	public List<Attribute> attributes;
	
	/**
	 * The animator attacted to this icon.
	 */
	private Animator anim;
	
	/**
	 * Get the given attribute from this product icon.
	 * @param name The string name of the attribute to get.
	 * @return The requested attribute if found.
	 * @return Null if the requested type was not found.
	 */
	public Attribute GetAttribute(string name)
	{
		foreach (Attribute attr in this.attributes)
		{
			if (attr.Name == name)
			{
				return attr;
			}
		}
		return null;
	}
	
	public bool Equals(ProductIcon other)
	{
		if (other == null 
		    || this.attributes.Count != other.attributes.Count)
		{
			return false;
		}
		foreach (Attribute attr in this.attributes)
		{
			if (!other.attributes.Exists(
				delegate(Attribute a){return attr.Equals(a);}))
			{
				return false;
			}
		}
		return true;
	}
	
	private void Awake()
	{
		this.anim = this.GetComponent<Animator>();
		foreach (Attribute attr in this.attributes)
		{
			this.anim.SetInteger(attr.Name, attr.index);
			attr.OnChange.Add(
				delegate(string o, string n){
					this.anim.SetInteger(attr.Name, attr.index);});
		}
	}
}
