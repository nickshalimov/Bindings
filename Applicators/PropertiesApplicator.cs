using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators
{
    public abstract class PropertiesApplicator: Applicator
    {
        private PropertyApplicator[] _properties = {};

        private void Awake()
        {
            _properties = GetProperties() ?? new PropertyApplicator[] { };
        }

        protected abstract PropertyApplicator[] GetProperties();

        protected sealed override void OnBind()
        {
            for (int i = 0, count = _properties.Length; i < count; ++i)
            {
                _properties[i].Bind();
            }
        }

        protected sealed override void OnUnbind()
        {
            for (int i = 0, count = _properties.Length; i < count; ++i)
            {
                _properties[i].Unbind();
            }
        }

        [System.Serializable]
        public abstract class PropertyApplicator
        {
            [SerializeField] protected Stream _stream;

            public void Bind()
            {
                if (_stream == null)
                {
                    return;
                }

                _stream.Next += OnNext;
                OnNext();
            }

            public void Unbind()
            {
                if (_stream == null)
                {
                    return;
                }

                _stream.Next -= OnNext;
            }

            protected abstract void OnNext();
        }

        protected class BasePropertyApplicator<T>: PropertyApplicator
        {
            private System.Action<T> _action;

            public BasePropertyApplicator<T> WithAction(System.Action<T> action)
            {
                _action = action;
                return this;
            }

            protected override void OnNext()
            {
                var valueReader = _stream as IValueReader<T>;

                if (_action == null || valueReader == null)
                {
                    return;
                }

                _action(valueReader.GetValue());
            }
        }

        [System.Serializable] protected class BoolPropertyApplicator: BasePropertyApplicator<bool> {}
        [System.Serializable] protected class IntPropertyApplicator: BasePropertyApplicator<int> {}
        [System.Serializable] protected class FloatPropertyApplicator: BasePropertyApplicator<float> {}
        [System.Serializable] protected class StringhPropertyApplicator: BasePropertyApplicator<string> {}
    }
}