using Bindings.Objects;
using UnityEngine;

namespace Bindings.Streams
{
    public sealed class ColorStream: ReferenceStream<Color>
    {
        [SerializeField] private ColorObject _reference;

        protected override IMutableValueStream<Color> GetReference()
        {
            return _reference;
        }
    }
}