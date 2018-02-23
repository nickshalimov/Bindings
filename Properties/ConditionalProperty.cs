using UnityEngine;

namespace Bindings.Properties
{
    [System.Serializable]
    public class ConditionalProperty: Property, IValueReader<bool>
    {
        [SerializeField] private ConditionalExpressionProperty[] _expressions = {};
        [SerializeField] private bool _any;

        private bool _value;

        public bool GetValue()
        {
            return _value;
        }

        protected override void OnBind()
        {
            for (int i = 0, count = _expressions.Length; i < count; ++i)
            {
                _expressions[i].Next += OnNext;
            }

            OnNext();
        }

        protected override void OnUnbind()
        {
            for (int i = 0, count = _expressions.Length; i < count; ++i)
            {
                _expressions[i].Next -= OnNext;
            }
        }

        private void OnNext()
        {
            var newValue = _any
                ? System.Array.Exists(_expressions, c => c.GetValue()) 
                : System.Array.TrueForAll(_expressions, c => c.GetValue());

            if (_value != newValue)
            {
                _value = newValue;
                NotifyNext();
            }
        }
    }
}
