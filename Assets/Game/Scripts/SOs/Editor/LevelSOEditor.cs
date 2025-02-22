using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ConfigLevel))]
public class LevelSOEditor : Editor
{
    private SerializedProperty backgroundProperty;
    private SerializedProperty waveListProperty;

    private ReorderableList reorderableList;

    private void OnEnable()
    {
        backgroundProperty = serializedObject.FindProperty("background");
        waveListProperty = serializedObject.FindProperty("waveList");

        reorderableList = new ReorderableList(serializedObject, waveListProperty, true, true, true, true)
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Wave List"),
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                string labelString = "Wave " + index;
                var element = waveListProperty.GetArrayElementAtIndex(index);
                SerializedProperty enemyPrefab = element.FindPropertyRelative("enemyPrefab");
                SerializedProperty enemyAmount = element.FindPropertyRelative("enemyAmount");

                float space = 2f;
                rect.y += space;
                float lineHeight = EditorGUIUtility.singleLineHeight;

                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, lineHeight), labelString);
                rect.y += lineHeight + space;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), enemyPrefab, new GUIContent("Enemy Prefab"));

                rect.y += lineHeight + space;
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), enemyAmount, new GUIContent("Enemy Amount"));
            },
            elementHeightCallback = index =>
            {
                return EditorGUIUtility.singleLineHeight * 3 + 6f;
            }
        };


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        reorderableList.DoLayoutList();

        EditorGUILayout.PropertyField(backgroundProperty);
        serializedObject.ApplyModifiedProperties();
    }

}