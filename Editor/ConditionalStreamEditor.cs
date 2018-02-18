using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomEditor(typeof(ConditionalStream))]
    public class ConditionalStreamEditor: Editor
    {
        private ListProperty _conditions;
        private StreamsProperty<ValueStream> _streams;

        private void OnEnable()
        {
            var stream = target as ConditionalStream;

            _streams = new StreamsProperty<ValueStream>(stream.transform.parent);

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
            _conditions.DrawLayout();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnDrawElement(SerializedProperty element)
        {
            var stream = _streams.DrawLayout(element.FindPropertyRelative("_stream"));

            var elementInverse = element.FindPropertyRelative("_inverse");

            if (stream is BooleanStream)
            {
                var options = new[] { "True", "False" };
                var index = EditorGUILayout.Popup(elementInverse.boolValue ? 0 : 1, options, GUILayout.Width(60));
                elementInverse.boolValue = index == 0;
            }
            // TBD...
        }
    }
}
