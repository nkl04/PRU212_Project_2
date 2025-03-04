using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Splines.SplineInstantiate;

[CustomEditor(typeof(ConfigLevel))]
[CanEditMultipleObjects]
public class LevelSOEditor : Editor
{
    private SerializedProperty backgroundProperty;
    private SerializedProperty durationProperty;
    private SerializedProperty waveListProperty;

    private ReorderableList waveReorderableList;
    private Dictionary<int, ReorderableList> waveEnemyLists = new Dictionary<int, ReorderableList>();
    private Dictionary<int, bool> foldoutStates = new Dictionary<int, bool>();
    private Dictionary<int, bool> waveEnemyListFoldoutStates = new Dictionary<int, bool>();
    private Dictionary<int, bool> waveEnemyFoldoutStates = new Dictionary<int, bool>();

    private void OnEnable()
    {
        backgroundProperty = serializedObject.FindProperty("background");
        durationProperty = serializedObject.FindProperty("durations");
        waveListProperty = serializedObject.FindProperty("waveList");

        waveReorderableList = new ReorderableList(serializedObject, waveListProperty, true, true, true, true)
        {
            drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Wave List");
                EditorGUI.LabelField(
                    new Rect(rect.x + rect.width * 0.65f, rect.y, rect.width * 0.35f, EditorGUIUtility.singleLineHeight),
                    new GUIContent($"Start Time")
                );
            },
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = waveListProperty.GetArrayElementAtIndex(index);
                SerializedProperty startTime = element.FindPropertyRelative("startTime");
                SerializedProperty radiousSpawnCircle = element.FindPropertyRelative("radiousSpawnCircle");
                SerializedProperty spawnStyle = element.FindPropertyRelative("spawnStyle");
                SerializedProperty timeBetweenSpawns = element.FindPropertyRelative("timeBetweenSpawns");
                SerializedProperty waveEnemyList = element.FindPropertyRelative("waveEnemyList");

                float space = EditorGUIUtility.standardVerticalSpacing;
                float lineHeight = EditorGUIUtility.singleLineHeight;
                rect.y += space;

                // Toggle foldout state
                if (!foldoutStates.ContainsKey(index))
                    foldoutStates[index] = false;

                EditorGUI.indentLevel++;
                foldoutStates[index] = EditorGUI.Foldout(new Rect(rect.x, rect.y, rect.width, lineHeight), foldoutStates[index], $"Wave {index + 1}", true);

                EditorGUI.LabelField(
                    new Rect(rect.x + rect.width * 0.65f, rect.y, rect.width * 0.35f, lineHeight),
                    new GUIContent(Utilities.FormatTime(startTime.intValue))
                );

                rect.y += lineHeight + space;

                if (foldoutStates[index])
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), startTime, new GUIContent("Start Time"));
                    rect.y += lineHeight + space;

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), radiousSpawnCircle, new GUIContent("Radius Spawn Circle"));
                    rect.y += lineHeight + space;

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), spawnStyle, new GUIContent("Spawn Style"));
                    rect.y += lineHeight + space;

                    SpawnStyle _spawnStyle = (SpawnStyle)spawnStyle.enumValueIndex;
                    if (_spawnStyle == SpawnStyle.Sequential)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), timeBetweenSpawns, new GUIContent("Time Between Spawns"));
                        rect.y += lineHeight + space;
                        EditorGUI.indentLevel--;
                    }

                    // Wave Enemy List Foldout
                    if (!waveEnemyListFoldoutStates.ContainsKey(index))
                        waveEnemyListFoldoutStates[index] = false;

                    waveEnemyListFoldoutStates[index] = EditorGUI.Foldout(new Rect(rect.x, rect.y, rect.width, lineHeight), waveEnemyListFoldoutStates[index], "Wave Enemy List", true);
                    rect.y += lineHeight + space;

                    if (waveEnemyListFoldoutStates[index])
                    {
                        if (!waveEnemyLists.ContainsKey(index))
                        {
                            waveEnemyLists[index] = CreateWaveEnemyList(waveEnemyList);
                        }

                        waveEnemyLists[index].DoList(new Rect(rect.x, rect.y, rect.width, waveEnemyLists[index].GetHeight()));
                    }

                }
                EditorGUI.indentLevel--;
            },
            elementHeightCallback = index =>
            {
                float baseHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                if (foldoutStates.ContainsKey(index) && foldoutStates[index])
                {
                    baseHeight += EditorGUIUtility.singleLineHeight * 5f;

                    var element = waveListProperty.GetArrayElementAtIndex(index);
                    SerializedProperty spawnStyle = element.FindPropertyRelative("spawnStyle");

                    if ((SpawnStyle)spawnStyle.enumValueIndex == SpawnStyle.Sequential)
                    {
                        baseHeight += EditorGUIUtility.singleLineHeight;
                    }

                    if (waveEnemyListFoldoutStates.ContainsKey(index) && waveEnemyListFoldoutStates[index] && waveEnemyLists.ContainsKey(index))
                    {
                        baseHeight += waveEnemyLists[index].GetHeight();
                    }
                }

                return baseHeight;
            }
        };
    }

    private ReorderableList CreateWaveEnemyList(SerializedProperty waveEnemyList)
    {
        return new ReorderableList(serializedObject, waveEnemyList, true, true, true, true)
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Wave Enemy List"),
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var waveEnemyElement = waveEnemyList.GetArrayElementAtIndex(index);
                SerializedProperty enemyPrefab = waveEnemyElement.FindPropertyRelative("enemyPrefab");
                SerializedProperty enemyAmount = waveEnemyElement.FindPropertyRelative("enemyAmount");

                float space = EditorGUIUtility.standardVerticalSpacing;
                float lineHeight = EditorGUIUtility.singleLineHeight;
                rect.y += space;

                if (!waveEnemyFoldoutStates.ContainsKey(index))
                {
                    waveEnemyFoldoutStates[index] = false;
                }
                EditorGUI.indentLevel++;
                waveEnemyFoldoutStates[index] = EditorGUI.Foldout(
                      new Rect(rect.x, rect.y, rect.width, lineHeight),
                      waveEnemyFoldoutStates[index],
                      enemyPrefab.objectReferenceValue != null ? enemyPrefab.objectReferenceValue.name : "Null",
                      true
                  );

                if (enemyPrefab.objectReferenceValue != null)
                {
                    EditorGUI.LabelField(
                        new Rect(rect.x + rect.width * 0.65f, rect.y, rect.width * 0.35f, lineHeight),
                        new GUIContent("Qty: " + enemyAmount.intValue)
                    );
                }

                rect.y += lineHeight + space;

                if (waveEnemyFoldoutStates[index])
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), enemyPrefab, new GUIContent("Enemy Prefab"));
                    rect.y += lineHeight + space;

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), enemyAmount, new GUIContent("Enemy Amount"));
                }
                EditorGUI.indentLevel--;
            },
            elementHeightCallback = index =>
            {
                float baseHeight = EditorGUIUtility.singleLineHeight + 2f;
                if (waveEnemyFoldoutStates.ContainsKey(index) && waveEnemyFoldoutStates[index])
                {
                    baseHeight += EditorGUIUtility.singleLineHeight * 3;
                }
                return baseHeight;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(backgroundProperty);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(durationProperty, new GUIContent("Duration"));
        GUILayout.Space(50);
        EditorGUILayout.LabelField(Utilities.FormatTime(durationProperty.intValue), GUILayout.Width(80));
        EditorGUILayout.EndHorizontal();

        waveReorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}
