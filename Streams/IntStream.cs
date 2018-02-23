using UnityEngine;

namespace Bindings.Streams
{
    public class IntStream: ValueStream, IValueReaderStream<int>
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
