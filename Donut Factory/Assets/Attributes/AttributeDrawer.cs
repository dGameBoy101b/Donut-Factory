using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Attribute))]
public class AttributeDrawer : PropertyDrawer
{
	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		EditorGUI.BeginProperty(pos, label, prop);
		pos = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		Rect objRect = pos;
		if (prop.objectReferenceValue != null)
		{
			objRect = new Rect(pos.x, pos.y, pos.width / 2 - 5, pos.height);
		}
		prop.objectReferenceValue = EditorGUI.ObjectField(
			objRect, prop.objectReferenceValue, 
			typeof(Attribute), true);
		if (prop.objectReferenceValue != null)
		{
			Attribute attr = prop.objectReferenceValue as Attribute;
			Rect enumRect = new Rect(
				pos.x + pos.width / 2 + 5, pos.y, 
				pos.width / 2 - 5, pos.height);
			if(EditorGUI.DropdownButton(enumRect, new GUIContent(attr.Value), FocusType.Passive))
			{
				GenericMenu enumDropdown = new GenericMenu();
				foreach (string val in attr.Values)
				{
					enumDropdown.AddItem(
						new GUIContent(val),
						attr.Value == val,
						delegate(){
							attr.Value = val;
							PrefabUtility.RecordPrefabInstancePropertyModifications(attr);});
				}
				enumDropdown.DropDown(enumRect);
			}
		}
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}
}
