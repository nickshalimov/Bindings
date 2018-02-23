using Bindings.Streams;
using UnityEngine;

namespace Bindings.Properties
{
    [System.Serializable]
    public class TupleProperty: Property
    {
        [SerializeField] private ValueStream[] _items = {};

        public T[] ConvertAll<T>(System.Converter<ValueStream, T> converter)
        {
            return System.Array.ConvertAll(_items, converter);
        }

        protected override void Bind()
        {
            for (int i = 0, count = _items.Length; i < count; ++i)
            {
                _items[i].Next += NotifyNext;
            }
        }

        protected override void Unbind()
        {
            for (int i = 0, count = _items.Length; i < count; ++i)
            {
                _items[i].Next -= NotifyNext;
            }
        }
    }
}
