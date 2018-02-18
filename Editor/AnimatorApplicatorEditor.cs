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

        private StreamsProperty<Stream> _streams;

        private void OnEnable()
        {
            var applicator = target as AnimatorApplicator;

            _animParameters = applicator.GetComponent<Animator>().parameters;
            _animParameterNames = System.Array.ConvertAll(_animParameters, p => string.Format("{0} ({1})", p.name, p.type));

            _streams = new StreamsProperty<Stream>(applicator);

            _parameters = new ListProperty(serializedObject.FindProperty("_parameters"));
            _parameters.DrawElement += OnDrawElement;
        }

        private void OnDisable()
        {
            _parameters.DrawElement -= OnDrawElement;
            _parameters.Destroy();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _parameters.DrawLayout();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnDrawElement(SerializedProperty element)
        {
            _streams.DrawLayout(element.FindPropertyRelative("_stream"));

            var itemParameter = element.FindPropertyRelative("_name");

            int paramIndex = System.Array.FindIndex(_animParameterNames, p => p == itemParameter.stringValue);
            int newParamIndex = Mathf.Max(0, EditorGUILayout.Popup(paramIndex, _animParameterNames));

            if (newParamIndex != paramIndex)
            {
                itemParameter.stringValue = _animParameterNames[newParamIndex];
            }
        }
    }
}
