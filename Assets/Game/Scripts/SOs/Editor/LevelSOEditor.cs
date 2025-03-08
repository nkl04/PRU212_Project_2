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

    private int selectedWaveIndex = -1;
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;

        backgroundProperty = serializedObject.FindProperty("background");
        durationProperty = serializedObject.FindProperty("durations");
        waveListProperty = serializedObject.FindProperty("waveList");

        waveReorderableList = new ReorderableList(serializedObject, waveListProperty, true, true, true, true)
        {
            drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Wave List");
                EditorGUI.LabelField(
                    new Rect(rect.x + rect.width * 0.6f, rect.y, rect.width * 0.35f, EditorGUIUtility.singleLineHeight),
                    new GUIContent($"Start Time")
                );
                EditorGUI.LabelField(
                    new Rect(rect.x + rect.width * 0.8f, rect.y, rect.width * 0.35f, EditorGUIUtility.singleLineHeight),
                    new GUIContent($"End Time")
                );
            },
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = waveListProperty.GetArrayElementAtIndex(index);
                SerializedProperty startTime = element.FindPropertyRelative("startTime");
                SerializedProperty endTime = element.FindPropertyRelative("endTime");
                SerializedProperty radiousSpawnCircle = element.FindPropertyRelative("radiousSpawnCircle");
                SerializedProperty offset = element.FindPropertyRelative("offset");
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
                    new Rect(rect.x + rect.width * 0.6f, rect.y, rect.width * 0.35f, lineHeight),
                    new GUIContent(Utilities.FormatTime(startTime.intValue))
                );

                EditorGUI.LabelField(
                    new Rect(rect.x + rect.width * 0.8f, rect.y, rect.width * 0.35f, lineHeight),
                    new GUIContent(spawnStyle.enumValueIndex == (int)SpawnStyle.Sequential ? Utilities.FormatTime(endTime.intValue) : "  -")
                );

                rect.y += lineHeight + space;

                if (foldoutStates[index])
                {
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

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), startTime, new GUIContent("Start Time"));
                    rect.y += lineHeight + space;

                    if (_spawnStyle == SpawnStyle.Sequential)
                    {
                        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), endTime, new GUIContent("End Time"));
                        rect.y += lineHeight + space;
                    }

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), radiousSpawnCircle, new GUIContent("Radius Spawn Circle"));
                    rect.y += lineHeight + space;

                    EditorGUI.indentLevel++;
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), offset, new GUIContent("Offset"));
                    rect.y += lineHeight + space;
                    EditorGUI.indentLevel--;

                    // Wave Enemy List Foldout
                    if (!waveEnemyListFoldoutStates.ContainsKey(index))
                        waveEnemyListFoldoutStates[index] = false;

                    waveEnemyListFoldoutStates[index] = EditorGUI.Foldout(new Rect(rect.x, rect.y, rect.width, lineHeight), waveEnemyListFoldoutStates[index], "Wave Enemy List", true);
                    rect.y += lineHeight + space;

                    if (waveEnemyListFoldoutStates[index])
                    {
                        waveEnemyLists[index] = CreateWaveEnemyList(waveEnemyList, (SpawnStyle)spawnStyle.enumValueIndex);
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
                    baseHeight += EditorGUIUtility.singleLineHeight * 7f;

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

        waveReorderableList.onSelectCallback = list =>
        {
            selectedWaveIndex = list.index;
            Debug.Log($"Selected Wave {selectedWaveIndex + 1}");
        };
    }

    private ReorderableList CreateWaveEnemyList(SerializedProperty waveEnemyList, SpawnStyle spawnStyle)
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

                if (enemyPrefab.objectReferenceValue != null && spawnStyle == SpawnStyle.Immediately)
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

                    if (spawnStyle == SpawnStyle.Immediately)
                    {
                        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, lineHeight), enemyAmount, new GUIContent("Enemy Amount"));
                        rect.y += lineHeight + space;
                    }
                }
                EditorGUI.indentLevel--;
            },
            elementHeightCallback = index =>
            {
                float baseHeight = EditorGUIUtility.singleLineHeight + 2f;
                if (waveEnemyFoldoutStates.ContainsKey(index) && waveEnemyFoldoutStates[index])
                {
                    baseHeight += EditorGUIUtility.singleLineHeight;
                    if (spawnStyle == SpawnStyle.Immediately)
                    {
                        baseHeight += EditorGUIUtility.singleLineHeight;
                    }
                }
                return baseHeight;
            }
        };
    }
    private void OnSceneGUI(SceneView sceneView)
    {
        if (selectedWaveIndex >= 0 && selectedWaveIndex < waveListProperty.arraySize)
        {
            var selectedWave = waveListProperty.GetArrayElementAtIndex(selectedWaveIndex);
            var radiusProperty = selectedWave.FindPropertyRelative("radiousSpawnCircle");
            var offsetProperty = selectedWave.FindPropertyRelative("offset");

            float radius = radiusProperty.floatValue;
            float offset = offsetProperty.floatValue;
            Vector3 position = Vector3.zero;

            Handles.color = Color.green;
            Handles.DrawWireDisc(position, Vector3.forward, radius);

            // Vẽ đường zic-zac xung quanh hình tròn
            DrawZigZagCircle(position, radius, offset, 72);

            SceneView.RepaintAll();
        }
    }
    private void DrawZigZagCircle(Vector3 center, float radius, float offset, int segments)
    {
        Vector3[] points = new Vector3[segments * 2];

        for (int i = 0; i < segments; i++)
        {
            float angle = (i / (float)segments) * Mathf.PI * 2;
            float nextAngle = ((i + 1) / (float)segments) * Mathf.PI * 2;

            // Xen kẽ giữa +offset và -offset
            float dynamicRadius = radius + (i % 2 == 0 ? offset : -offset);

            Vector3 point = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * dynamicRadius;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(nextAngle), Mathf.Sin(nextAngle), 0) * dynamicRadius;

            points[i * 2] = point;
            points[i * 2 + 1] = nextPoint;
        }

        Handles.color = Color.red;
        Handles.DrawAAPolyLine(3, points);
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

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}
