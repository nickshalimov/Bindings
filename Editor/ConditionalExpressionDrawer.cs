using Bindings.Expressions;
using Bindings.Streams;
using UnityEditor;
using UnityEngine;

namespace Bindings
{
    [CustomPropertyDrawer(typeof(ConditionalExpression))]
    public class ConditionalExpressionDrawer: PropertyDrawer
    {
        private const float ConditionWidth = 70.0f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (IsEmpty(label) ? 1.0f : 2.0f) * EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var templateRect = new Rect { y = position.y, height = position.height };
                float ident = 0.0f;

                if (!IsEmpty(label))
                {
                    templateRect.y += EditorGUIUtility.singleLineHeight;
                    templateRect.height -= EditorGUIUtility.singleLineHeight;

                    var labelRect = new Rect(position) { height = EditorGUIUtility.singleLineHeight };
                    EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);

                    using (new EditorGUI.IndentLevelScope())
                    {
                        position = EditorGUI.IndentedRect(position);
                    }
                }

                using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel))
                {
                    var valueRect = new Rect(templateRect)
                    {
                        x = position.xMax - EditorGUIUtility.fieldWidth,
                        width = EditorGUIUtility.fieldWidth
                    };
    
                    var conditionRect = new Rect(templateRect)
                    {
                        x = valueRect.x - ConditionWidth - EditorGUIUtility.standardVerticalSpacing,
                        width = ConditionWidth
                    };
    
                    var streamRect = new Rect(templateRect)
                    {
                        x = position.x,
                        xMax = conditionRect.x - EditorGUIUtility.standardVerticalSpacing
                    };
    
                    var streams = new StreamsProperty(property.serializedObject.targetObject as Component, typeof(Stream));
                    var stream = streams.Draw(property.FindPropertyRelative("_stream"), streamRect);
    
                    var elementInverse = property.FindPropertyRelative("_inverse");
                    var elementCondition = property.FindPropertyRelative("_condition");
    
                    if (stream is BooleanStream)
                    {
                        var options = new[] { "True", "False" };
                        var index = EditorGUI.Popup(conditionRect, elementInverse.boolValue ? 0 : 1, options);
                        elementInverse.boolValue = index == 0;
                    }
                    else if (stream is IntStream)
                    {
                        var options = new[] { "Equals", "Not Equals", "Greater", "Less" };
                        var index = (elementCondition.enumValueIndex << 1) + (elementInverse.boolValue ? 1 : 0);
    
                        var newIndex = EditorGUI.Popup(conditionRect, index, options);
                        if (index != newIndex)
                        {
                            elementCondition.intValue = newIndex >> 1;
                            elementInverse.boolValue = newIndex % 2 == 1;
                        }
    
                        var elementValue = property.FindPropertyRelative("_int");
                        EditorGUI.PropertyField(valueRect, elementValue, GUIContent.none);
                    }
                    // TBD...

                }
            }
        }

        private static bool IsEmpty(GUIContent label)
        {
            return string.IsNullOrEmpty(label.text);
        }
    }
}
