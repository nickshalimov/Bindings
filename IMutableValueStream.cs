namespace Bindings
{
    public interface IMutableValueStream<T>: IValueStream<T>
    {
        void SetValue(T value);
    }
}
