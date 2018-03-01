using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators
{
    public class AngleAxisApplicator: Applicator
    {
        [SerializeField] private Space _space;
        [SerializeField] private Vector3 _axis = Vector3.up;
        [SerializeField] private FloatStream _stream;
        [SerializeField] private float _damping = 10.0f;

        private float _angle;
        private float _targetAngle;

        protected override void Bind()
        {
            if (_stream != null)
            {
                _stream.Next += OnNext;
                _angle = _stream.GetValue();
                OnNext();
            }
        }

        protected override void Unbind()
        {
            if (_stream != null)
            {
                _stream.Next -= OnNext;
            }
        }

        private void OnNext()
        {
            _targetAngle = _stream.GetValue();
        }

        private void LateUpdate()
        {
            var oldAngle = _angle;
            _angle = _damping > 0.0f
                ? Mathf.LerpAngle(_angle, _targetAngle, _damping * Time.deltaTime)
                : _targetAngle;

            transform.Rotate(_axis, _angle - oldAngle, _space);
        }
    }
}
