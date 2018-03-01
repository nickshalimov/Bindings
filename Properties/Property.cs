namespace Bindings.Properties
{
    public abstract class Property: IStream
    {
        public event System.Action Next
        {
            add
            {
                var bound = _next == null;
                _next += value;

                if (!bound && _next != null)
                {
                    Bind();
                }
            }

            remove
            {
                var bound = _next != null;
                _next -= value;

                if (bound && _next == null)
                {
                    Unbind();
                }
            }
        }

        private event System.Action _next;

        protected abstract void Bind();
        protected abstract void Unbind();

        protected void NotifyNext()
        {
            if (_next != null)
            {
                _next();
            }
        }
    }
}
