using UnityEngine;

namespace Bindings.Streams
{
    public class MultiValueStream: Stream
    {
        [SerializeField] private ValueStream[] _valueStreams = {};

        public ValueStream[] Streams
        {
            get { return _valueStreams; }
        }

        private void OnEnable()
        {
            for (int i = 0, count = _valueStreams.Length; i < count; ++i)
            {
                _valueStreams[i].Next += NotifyNext;
            }
        }

        private void OnDisable()
        {
            for (int i = 0, count = _valueStreams.Length; i < count; ++i)
            {
                _valueStreams[i].Next -= NotifyNext;
            }
        }
    }
}
