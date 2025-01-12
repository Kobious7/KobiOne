using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HideByOpAttribute))]
public class HideByOpAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Find the Skill script that this property belongs to
        OpSkillSO skill = property.serializedObject.targetObject as OpSkillSO;

        if (skill != null && property.propertyType == SerializedPropertyType.Enum)
        {
            // Get the current value of the mainTarget property
            SkillTarget mainTargetValue = skill.MainTarget;

            // Get the enum values as strings
            string[] enumNames = property.enumDisplayNames;

            // Filter out values that contain "Tile" if mainTarget is "Tile"
            string[] filteredEnumNames;
                
            filteredEnumNames = System.Array.FindAll(enumNames, name => !name.Contains(mainTargetValue.ToString()) && !name.Contains("TILE"));

            // Display the filtered enum popup
            int selectedIndex = System.Array.IndexOf(filteredEnumNames, property.enumDisplayNames[property.enumValueIndex]);
            if (selectedIndex < 0) selectedIndex = 0; // Fallback to the first option if an invalid selection is made

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, filteredEnumNames);

            // Map the selected index back to the original enum and set the property value
            string selectedName = filteredEnumNames[selectedIndex];
            property.enumValueIndex = System.Array.IndexOf(enumNames, selectedName);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}