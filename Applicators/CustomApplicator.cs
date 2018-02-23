using System.Collections.Generic;

namespace Bindings.Applicators
{
    public abstract class CustomApplicator: Applicator
    {
        private List<IPropertyApplicator> _properties;

        private void Awake()
        {
            _properties = new List<IPropertyApplicator>();
            BindProperties();
        }

        private void OnDestroy()
        {
            _properties.Clear();
        }

        protected void BindProperty<T>(IValueReaderStream<T> source, System.Action<T> action)
        {
            _properties.Add(new PropertyApplicator<T>(source, action));
        }

        protected abstract void BindProperties();

        protected sealed override void Bind()
        {
            for (int i = 0, count = _properties.Count; i < count; ++i)
            {
                _properties[i].Bind();
            }
        }

        protected sealed override void Unbind()
        {
            for (int i = 0, count = _properties.Count; i < count; ++i)
            {
                _properties[i].Unbind();
            }
        }

        private interface IPropertyApplicator
        {
            void Bind();
            void Unbind();
        }

        private class PropertyApplicator<T>: IPropertyApplicator
        {
            private readonly IValueReaderStream<T> _source;
            private readonly System.Action<T> _action;

            public PropertyApplicator(IValueReaderStream<T> source, System.Action<T> action)
            {
                _source = source;
                _action = action;
            }

            private void Apply()
            {
                if (_action != null && _source != null)
                {
                    _action(_source.GetValue());
                }
            }

            void IPropertyApplicator.Bind()
            {
                _source.Next += Apply;
                Apply();
            }

            void IPropertyApplicator.Unbind()
            {
                _source.Next -= Apply;
            }
        }
    }
}
