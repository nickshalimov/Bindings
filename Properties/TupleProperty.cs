using Bindings.Streams;
using UnityEngine;

namespace Bindings.Properties
{
    [System.Serializable]
    public class TupleProperty: Property
    {
        [SerializeField] private ValueStream[] _items = {};

        private event System.Action _next;

        public T[] ConvertAll<T>(System.Converter<ValueStream, T> converter)
        {
            return System.Array.ConvertAll(_items, converter);
        }

        protected override void OnBind()
        {
            for (int i = 0, count = _items.Length; i < count; ++i)
            {
                _items[i].Next += NotifyNext;
            }
        }

        protected override void OnUnbind()
        {
            for (int i = 0, count = _items.Length; i < count; ++i)
            {
                _items[i].Next -= NotifyNext;
            }
        }
    }
}
