using Bindings.Objects;
using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableFloatStream: FloatStream, IMutableValueStream<float>
    {
        [SerializeField] private FloatObject _reference;
        [SerializeField] private float _value;

        protected override void Bind()
        {
            if (_reference != null)
            {
                _reference.Next += NotifyNext;
            }

            NotifyNext();
        }

        protected override void Unbind()
        {
            if (_reference != null)
            {
                _reference.Next -= NotifyNext;
            }
        }

        public override float GetValue()
        {
            return _reference != null ? _reference.GetValue() : _value;
        }

        public void SetValue(float value)
        {
            if (_reference != null)
            {
                _reference.SetValue(value);
            }
            else// if (!_value.Equals(value))
            {
                _value = value;
                NotifyNext();
            }
        }
    }
}
