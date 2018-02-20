using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators.UI
{
    public class CanvasGroupApplicator: PropertiesApplicator
    {
        [SerializeField] private FloatPropertyApplicator _alpha;
        [SerializeField] private BoolPropertyApplicator _interactable;

        protected override PropertyApplicator[] GetProperties()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                return null;
            }

            return new PropertyApplicator[]
            {
                _alpha.WithAction(v => canvasGroup.alpha = v),
                _interactable.WithAction(v => canvasGroup.interactable = v)
            };
        }
    }
}
