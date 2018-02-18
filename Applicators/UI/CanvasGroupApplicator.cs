using UnityEngine;

namespace Bindings.Applicators.UI
{
    public class CanvasGroupApplicator: Applicator
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void OnBind()
        {
            
        }

        protected override void OnUnbind()
        {
            
        }
    }
}
