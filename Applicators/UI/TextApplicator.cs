using UnityEngine;

namespace Bindings.Applicators.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class TextApplicator: StringApplicator
    {
        private UnityEngine.UI.Text _textField;

        protected override void Apply(string value)
        {
            _textField = _textField ?? GetComponent<UnityEngine.UI.Text>();

            if (_textField != null)
            {
                _textField.text = value;
            }
        }
    }
}
