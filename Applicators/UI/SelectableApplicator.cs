using Bindings.Streams;
using UnityEngine;
using UnityEngine.UI;

namespace Bindings.Applicators.UI
{
    public class SelectableApplicator: PropertiesApplicator
    {
        [SerializeField] private BoolPropertyApplicator _interactable;

        protected override PropertyApplicator[] GetProperties()
        {
            var selectable = GetComponent<Selectable>();

            return selectable == null
                ? null
                : new PropertyApplicator[]
                {
                    _interactable.WithAction(v => selectable.interactable = v)
                };
        }
    }
}
