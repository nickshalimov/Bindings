using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Bindings
{
    public class ListProperty
    {
        public delegate void DrawElementCallback(Rect position);
        public delegate void DrawElementLineCallback(int line, Rect position, SerializedProperty element);
        public delegate void ChangedCallback();

        private readonly ReorderableList _list;
        private float _elementWidth;

        public DrawElementCallback DrawHeader;

        public DrawElementLineCallback DrawElementLine;
        public ChangedCallback Changed;

        private SerializedProperty _property;
        private int _linesCount = 1;

        public SerializedProperty Property
        {
            get { return _property; }
        }

        public string DisplayName { get; set; }

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

            DisplayName = _list.serializedProperty.displayName;

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
            //EditorGUI.LabelField(rect, DisplayName);
            rect = EditorGUI.PrefixLabel(rect, new GUIContent(DisplayName));

            if (DrawHeader != null)
            {
                DrawHeader(rect);
            }
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (DrawElementLine == null)
            {
                return;
            }

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
            _list.DoList(position);
        }

        public void DrawLayout()
        {
            EditorGUILayout.Separator();
            _list.DoLayoutList();
            EditorGUILayout.Separator();
        }
    }
}
