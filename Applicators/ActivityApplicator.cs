using Bindings.Expressions;
using UnityEngine;

namespace Bindings.Applicators
{
    public class ActivityApplicator: Applicator
    {
        [SerializeField] private Entry[] _entries = {};

        protected override void OnBind()
        {
            for (int i = 0, count = _entries.Length; i < count; ++i)
            {
                _entries[i].Bind();
            }
        }

        protected override void OnUnbind()
        {
            for (int i = 0, count = _entries.Length; i < count; ++i)
            {
                _entries[i].Unbind();
            }
        }

        [System.Serializable]
        private class Entry
        {
            [SerializeField] private ConditionalExpression _expression;
            [SerializeField] private GameObject _target;

            public void Bind()
            {
                _expression.Next += OnNext;
            }

            public void Unbind()
            {
                _expression.Next -= OnNext;
            }

            private void OnNext()
            {
                if (_target != null)
                {
                    _target.SetActive(_expression.GetValue());
                }
            }
        }
    }
}
