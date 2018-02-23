using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableBooleanStream: BooleanStream, IValueWriter<bool>
    {
        [SerializeField] private bool _value;

        public override bool GetValue()
        {
            return _value;
        }

        public void SetValue(bool value)
        {
            if (value == _value)
            {
                return;
            }

            _value = value;
            NotifyNext();
        }
    }
}
