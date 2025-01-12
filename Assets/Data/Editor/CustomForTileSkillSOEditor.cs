using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TileSkillSO))]
[CanEditMultipleObjects]
public class CustomForTileSkillSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TileSkillSO skill = (TileSkillSO)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Button"), new GUIContent("Button"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ManaCost"), new GUIContent("Mana Cost"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SkillIcon"), new GUIContent("Skill Icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AnotherTargets"), new GUIContent("Another Targets"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSpawnCount"), new GUIContent("Object SpawnCount"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Area"), new GUIContent("Area"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSpawnPos"), new GUIContent("Object Spawn Positions"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSprite"), new GUIContent("Object Sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectSpeed"), new GUIContent("Object Speed"));

        // Show/hide fields based on the value of AnotherTargets
        switch (skill.AnotherTargets)
        {
            case SkillTarget.SELF:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Buffs"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Effects"), true);
                break;

            case SkillTarget.OPPONENT:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Damage"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Debuffs"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("DeEffects"), true);
                break;

            case SkillTarget.SELFOPPONENT:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Damage"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Buffs"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Effects"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Debuffs"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("DeEffects"), true);
                break;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Description"), new GUIContent("Description"));

        // Apply property modifications
        serializedObject.ApplyModifiedProperties();
    }
}