using Bindings.Properties;
using UnityEngine;

namespace Bindings.Streams
{
    public class ConditionalStream: BooleanStream
    {
        [SerializeField] private ConditionalProperty _conditions;

        private void OnEnable()
        {
            _conditions.Next += NotifyNext;
            NotifyNext();
        }

        private void OnDisable()
        {
            _conditions.Next -= NotifyNext;
        }

        public override bool GetValue()
        {
            return _conditions.GetValue();
        }
    }
}
