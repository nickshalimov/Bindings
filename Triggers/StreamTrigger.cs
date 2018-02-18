using Bindings.Streams;
using UnityEngine;

namespace Bindings.Triggers
{
    public class StreamTrigger: MonoBehaviour
    {
        [SerializeField] private Stream _stream;

        public void Pull()
        {
            UpdateStream();
        }

        public void Pull(bool value)
        {
            UpdateStream(value);
        }

        public void Pull(int value)
        {
            UpdateStream(value);
        }

        public void Pull(float value)
        {
            UpdateStream(value);
        }

        public void Pull(string value)
        {
            UpdateStream(value);
        }

        private void UpdateStream()
        {
            var signal = _stream as SignalStream;
            if (signal != null)
            {
                signal.Dispatch();
            }
        }

        private void UpdateStream<T>(T value)
        {
            var property = _stream as IValueWriter<T>;
            if (property != null)
            {
                property.SetValue(value);
            }
            else
            {
                UpdateStream();
            }
        }
    }
}
