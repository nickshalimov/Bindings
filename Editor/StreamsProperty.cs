using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    public struct StreamsProperty<T> where T: Stream
    {
        private readonly T[] _streams;
        private readonly  GUIContent[] _names;

        public StreamsProperty(Component origin)
        {
            _streams = origin.GetComponentsInParent<T>();
            _names = System.Array.ConvertAll(
                _streams,
                s => new GUIContent(string.Format("{0} :: {1}", s.name, s.Name))
            );
        }

        public T DrawLayout(SerializedProperty property)
        {
            var streamIndex = System.Array.FindIndex(_streams, s => s == property.objectReferenceValue);
            var newStreamIndex = EditorGUILayout.Popup(streamIndex, _names);

            var stream = newStreamIndex >= 0 ? _streams[newStreamIndex] : null;
            if (newStreamIndex != streamIndex)
            {
                property.objectReferenceValue = stream;
            }

            return stream;
        }
    }
}
