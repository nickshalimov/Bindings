using Bindings.Expressions;
using UnityEngine;

namespace Bindings.Streams
{
    public class BooleanStream: ValueStream, IValueReader<bool>
    {
        [SerializeField] private bool _value;

        public bool GetValue()
        {
            return _value;
        }
        
        protected void UpdateValue(bool value)
        {
            if (_value != value)
            {
                _value = value;
                NotifyNext();
            }
        }
        
        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool EvaluateCondition(Condition condition, ValueStream stream)
        {
            var boolProperty = stream as IValueReader<bool>;
            if (boolProperty != null)
            {
                return condition == Condition.Equals && _value == boolProperty.GetValue();
            }

            return false;
        }

        private void OnValidate()
        {
            NotifyNext();
        }
    }
}
