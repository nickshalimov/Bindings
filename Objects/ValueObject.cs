using UnityEngine;

namespace Bindings.Objects
{
    public abstract class ValueObject<T>: ScriptableObject, IMutableValueStream<T>
    {
        [SerializeField] private T _value;

        public event System.Action Next;

        public T GetValue()
        {
            return _value;
        }

        public void SetValue(T value)
        {
            if (_value.Equals(value))
            {
                return;
            }

            _value = value;
            NotifyNext();
        }

        private void NotifyNext()
        {
            if (Next != null)
            {
                Next();
            }
        }

        private void OnValidate()
        {
            NotifyNext();
        }
    }
}
