namespace Bindings.Streams
{
    public abstract class ValueStream: Stream
    {

        private void OnValidate()
        {
            NotifyNext();
        }

    }
}
