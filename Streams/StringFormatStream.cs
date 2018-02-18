using Bindings.Text;
using UnityEngine;

namespace Bindings.Streams
{
    public class StringFormatStream: StringStream
    {
        [SerializeField] private MultiValueStream _stream;
        
        private IFormatProvider _formatProvider;

        private void Awake()
        {
            _formatProvider = GetComponent<IFormatProvider>();
        }

        private void OnEnable()
        {
            _stream.Next += OnNext;
            OnNext();
        }

        private void OnDisable()
        {
            _stream.Next -= OnNext;
        }

        private void OnNext()
        {
            var value = _formatProvider != null
                ? _formatProvider.Format(_stream.Streams)
                : string.Empty;

            UpdateValue(value);
        }
    }
}
