using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators
{
    public abstract class StringApplicator: Applicator
    {
        [SerializeField] private ValueStream _stream;

        protected override void OnBind()
        {
            _stream.Next += OnNext;
            OnNext();
        }

        protected override void OnUnbind()
        {
            _stream.Next -= OnNext;
        }

        private void OnNext()
        {
            var asString = _stream as IValueReader<string>;

            var value = asString != null
                ? asString.GetValue()
                : _stream.ToString();

            Apply(value);
        }

        protected abstract void Apply(string value);
    }
}
