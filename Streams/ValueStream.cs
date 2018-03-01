namespace Bindings.Streams
{
    public abstract class ValueStream: Stream
    {
        private void OnValidate()
        {
            NotifyNext();
        }

        private void OnEnable()
        {
            Bind();
            NotifyNext();
        }

        private void OnDisable()
        {
            Unbind();
        }

        protected abstract void Bind();
        protected abstract void Unbind();
    }

    public abstract class ValueStream<T>: ValueStream, IValueStream<T>
    {
        public abstract T GetValue();

        public override string ToString()
        {
            return GetValue().ToString();
        }
    }

    public abstract class BooleanStream: ValueStream<bool> {}
    public abstract class IntStream: ValueStream<int> {}
    public abstract class FloatStream: ValueStream<float> {}
    public abstract class StringStream: ValueStream<string> {}
}
