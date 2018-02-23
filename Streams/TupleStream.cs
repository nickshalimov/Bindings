using UnityEngine;

namespace Bindings.Streams
{
    [System.Serializable]
    public class TupleStream: IStream
    {
        [SerializeField] private ValueStream[] _items = {};

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

        public T[] ConvertAll<T>(System.Converter<ValueStream, T> converter)
        {
            return System.Array.ConvertAll(_items, converter);
        }

        private void Bind()
        {
            for (int i = 0, count = _items.Length; i < count; ++i)
            {
                _items[i].Next += NotifyNext;
            }
        }

        private void Unbind()
        {
            for (int i = 0, count = _items.Length; i < count; ++i)
            {
                _items[i].Next -= NotifyNext;
            }
        }

        private void NotifyNext()
        {
            if (_next != null)
            {
                _next();
            }
        }
    }
}
