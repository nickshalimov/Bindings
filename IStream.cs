namespace Bindings
{
    public interface IStream
    {
        event System.Action Next;
    }

    public interface IValueReaderStream<T>: IStream, IValueReader<T> {}
}
