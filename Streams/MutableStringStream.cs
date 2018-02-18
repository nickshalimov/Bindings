namespace Bindings.Streams
{
    public sealed class MutableStringStream: StringStream, IValueWriter<string>
    {
        public void SetValue(string value)
        {
            UpdateValue(value);
        }
    }
}
