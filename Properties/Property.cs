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
                    OnBind();
                }

                _next += value;
            }

            remove
            {
                _next -= value;

                if (_next == null)
                {
                    OnUnbind();
                }
            }
        }

        private event System.Action _next;

        protected abstract void OnBind();
        protected abstract void OnUnbind();

        protected void NotifyNext()
        {
            if (_next != null)
            {
                _next();
            }
        }
    }
}
