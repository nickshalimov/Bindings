using Bindings.Objects;
using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableBooleanStream: BooleanStream, IMutableValueStream<bool>
    {
        [SerializeField] private BooleanObject _reference;
        [SerializeField] private bool _value;

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

        public override bool GetValue()
        {
            return _reference != null ? _reference.GetValue() : _value;
        }

        public void SetValue(bool value)
        {
            if (_reference != null)
            {
                _reference.SetValue(value);
            }
            else if (_value != value)
            {
                _value = value;
                NotifyNext();
            }
        }
    }
}
