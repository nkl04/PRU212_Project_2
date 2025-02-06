using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Pool))]
public class PoolEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty prefabProp = property.FindPropertyRelative("prefab");
        SerializedProperty sizeProp = property.FindPropertyRelative("size");
        SerializedProperty expandableProp = property.FindPropertyRelative("expandable");
        SerializedProperty hasMaxProp = property.FindPropertyRelative("hasMax");
        SerializedProperty maxProp = property.FindPropertyRelative("max");

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        Rect foldoutRect = new Rect(position.x, position.y, position.width, lineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;

            Rect prefabRect = new Rect(position.x, position.y + lineHeight + spacing, position.width, lineHeight);
            EditorGUI.PropertyField(prefabRect, prefabProp);

            Rect sizeRect = new Rect(position.x, prefabRect.y + lineHeight + spacing, position.width, lineHeight);
            EditorGUI.PropertyField(sizeRect, sizeProp);

            Rect expandableRect = new Rect(position.x, sizeRect.y + lineHeight + spacing, position.width, lineHeight);
            EditorGUI.PropertyField(expandableRect, expandableProp);

            if (expandableProp.boolValue)
            {
                Rect hasMaxRect = new Rect(position.x, expandableRect.y + lineHeight + spacing, position.width, lineHeight);
                EditorGUI.PropertyField(hasMaxRect, hasMaxProp);
                if (hasMaxProp.boolValue)
                {
                    Rect maxRect = new Rect(position.x, hasMaxRect.y + lineHeight + spacing, position.width, lineHeight);
                    EditorGUI.PropertyField(maxRect, maxProp);
                }
            }


            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;
        int lineCount = property.isExpanded ? (!property.FindPropertyRelative("expandable").boolValue ? 4 : !property.FindPropertyRelative("hasMax").boolValue ? 5 : 6) : 1;
        return lineHeight * lineCount + spacing * (lineCount - 1);
    }
}
