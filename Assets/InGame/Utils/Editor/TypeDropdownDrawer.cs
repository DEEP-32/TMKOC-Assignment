using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TMKOC.Utils.Editor {
    [CustomPropertyDrawer(typeof(TypeDropdownAttribute))]
    public class TypeDropdownDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            // Enforce that this attribute is only applied to string variables
            if (property.propertyType != SerializedPropertyType.String) 
            {
                EditorGUI.LabelField(position, label.text, "Use [TypeDropdown] with strings.");
                return;
            }

            TypeDropdownAttribute typeDropdownAttribute = (TypeDropdownAttribute)attribute;
            
            // Fast lookup of all concrete classes implementing the interface
            var typeCollection = TypeCache.GetTypesDerivedFrom(typeDropdownAttribute.BaseType)
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .ToList();

            List<string> typeNames = typeCollection.Select(t => t.Name).ToList();

            if (typeNames.Count == 0) 
            {
                EditorGUI.LabelField(position, label.text, "No valid types found");
                return;
            }

            // Match the currently saved string to the dropdown index
            int currentIndex = typeNames.IndexOf(property.stringValue);
            if (currentIndex < 0) currentIndex = 0; 

            // Draw the dropdown
            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, typeNames.ToArray());

            // Save the newly selected string back to the SerializedProperty
            property.stringValue = typeNames[newIndex];
        }
    }
}