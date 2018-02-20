using Bindings.Applicators;
using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomPropertyDrawer(typeof(PropertiesApplicator.PropertyApplicator), true)]
    public class PropertyApplicatorDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                var streams = new StreamsProperty(property.serializedObject.targetObject as Component, typeof(Stream), true);
                streams.Draw(property.FindPropertyRelative("_stream"), position);
            }
        }
    }
}
