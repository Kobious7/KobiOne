using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SelfSkillSO))]
[CanEditMultipleObjects]
public class CustomForSelfSkillSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SelfSkillSO skill = (SelfSkillSO)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Button"), new GUIContent("Button"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ManaCost"), new GUIContent("Mana Cost"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SkillIcon"), new GUIContent("Skill Icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AnotherTargets"), new GUIContent("Another Targets"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Buffs"), new GUIContent("Buffs"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Effects"), new GUIContent("Effects"));

        // Show/hide fields based on the value of AnotherTargets
        switch (skill.AnotherTargets)
        {
            case SkillTarget.OPPONENT:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Debuffs"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("DeEffects"), true);
                break;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"), new GUIContent("Description"));

        // Apply property modifications
        serializedObject.ApplyModifiedProperties();
    }
}