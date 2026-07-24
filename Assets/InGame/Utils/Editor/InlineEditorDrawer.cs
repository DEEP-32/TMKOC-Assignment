using UnityEditor;
using UnityEngine;
using TMKOC.Utils;

namespace TMKOC.Utils.Editor {
    [CustomPropertyDrawer(typeof(InlineEditorAttribute))]
    public class InlineEditorDrawer : PropertyDrawer {
        // Calculates how much vertical space the expanded object needs
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            float totalHeight = EditorGUIUtility.singleLineHeight;

            if (property.objectReferenceValue != null && property.isExpanded) {
                SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);
                SerializedProperty prop = serializedObject.GetIterator();

                if (prop.NextVisible(true)) {
                    do {
                        if (prop.name == "m_Script") continue; // Hide the script reference field
                        totalHeight += EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
                    } while (prop.NextVisible(false));
                }

                totalHeight += EditorGUIUtility.standardVerticalSpacing * 2; // Bottom padding
            }

            return totalHeight;
        }

        // Draws the actual GUI
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            // Draw the main object field
            Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            // Draw a foldout arrow if an object is assigned
            if (property.objectReferenceValue != null) {
                property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, 10, EditorGUIUtility.singleLineHeight), property.isExpanded, GUIContent.none);
            }

            EditorGUI.PropertyField(fieldRect, property, label);

            // If expanded, draw all the properties of the ScriptableObject!
            if (property.objectReferenceValue != null && property.isExpanded) {
                EditorGUI.indentLevel++; // Indent it so it looks like a child

                SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);
                serializedObject.Update();

                SerializedProperty prop = serializedObject.GetIterator();
                float currentY = fieldRect.yMax + EditorGUIUtility.standardVerticalSpacing;

                if (prop.NextVisible(true)) {
                    do {
                        if (prop.name == "m_Script") continue; // Hide the script reference field

                        float height = EditorGUI.GetPropertyHeight(prop, true);
                        Rect propRect = new Rect(position.x, currentY, position.width, height);
                        EditorGUI.PropertyField(propRect, prop, true);
                        currentY += height + EditorGUIUtility.standardVerticalSpacing;
                    } while (prop.NextVisible(false));
                }

                serializedObject.ApplyModifiedProperties();
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }
    }
}