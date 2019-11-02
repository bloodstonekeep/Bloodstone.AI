using UnityEditor;
using UnityEngine;

namespace Bloodstone.AI.Utility
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        private const bool DisabledScopeFlag = true;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (var disabledScope = new EditorGUI.DisabledGroupScope(DisabledScopeFlag))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }
}