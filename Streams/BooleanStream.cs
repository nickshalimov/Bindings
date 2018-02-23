namespace Bindings.Streams
{
    public abstract class BooleanStream: ValueStream, IValueReader<bool>
    {
        public abstract bool GetValue();

        public override string ToString()
        {
            return GetValue().ToString();
        }
    }
}
