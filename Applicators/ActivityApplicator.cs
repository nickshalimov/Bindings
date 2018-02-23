using Bindings.Properties;
using UnityEngine;

namespace Bindings.Applicators
{
    public class ActivityApplicator: Applicator
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private ConditionalProperty _condition;

        protected override void OnBind()
        {
            _condition.Next += OnNext;
            OnNext();
        }

        protected override void OnUnbind()
        {
            _condition.Next -= OnNext;
        }

        private void OnNext()
        {
            if (_target != null)
            {
                _target.SetActive(_condition.GetValue());
            }
        }
    }
}
