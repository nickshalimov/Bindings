using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Bindings
{
    public class ListProperty
    {
        public delegate void DrawElementCallback(SerializedProperty element);
        public delegate void DrawElementLineCallback(int line, Rect position, SerializedProperty element);
        public delegate void ChangedCallback();

        private readonly ReorderableList _list;
        private float _elementWidth;
        
        public event DrawElementCallback DrawElement;
        public event DrawElementCallback DrawHeader;

        public DrawElementLineCallback DrawElementLine;
        public ChangedCallback Changed;

        protected SerializedProperty _property;
        private int _linesCount = 1;

        public SerializedProperty Property
        {
            get { return _property; }
        }

        public int LinesCount
        {
            get { return _linesCount; }
            set
            {
                _linesCount = value < 1 ? 1 : value;
                _list.elementHeight =
                    _linesCount * EditorGUIUtility.singleLineHeight +
                    EditorGUIUtility.standardVerticalSpacing * (_linesCount + 2);
            }
        }

        public float Height
        {
            get { return _list.GetHeight(); }
        }

        public ListProperty(SerializedProperty property)
        {
            _property = property;

            _list = new ReorderableList(
                property.serializedObject,
                property,
                true, false, true, true
            );

            _list.drawHeaderCallback = OnDrawHeader;
            _list.drawElementCallback = OnDrawElement;
            _list.onChangedCallback = OnChanged;
        }

        public void Destroy()
        {
            _list.drawHeaderCallback = null;
            _list.drawElementCallback = null;
            _list.onChangedCallback = null;
            DrawElementLine = null;
        }

        private void OnDrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _list.serializedProperty.displayName);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (DrawElementLine != null)
            {
                var property = _list.serializedProperty.GetArrayElementAtIndex(index);
                for (int i = 0; i < _linesCount; ++i)
                {
                    var pos = new Rect(rect)
                    {
                        y = rect.y + EditorGUIUtility.standardVerticalSpacing + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * i,
                        height = EditorGUIUtility.singleLineHeight
                    };

                    DrawElementLine(i, pos, property);
                }

                return;
            }

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

        private void OnChanged(ReorderableList list)
        {
            if (Changed != null)
            {
                Changed();
            }
        }

        public void Draw(Rect position)
        {
            //EditorGUILayout.Separator();
            _list.DoList(position);
            //EditorGUILayout.Separator();
        }

        public void DrawLayout()
        {
            EditorGUILayout.Separator();
            _list.DoLayoutList();
            EditorGUILayout.Separator();
        }
    }
}
