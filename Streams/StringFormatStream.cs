using Bindings.Properties;
using Bindings.Text;
using UnityEngine;

namespace Bindings.Streams
{
    public class StringFormatStream: StringStream
    {
        [SerializeField] private TupleProperty _values;

        private string _value;
        private IFormatProvider _formatProvider;

        private void Awake()
        {
            _formatProvider = GetComponent<IFormatProvider>();
        }

        protected override void Bind()
        {
            _values.Next += OnNext;
        }

        protected override void Unbind()
        {
            _values.Next -= OnNext;
        }

        private void OnNext()
        {
            var value = _formatProvider != null
                ? _formatProvider.Format(_values.ConvertAll(s => s.ToString()))
                : string.Empty;

            if (value != _value)
            {
                _value = value;
                NotifyNext();
            }
        }

        public override string GetValue()
        {
            return _value;
        }
    }
}
