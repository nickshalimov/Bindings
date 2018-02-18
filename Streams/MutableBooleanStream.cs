namespace Bindings.Streams
{
    public sealed class MutableBooleanStream: BooleanStream, IValueWriter<bool>
    {
        public void SetValue(bool value)
        {
            UpdateValue(value);
        }
    }
}
