using System.Collections;
using System.Collections.Generic;
using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    public class StreamsProperty
    {
        private readonly Component[] _streams;
        private readonly GUIContent[] _names;

        public StreamsProperty(Component origin, System.Type streamType, bool includeDefault = false)
        {
            var components = origin.GetComponentsInParent(streamType, true);

            var streams = new List<Component>(components);
            var names = streams.ConvertAll(
                c => new GUIContent(string.Format("{0} [{1}]", c is Stream ? (c as Stream).Id : "Null", c == null ? "" : c.name))
            );

            if (includeDefault)
            {
                streams.Insert(0, null);
                names.Insert(0, new GUIContent("None"));
            }

            _streams = streams.ToArray();
            _names = names.ToArray();
        }

        public Stream Draw(SerializedProperty property, Rect position)
        {
            var streamIndex = System.Array.FindIndex(_streams, s => s == property.objectReferenceValue);
            var newStreamIndex = EditorGUI.Popup(position, streamIndex, _names);

            var stream = newStreamIndex >= 0 ? _streams[newStreamIndex] : null;
            if (newStreamIndex != streamIndex)
            {
                property.objectReferenceValue = stream;
            }

            return stream as Stream;
        }

        public Stream DrawLayout(SerializedProperty property, params GUILayoutOption[] options)
        {
            var streamIndex = System.Array.FindIndex(_streams, s => s == property.objectReferenceValue);
            var newStreamIndex = EditorGUILayout.Popup(streamIndex, _names, options);

            var stream = newStreamIndex >= 0 ? _streams[newStreamIndex] : null;
            if (newStreamIndex != streamIndex)
            {
                property.objectReferenceValue = stream;
            }

            return stream as Stream;
        }
    }
}
