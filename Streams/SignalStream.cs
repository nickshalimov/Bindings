namespace Bindings.Streams
{
    public class SignalStream: Stream
    {
        public void Dispatch()
        {
            NotifyNext();
        }
    }
}
