using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(LevelSO))]
public class LevelSOEditor : Editor
{
    private SerializedProperty backgroundProperty;
    private SerializedProperty waveListProperty;
    private SerializedProperty timeBetweenWavesListProperty;

    private ReorderableList reorderableList;
    private ReorderableList timeBetweenWavesReorderableList;

    private void OnEnable()
    {
        backgroundProperty = serializedObject.FindProperty("background");
        waveListProperty = serializedObject.FindProperty("waveList");
        timeBetweenWavesListProperty = serializedObject.FindProperty("timeBetweenWaveList");

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

        timeBetweenWavesReorderableList = new ReorderableList(serializedObject, timeBetweenWavesListProperty, true, true, true, true)
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Time Between Stages"),
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                SerializedProperty element = timeBetweenWavesListProperty.GetArrayElementAtIndex(index);
                string label = $"Stage {index} to {index + 1}";

                rect.y += 2f;
                float lineHeight = EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), element, new GUIContent(label));
            },
            elementHeightCallback = index =>
            {
                return EditorGUIUtility.singleLineHeight + 2f;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        reorderableList.DoLayoutList();
        timeBetweenWavesReorderableList.DoLayoutList();

        EditorGUILayout.PropertyField(backgroundProperty);
        serializedObject.ApplyModifiedProperties();
    }

}