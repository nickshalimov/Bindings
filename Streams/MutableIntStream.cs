using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableIntStream: IntStream, IValueWriter<int>
    {
        [SerializeField] private int _value;

        public override int GetValue()
        {
            return _value;
        }

        public void SetValue(int value)
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
