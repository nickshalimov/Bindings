using Bindings.Streams;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Bindings
{
    [CustomPropertyDrawer(typeof(TupleStream))]
    public class TupleStreamDrawer: PropertyDrawer
    {
        private SerializedProperty _property;
        private ListProperty _streamsProperty;
        private StreamsProperty _streams;

        private void ValidateStreams(SerializedProperty property)
        {
            if (_streams == null || _streamsProperty == null)// || _property != property)
            {
                _streams = new StreamsProperty(property.serializedObject.targetObject as Component, typeof(ValueStream));

                _streamsProperty = new ListProperty(property.FindPropertyRelative("_items"))
                {
                    DrawElementLine = OnDrawElement,
                    Changed = OnChanged
                };
            }

            _property = property;
        }

        private void OnChanged()
        {
            _streamsProperty = null;
            ValidateStreams(_property);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ValidateStreams(property);
            return _streamsProperty.Height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ValidateStreams(property);
            _streamsProperty.Draw(position);
        }

        private void OnDrawElement(int line, Rect position, SerializedProperty element)
        {
            _streams.Draw(element, position);
        }
    }
}
