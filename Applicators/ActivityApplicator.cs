using UnityEngine;

namespace Bindings.Applicators
{
    public class ActivityApplicator: BooleanApplicator
    {
        [SerializeField] private GameObject _targetGameObject;

        protected override void Apply(bool value)
        {
            if (_targetGameObject != null)
            {
                _targetGameObject.SetActive(value);
            }
        }

        private void OnValidate()
        {
            if (_targetGameObject == null)
            {
                return;
            }
            
            if (_targetGameObject.transform == transform || transform.IsChildOf(_targetGameObject.transform))
            {
                _targetGameObject = null;
            }
        }
    }
}
