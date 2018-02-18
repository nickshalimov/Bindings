using UnityEngine;

namespace Bindings.Streams
{
    public class Stream: MonoBehaviour, IStream
    {
        [SerializeField] private string _name;

        public event System.Action Next;

        public string Name
        {
            get { return _name; }
        }

        protected void NotifyNext()
        {
            if (Next != null)
            {
                Next();
            }
        }
    }
}
