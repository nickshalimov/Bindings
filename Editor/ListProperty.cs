using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Bindings
{
    public class ListProperty
    {
        public delegate void DrawElementCallback(SerializedProperty element);

        private readonly ReorderableList _list;
        private float _elementWidth;
        
        public event DrawElementCallback DrawElement;

        public float ElementHeight
        {
            get { return _list.elementHeight; }
            set { _list.elementHeight = value; }
        }

        public ListProperty(SerializedProperty property)
        {
            _list = new ReorderableList(
                property.serializedObject,
                property,
                true, false, true, true
            );

            _list.drawHeaderCallback = OnDrawHeader;
            _list.drawElementCallback = OnDrawElement;
        }

        public void Destroy()
        {
            _list.drawHeaderCallback = null;
            _list.drawElementCallback = null;
        }

        private void OnDrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _list.serializedProperty.displayName);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var offset = Vector2.one * 2.0f;
            var position = new Rect(offset, rect.size);

            // UGLY but works
            if (position.width < 0.0f)
            {
                position.width = _elementWidth;
            }
            else
            {
                _elementWidth = position.width;
            }
            
            using (new GUI.GroupScope(rect))
            using (new GUILayout.AreaScope(position))
            using (new GUILayout.HorizontalScope())
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);
                if (DrawElement != null)
                {
                    DrawElement(element);
                }

                GUILayout.Space(3.0f);
            }
        }

        public void DrawLayout()
        {
            EditorGUILayout.Separator();
            _list.DoLayoutList();
            EditorGUILayout.Separator();
        }
    }
}
