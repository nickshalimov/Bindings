using Bindings.Expressions;
using UnityEngine;

namespace Bindings.Streams
{
    public class FloatStream: ValueStream, IValueReader<float>, IValueReader<int>
    {
        [SerializeField] private float _value;

        public float GetValue()
        {
            return _value;
        }

        protected void UpdateValue(float value)
        {
            _value = value;
            NotifyNext();
        }

        int IValueReader<int>.GetValue()
        {
            return Mathf.RoundToInt(_value);
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool EvaluateCondition(Condition condition, float value)
        {
            return condition == Condition.Greater && _value > value;
        }

        public override bool EvaluateCondition(Condition condition, ValueStream stream)
        {
            var asFloat = stream as IValueReader<float>;
            return asFloat != null && EvaluateCondition(condition, asFloat.GetValue());
        }

        private void OnValidate()
        {
            NotifyNext();
        }
    }
}
