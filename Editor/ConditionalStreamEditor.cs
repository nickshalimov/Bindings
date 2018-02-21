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

            _conditions = new ListProperty(serializedObject.FindProperty("_conditions"))
            {
                DrawElementLine = OnDrawElement
            };
        }

        private void OnDisable()
        {
            _conditions.Destroy();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_name"));
            _conditions.DrawLayout();
            serializedObject.ApplyModifiedProperties();
        }

        private static void OnDrawElement(int line, Rect rect, SerializedProperty element)
        {
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        }
    }
}
