namespace Bindings.Streams
{
    public abstract class FloatStream: ValueStream, IValueReader<float>
    {
        public abstract float GetValue();

        public override string ToString()
        {
            return GetValue().ToString();
        }
    }
}
