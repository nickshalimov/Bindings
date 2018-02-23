namespace Bindings.Properties
{
    public abstract class Property: IStream
    {
        public event System.Action Next
        {
            add
            {
                if (_next == null)
                {
                    Bind();
                }

                _next += value;
            }

            remove
            {
                _next -= value;

                if (_next == null)
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
