using Bindings.Expressions;

namespace Bindings.Streams
{
    public abstract class ValueStream: Stream
    {
        public abstract bool EvaluateCondition(Condition condition, ValueStream stream);
    }
}
