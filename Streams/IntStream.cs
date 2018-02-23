namespace Bindings.Streams
{
    public abstract class IntStream: ValueStream, IValueReader<int>
    {
        public abstract int GetValue();

        public override string ToString()
        {
            return GetValue().ToString();
        }
    }
}
