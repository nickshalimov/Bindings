using UnityEngine;

namespace Bindings.Applicators
{
    public abstract class Applicator: MonoBehaviour
    {
        private void OnEnable()
        {
            OnBind();
        }

        private void OnDisable()
        {
            OnUnbind();
        }

        protected abstract void OnBind();
        protected abstract void OnUnbind();
    }
}
