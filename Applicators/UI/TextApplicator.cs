using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class TextApplicator: CustomApplicator
    {
        [SerializeField] private StringStream _text;

        protected override void BindProperties()
        {
            var textField = GetComponent<UnityEngine.UI.Text>();
            BindProperty(_text, () => textField.text = _text.GetValue());
        }
    }
}
