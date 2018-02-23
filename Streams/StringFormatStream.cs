using Bindings.Text;
using UnityEngine;

namespace Bindings.Streams
{
    public class StringFormatStream: StringStream
    {
        [SerializeField] private TupleStream _values;

        private IFormatProvider _formatProvider;

        private void Awake()
        {
            _formatProvider = GetComponent<IFormatProvider>();
        }

        private void OnEnable()
        {
            _values.Next += OnNext;
            OnNext();
        }

        private void OnDisable()
        {
            _values.Next -= OnNext;
        }

        private void OnNext()
        {
            var value = _formatProvider != null
                ? _formatProvider.Format(_values.ConvertAll(s => s.ToString()))
                : string.Empty;

            UpdateValue(value);
        }
    }
}
