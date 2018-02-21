using Bindings.Applicators;
using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomEditor(typeof(AnimatorApplicator))]
    public class AnimatorApplicatorEditor: Editor
    {
        private ListProperty _parameters;

        private AnimatorControllerParameter[] _animParameters;
        private string[] _animParameterNames;

        private StreamsProperty _streams;

        private void OnEnable()
        {
            var applicator = target as AnimatorApplicator;

            if (applicator == null)
            {
                return;
            }

            _animParameters = applicator.GetComponent<Animator>().parameters;
            _animParameterNames = System.Array.ConvertAll(_animParameters, p => string.Format("{0} ({1})", p.name, p.type));

            _streams = new StreamsProperty(applicator, typeof(Stream));

            _parameters = new ListProperty(serializedObject.FindProperty("_parameters"))
            {
                DrawElementLine = OnDrawElement
            };
        }

        private void OnDisable()
        {
            if (_parameters != null)
            {
                _parameters.Destroy();
                _parameters = null;
            }
        }

        public override void OnInspectorGUI()
        {
            if (_parameters != null)
            {
                serializedObject.Update();
                _parameters.DrawLayout();
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void OnDrawElement(int line, Rect position, SerializedProperty element)
        {
            var rect = new Rect(position) { width = 0.5f * (position.width - EditorGUIUtility.standardVerticalSpacing) };

            _streams.Draw(element.FindPropertyRelative("_stream"), rect);

            var itemParameter = element.FindPropertyRelative("_name");

            rect.x += rect.width + EditorGUIUtility.standardVerticalSpacing;

            int paramIndex = System.Array.FindIndex(_animParameters, p => p.name == itemParameter.stringValue);
            int newParamIndex = Mathf.Max(
                0,
                EditorGUI.Popup(
                    rect,
                    paramIndex,
                    _animParameterNames)
            );

            if (newParamIndex != paramIndex)
            {
                itemParameter.stringValue = _animParameters[newParamIndex].name;
            }
        }
    }
}
