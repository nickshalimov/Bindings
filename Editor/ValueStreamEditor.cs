using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomEditor(typeof(ValueStream))]
    public class ValueStreamEditor: Editor
    {
        public sealed override void OnInspectorGUI()
        {
            serializedObject.Update();
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_name"), GUIContent.none, GUILayout.Width(EditorGUIUtility.labelWidth));
                DrawValue();
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawValue()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_value"), new GUIContent(" "));
        }
    }
}
