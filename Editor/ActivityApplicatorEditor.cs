using Bindings.Applicators;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomEditor(typeof(ActivityApplicator))]
    public class ActivityApplicatorEditor: Editor
    {
        private ListProperty _entries;
        
        private void OnEnable()
        {
            var applicator = target as ActivityApplicator;
            if (applicator == null)
            {
                return;
            }

            _entries = new ListProperty(serializedObject.FindProperty("_entries"))
            {
                LinesCount = 2,
                DrawElementLine = OnDrawElementLine
            };
        }

        private void OnDisable()
        {
            if (_entries != null)
            {
                _entries.Destroy();
                _entries = null;
            }
        }

        public override void OnInspectorGUI()
        {
            if (_entries != null)
            {
                serializedObject.Update();
                _entries.DrawLayout();
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void OnDrawElementLine(int line, Rect position, SerializedProperty element)
        {
            if (line == 0)
            {
                EditorGUI.PropertyField(
                    position,
                    element.FindPropertyRelative("_expression"),
                    GUIContent.none
                );
            }
            else if (line == 1)
            {
                EditorGUI.PropertyField(
                    position,
                    element.FindPropertyRelative("_target")
                );
            }
        }
    }
}
