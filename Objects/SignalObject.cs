using UnityEngine;

namespace Bindings.Objects
{
    [CreateAssetMenu(menuName = "Bindings/Objects/Signal")]
    public class SignalObject: ScriptableObject, IStream
    {
        public event System.Action Next;

        [ContextMenu("Dispatch")]
        public void Dispatch()
        {
            if (Next != null)
            {
                Next();
            }
        }
    }
}
