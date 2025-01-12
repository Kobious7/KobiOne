using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SkillButtonAttribute))]
public class SkillButtonPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Get the required ButtonType from the attribute
        SkillButtonAttribute skillButtonAttribute = (SkillButtonAttribute)attribute;
        SkillButton requiredType = skillButtonAttribute.RequiredSkillButton;

        // Get the SkillSO instance assigned to the field
        SkillSO skillSO = property.objectReferenceValue as SkillSO;

        if (skillSO != null && skillSO.Button != requiredType)
        {
            // Show a warning if the assigned SkillSO has the wrong ButtonType
            GUI.color = Color.red;
            EditorGUI.PropertyField(position, property, label);
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.HelpBox(position, $"Only SkillSO with ButtonType '{requiredType}' can be assigned here.", MessageType.Warning);
            GUI.color = Color.white;
        }
        else
        {
            // Draw the property field normally if it's correct or empty
            EditorGUI.PropertyField(position, property, label);
        }

        EditorGUI.EndProperty();
    }
}