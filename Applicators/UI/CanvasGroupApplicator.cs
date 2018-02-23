using Bindings.Properties;
using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators.UI
{
    public class CanvasGroupApplicator: CustomApplicator
    {
        [SerializeField] private FloatStream _alpha;
        [SerializeField] private ConditionalProperty _interactable;

        protected override void BindProperties()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                return;
            }

            BindProperty(_alpha, () => canvasGroup.alpha = _alpha.GetValue());
            BindProperty(_interactable, () => canvasGroup.interactable = _interactable.GetValue());
        }
    }
}
