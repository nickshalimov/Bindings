using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators
{
    public abstract class BooleanApplicator: Applicator
    {
        [SerializeField] private BooleanStream _stream;

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
            Apply(_stream.GetValue());
        }

        protected abstract void Apply(bool value);
    }
}
