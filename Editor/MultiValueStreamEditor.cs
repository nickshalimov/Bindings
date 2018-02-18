using System.Reflection;
using Bindings.Streams;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Bindings
{
    [CustomEditor(typeof(MultiValueStream))]
    public class MultiValueStreamEditor: Editor
    {
        private ListProperty _streamsProperty;
        private ValueStream[] _streams;
        private string[] _streamNames;

        private void OnEnable()
        {
            _streamsProperty = null;

            var binding = target as MultiValueStream;
            if (binding == null)
            {
                return;
            }

            _streams = binding.transform.parent.GetComponentsInParent<ValueStream>();
            _streamNames = System.Array.ConvertAll(_streams, s => s.name + " :: " + s.Name);

            _streamsProperty = new ListProperty(serializedObject.FindProperty("_valueStreams"));
            _streamsProperty.DrawElement += OnDrawElement;
        }

        private void OnDisable()
        {
            if (_streamsProperty != null)
            {
                _streamsProperty.DrawElement -= OnDrawElement;
                _streamsProperty.Destroy();
            }
        }

        public override void OnInspectorGUI()
        {
            if (_streamsProperty != null)
            {
                serializedObject.Update();
                _streamsProperty.DrawLayout();
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUILayout.HelpBox("Can't find any ValueStream", MessageType.Warning);
            }
        }

        private void OnDrawElement(SerializedProperty element)
        {
            int streamIndex = System.Array.FindIndex(_streams, s => s == element.objectReferenceValue);
            int newStreamIndex = EditorGUILayout.Popup(streamIndex, _streamNames);
            if (newStreamIndex != streamIndex)
            {
                element.objectReferenceValue = newStreamIndex >= 0 ? _streams[newStreamIndex] : null;
            }
        }
    }
}
