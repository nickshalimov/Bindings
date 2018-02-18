namespace Bindings.Streams
{
    public enum Condition
    {
        Equals,
        Greater
    }

    public abstract class ValueStream: Stream
    {
        public abstract bool EvaluateCondition(Condition condition, ValueStream stream);
    }
}
