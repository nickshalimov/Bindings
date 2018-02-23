using System.Collections.Generic;
using Bindings.Properties;

namespace Bindings.Applicators
{
    public abstract class CustomApplicator: Applicator
    {
        private List<PropertyApplicator> _properties;

        private void Awake()
        {
            _properties = new List<PropertyApplicator>();
            BindProperties();
        }

        private void OnDestroy()
        {
            _properties.Clear();
        }

        protected void BindProperty(IStream source, System.Action action)
        {
            if (source == null)
            {
                return;
            }

            _properties.Add(new PropertyApplicator(source, action));
        }

        protected void BindProperty(ConditionalProperty source, System.Action action)
        {
            if (source.IsEmpty())
            {
                return;
            }

            _properties.Add(new PropertyApplicator(source, action));
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

        private class PropertyApplicator
        {
            private readonly IStream _source;
            private readonly System.Action _action;

            public PropertyApplicator(IStream source, System.Action action)
            {
                _source = source;
                _action = action;
            }

            private void Apply()
            {
                if (_action != null && _source != null)
                {
                    _action();
                }
            }

            public void Bind()
            {
                _source.Next += Apply;
                Apply();
            }

            public void Unbind()
            {
                _source.Next -= Apply;
            }
        }
    }
}
