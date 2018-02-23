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

        private void OnValidate()
        {
            NotifyNext();
        }
    }
}
