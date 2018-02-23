using UnityEngine;

namespace Bindings.Streams
{
    public sealed class MutableFloatStream: FloatStream, IValueWriter<float>
    {
        [SerializeField] private float _value;

        public override float GetValue()
        {
            return _value;
        }

        public void SetValue(float value)
        {
            _value = value;
            NotifyNext();
        }
    }
}
