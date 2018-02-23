using Bindings.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings.Applicators.UI
{
    public class SelectableApplicator: CustomApplicator
    {
        [SerializeField] private ConditionalProperty _interactable;

        protected override void BindProperties()
        {
            var selectable = GetComponent<Selectable>();
            if (selectable == null)
            {
                return;
            }

            BindProperty(_interactable, () => selectable.interactable = _interactable.GetValue());
        }
    }
}
