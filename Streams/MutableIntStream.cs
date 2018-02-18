namespace Bindings.Streams
{
    public sealed class MutableIntStream: IntStream, IValueWriter<int>
    {
        public void SetValue(int value)
        {
            UpdateValue(value);
        }
    }
}