using Bindings.Properties;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomPropertyDrawer(typeof(ConditionalProperty))]
    public class ConditionalPropertyDrawer: PropertyDrawer
    {
        private const float AllAnyWidth = 60.0f;

        private ListProperty _expressionsProperty;
        private SerializedProperty _anyExpressionProperty;

        private void ValidateStreams(SerializedProperty property)
        {
            if (_expressionsProperty == null)
            {
                _anyExpressionProperty = property.FindPropertyRelative("_any");

                _expressionsProperty = new ListProperty(property.FindPropertyRelative("_expressions"))
                {
                    DisplayName = property.displayName,
                    DrawHeader = OnDrawHeader,
                    DrawElementLine = OnDrawElement
                };
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ValidateStreams(property);
            return _expressionsProperty.Height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ValidateStreams(property);
            _expressionsProperty.Draw(position);
        }

        private void OnDrawHeader(Rect position)
        {
            position.x = position.xMax - AllAnyWidth;
            position.width = AllAnyWidth;

            string[] options = { "All", "Any" };

            int index = _anyExpressionProperty.boolValue ? 1 : 0;
            if (index != EditorGUI.Popup(position, index, options))
            {
                _anyExpressionProperty.boolValue = !_anyExpressionProperty.boolValue;
            }
        }

        private static void OnDrawElement(int line, Rect position, SerializedProperty element)
        {
            EditorGUI.PropertyField(position, element, GUIContent.none);
        }
    }
}
