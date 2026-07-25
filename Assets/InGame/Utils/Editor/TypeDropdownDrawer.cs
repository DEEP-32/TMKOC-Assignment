using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TMKOC.Utils.Editor {
    [CustomPropertyDrawer(typeof(TypeDropdownAttribute))]
    public class TypeDropdownDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.propertyType != SerializedPropertyType.String) {
                EditorGUI.LabelField(position, label.text, "Use [TypeDropdown] with strings.");
                return;
            }

            TypeDropdownAttribute typeDropdownAttribute = (TypeDropdownAttribute)attribute;

            var typeCollection = TypeCache.GetTypesDerivedFrom(typeDropdownAttribute.BaseType)
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .ToList();

            if (typeCollection.Count == 0) {
                EditorGUI.LabelField(position, label.text, "No valid types found");
                return;
            }

            // 1. Array of SHORT names for the Inspector UI ("SuperShooter")
            string[] displayNames = typeCollection.Select(t => t.Name).ToArray();

            // 2. List of ASSEMBLY QUALIFIED names to actually save
            // This guarantees Type.GetType() works across any .asmdef boundary
            List<string> qualifiedNames = typeCollection.Select(t => t.AssemblyQualifiedName).ToList();

            // Find the index of the currently saved qualified name
            int currentIndex = qualifiedNames.IndexOf(property.stringValue);
            if (currentIndex < 0) currentIndex = 0;

            // Draw the dropdown using the SHORT names
            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, displayNames);

            // 3. Save the ASSEMBLY QUALIFIED name back to the string property
            property.stringValue = qualifiedNames[newIndex];
        }
    }
}