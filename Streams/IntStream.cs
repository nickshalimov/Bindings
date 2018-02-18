using UnityEngine;

namespace Bindings.Streams
{
    public class IntStream: ValueStream, IValueReader<int>, IValueReader<float>
    {
        [SerializeField] private int _value;

        public int GetValue()
        {
            return _value;
        }
        
        protected void UpdateValue(int value)
        {
            if (_value != value)
            {
                _value = value;
                NotifyNext();
            }
        }

        float IValueReader<float>.GetValue()
        {
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool EvaluateCondition(Condition condition, int value)
        {
            return condition == Condition.Equals && _value == value
                || condition == Condition.Greater && _value > value;
        }

        public override bool EvaluateCondition(Condition condition, ValueStream stream)
        {
            var asInt = stream as IValueReader<int>;
            return asInt != null && EvaluateCondition(condition, asInt.GetValue());
        }

        private void OnValidate()
        {
            NotifyNext();
        }
    }
}
