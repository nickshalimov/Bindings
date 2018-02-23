using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableStringStream: StringStream, IValueWriter<string>
    {
        [SerializeField] private string _value;

        public override string GetValue()
        {
            return _value;
        }

        public void SetValue(string value)
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
