using System;
using UnityEngine;

namespace TMKOC.Utils{
    /// <summary>
    /// Place this attribute on a string field to render a dropdown of classes implementing the specified base type.
    /// </summary>
    public class TypeDropdownAttribute : PropertyAttribute 
    {
        public Type BaseType { get; }

        public TypeDropdownAttribute(Type baseType) 
        {
            BaseType = baseType;
        }
    }
}