using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomEditor(typeof(ConditionalStream))]
    public class ConditionalStreamEditor: Editor
    {
        private ListProperty _conditions;
        private ValueStream[] _streams;
        private string[] _streamNames;

        private void OnEnable()
        {
            var stream = target as ConditionalStream;

            _streams = stream.GetComponentsInParent<ValueStream>();
            _streamNames = System.Array.ConvertAll(_streams, s => string.Format("{0} :: {1} ({2})", s.name, s.Name, ObjectNames.NicifyVariableName(s.GetType().Name)));

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
            var elementStream = element.FindPropertyRelative("_stream");

            int streamIndex = System.Array.FindIndex(_streams, s => s == elementStream.objectReferenceValue);
            int newStreamIndex = Mathf.Max(
                0,
                EditorGUILayout.Popup(streamIndex, _streamNames, GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.5f))
            );

            var stream = _streams[newStreamIndex];
            if (newStreamIndex != streamIndex)
            {
                elementStream.objectReferenceValue = stream;
            }

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
