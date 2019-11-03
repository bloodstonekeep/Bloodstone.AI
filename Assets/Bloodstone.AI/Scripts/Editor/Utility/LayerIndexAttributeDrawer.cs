using UnityEditor;
using UnityEngine;

namespace Bloodstone.AI.Utility
{
    [CustomPropertyDrawer(typeof(LayerIndexAttribute))]
    public class LayerIndexAttributeDrawer : PropertyDrawer
    {
        public const int MinIndex = 0;
        public const int MaxIndex = 31;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (var propertyScope = new EditorGUI.PropertyScope(position, label, property))
            {
                var index = Mathf.Clamp(property.intValue, MinIndex, MaxIndex);

                property.intValue = EditorGUI.LayerField(position, label, index);
            }
        }
    }
}