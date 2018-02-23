using Bindings.Properties;

namespace Bindings.Streams
{
    public sealed class MutableBooleanStream: BooleanStream, IValueWriter<bool>
    {
        public ConditionalProperty prop;

        public void SetValue(bool value)
        {
            UpdateValue(value);
        }
    }
}
