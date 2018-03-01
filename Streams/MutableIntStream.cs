using Bindings.Objects;
using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableIntStream: IntStream, IMutableValueStream<int>
    {
        [SerializeField] private IntObject _reference;
        [SerializeField] private int _value;

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

        public override int GetValue()
        {
            return _reference != null ? _reference.GetValue() : _value;
        }

        public void SetValue(int value)
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
