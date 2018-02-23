using UnityEngine;

namespace Bindings.Streams
{
    public class BooleanStream: ValueStream, IValueReaderStream<bool>
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

        private void OnValidate()
        {
            NotifyNext();
        }
    }
}
