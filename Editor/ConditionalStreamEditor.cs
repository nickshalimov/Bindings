using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomEditor(typeof(ConditionalStream))]
    public class ConditionalStreamEditor: Editor
    {
        private ListProperty _conditions;
        private StreamsProperty _streams;

        private void OnEnable()
        {
            var stream = target as ConditionalStream;

            _streams = new StreamsProperty(stream.transform.parent, typeof(ValueStream));

            _conditions = new ListProperty(serializedObject.FindProperty("_conditions"));
            _conditions.DrawElement += OnDrawElement;
        }

        private void OnDisable()
        {
            _conditions.DrawElement -= OnDrawElement;
            _conditions.Destroy();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_name"));
            _conditions.DrawLayout();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnDrawElement(SerializedProperty element)
        {
            var stream = _streams.DrawLayout(element.FindPropertyRelative("_stream"), GUILayout.Width(EditorGUIUtility.labelWidth));

            var elementInverse = element.FindPropertyRelative("_inverse");
            var elementCondition = element.FindPropertyRelative("_condition");

            if (stream is BooleanStream)
            {
                var options = new[] { "True", "False" };
                var index = EditorGUILayout.Popup(elementInverse.boolValue ? 0 : 1, options, GUILayout.Width(80));
                elementInverse.boolValue = index == 0;
            }
            else if (stream is IntStream)
            {
                var options = new[] { "Equals", "Not Equals", "Greater", "Less" };
                var index = (elementCondition.enumValueIndex << 1) + (elementInverse.boolValue ? 1 : 0);

                var newIndex = EditorGUILayout.Popup(index, options, GUILayout.Width(80));
                if (index != newIndex)
                {
                    elementCondition.intValue = newIndex >> 1;
                    elementInverse.boolValue = newIndex % 2 == 1;
                }

                var elementValue = element.FindPropertyRelative("_int");
                EditorGUILayout.PropertyField(elementValue, GUIContent.none);
            }
            // TBD...
        }
    }
}
