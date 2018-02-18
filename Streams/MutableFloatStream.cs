namespace Bindings.Streams
{
    public sealed class MutableFloatStream: FloatStream, IValueWriter<float>
    {
        public void SetValue(float value)
        {
            UpdateValue(value);
        }
    }
}
