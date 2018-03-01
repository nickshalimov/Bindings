using Bindings.Properties;
using UnityEngine;

namespace Bindings.Streams
{
    public class ConditionalStream: BooleanStream
    {
        [SerializeField] private ConditionalProperty _conditions;

        protected override void Bind()
        {
            _conditions.Next += NotifyNext;
        }

        protected override void Unbind()
        {
            _conditions.Next -= NotifyNext;
        }

        public override bool GetValue()
        {
            return _conditions.GetValue();
        }
    }
}
