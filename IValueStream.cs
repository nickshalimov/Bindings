namespace Bindings
{
    public interface IValueStream<T>: IStream
    {
        T GetValue();
    }
}
