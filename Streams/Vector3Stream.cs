using Bindings.Objects;
using UnityEngine;

namespace Bindings.Streams
{
    public sealed class Vector3Stream: ReferenceStream<Vector3>
    {
        [SerializeField] private Vector3Object _reference;

        protected override IMutableValueStream<Vector3> GetReference()
        {
            return _reference;
        }
    }
}
