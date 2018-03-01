using UnityEngine;

namespace Bindings.Streams
{
    public class Stream: MonoBehaviour, IStream
    {
        [SerializeField] private string _id;

        public event System.Action Next;

        public string Id
        {
            get { return _id; }
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
