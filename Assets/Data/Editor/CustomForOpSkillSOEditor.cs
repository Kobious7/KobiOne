using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OpSkillSO))]
[CanEditMultipleObjects]
public class CustomForOpSkillSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        OpSkillSO skill = (OpSkillSO)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Button"), new GUIContent("Button"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ManaCost"), new GUIContent("Mana Cost"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SkillIcon"), new GUIContent("Skill Icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AnotherTargets"), new GUIContent("Another Targets"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Damage"), new GUIContent("Damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSpawnCount"), new GUIContent("Object SpawnCount"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSpawnPos"), new GUIContent("Object Spawn Positions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSprite"), new GUIContent("Object Sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSpeed"), new GUIContent("Object Speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Debuffs"), new GUIContent("DeBuffs"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("DeEffects"), new GUIContent("DeEffects"));

        // Show/hide fields based on the value of AnotherTargets
        switch (skill.AnotherTargets)
        {
            case SkillTarget.SELF:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Buffs"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Effects"), true);
                break;
        }


        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"), new GUIContent("Description"));

        // Apply property modifications
        serializedObject.ApplyModifiedProperties();
    }
}