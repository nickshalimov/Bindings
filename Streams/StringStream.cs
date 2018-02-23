namespace Bindings.Streams
{
    public abstract class StringStream: ValueStream, IValueReader<string>
    {
        public abstract string GetValue();

        public override string ToString()
        {
            return GetValue();
        }
    }
}
