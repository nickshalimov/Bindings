using UnityEngine;

namespace Bindings.Applicators
{
    public abstract class Applicator: MonoBehaviour
    {
        private void OnEnable()
        {
            Bind();
        }

        private void OnDisable()
        {
            Unbind();
        }

        protected abstract void Bind();
        protected abstract void Unbind();
    }
}
