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

        private Stream[] _streams;
        private string[] _streamNames;

        private void OnEnable()
        {
            var applicator = target as AnimatorApplicator;

            _animParameters = applicator.GetComponent<Animator>().parameters;
            _animParameterNames = System.Array.ConvertAll(_animParameters, p => string.Format("{0} ({1})", p.name, p.type));

            _streams = applicator.GetComponentsInParent<Stream>();
            _streamNames = System.Array.ConvertAll(_streams, s => string.Format("{0} :: {1} ({2})", s.name, s.Name, ObjectNames.NicifyVariableName(s.GetType().Name)));
            
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
            var itemStream = element.FindPropertyRelative("_stream");

            int streamIndex = System.Array.FindIndex(_streams, s => s == itemStream.objectReferenceValue);
            int newStreamIndex = Mathf.Max(0, EditorGUILayout.Popup(streamIndex, _streamNames));

            if (newStreamIndex != streamIndex)
            {
                itemStream.objectReferenceValue = _streams[newStreamIndex];
            }

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
