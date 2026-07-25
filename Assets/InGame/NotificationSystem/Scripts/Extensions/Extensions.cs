using System;

namespace NotificationSystem.Runtime.Extensions {
    public static class StringReflectionExtensions {
        
        /// <summary>
        /// Safely converts an AssemblyQualifiedName string into a C# Type.
        /// </summary>
        public static Type ToType(this string qualifiedName) {
            if (string.IsNullOrEmpty(qualifiedName)) return null;
            return Type.GetType(qualifiedName);
        }

        /// <summary>
        /// Extracts the short class name from an AssemblyQualifiedName string. 
        /// Useful for UI display.
        /// </summary>
        public static string ToShortClassName(this string qualifiedName, string fallback = "Unknown Type") {
            Type targetType = qualifiedName.ToType();
            return targetType != null ? targetType.Name : fallback;
        }
    }
}