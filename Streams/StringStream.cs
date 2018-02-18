using UnityEngine;

namespace Bindings.Streams
{
    public class StringStream: ValueStream, IValueReader<string>
    {
        [SerializeField] private string _value;

        public string GetValue()
        {
            return _value;
        }

        protected void UpdateValue(string value)
        {
            if (_value != value)
            {
                _value = value;
                NotifyNext();
            }
        }
        
        public override string ToString()
        {
            return _value;
        }
    
        public bool EvaluateCondition(Condition condition, string value)
        {
            return condition == Condition.Equals && _value == value;
        }

        public override bool EvaluateCondition(Condition condition, ValueStream stream)
        {
            var asString = stream as IValueReader<string>;
            return asString != null && EvaluateCondition(condition, asString.GetValue());
        }

        private void OnValidate()
        {
            NotifyNext();
        }
    }
}
