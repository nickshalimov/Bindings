using UnityEngine;

namespace Bindings.Streams
{
    public class FloatStream: ValueStream, IValueReader<float>
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
