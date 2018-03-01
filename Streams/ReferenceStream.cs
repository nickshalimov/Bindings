namespace Bindings.Streams
{
    public abstract class ReferenceStream<T>: Stream, IMutableValueStream<T>
    {
        private IMutableValueStream<T> _boundReference;

        public T GetValue()
        {
            return _boundReference != null ? _boundReference.GetValue() : default(T);
        }

        public void SetValue(T value)
        {
            if (_boundReference != null)
            {
                _boundReference.SetValue(value);
            }
        }

        protected abstract IMutableValueStream<T> GetReference();

        protected void UpdateReference()
        {
            var reference = GetReference();
            if (_boundReference == null || _boundReference == reference)
            {
                return;
            }

            Unbind();
            Bind(reference);
        }

        private void OnEnable()
        {
            Bind(GetReference());

            if (_boundReference != null)
            {
                NotifyNext();
            }
        }

        private void OnDisable()
        {
            Unbind();
        }

        private void Bind(IMutableValueStream<T> reference)
        {
            _boundReference = reference;
            if (_boundReference != null)
            {
                _boundReference.Next += NotifyNext;
            }
        }

        private void Unbind()
        {
            if (_boundReference != null)
            {
                _boundReference.Next -= NotifyNext;
                _boundReference = null;
            }
        }

        private void OnValidate()
        {
            UpdateReference();
        }
    }
}
