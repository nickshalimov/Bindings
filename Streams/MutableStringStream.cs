using Bindings.Objects;
using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableStringStream: StringStream, IMutableValueStream<string>
    {
        [SerializeField] private StringObject _reference;
        [SerializeField] private string _value;

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

        public override string GetValue()
        {
            return _reference != null ? _reference.GetValue() : _value;
        }

        public void SetValue(string value)
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
