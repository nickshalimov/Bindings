using System.Collections.Generic;
using Bindings.Expressions;
using UnityEngine;

namespace Bindings.Streams
{
    public class ConditionalStream: BooleanStream
    {
        [SerializeField] private ConditionalExpression[] _conditions = {};
        [SerializeField] private bool _any;

        private void OnEnable()
        {
            for (int i = 0, count = _conditions.Length; i < count; ++i)
            {
                _conditions[i].Bind();
                _conditions[i].Next += OnNext;
            }

            OnNext();
        }

        private void OnDisable()
        {
            for (int i = 0, count = _conditions.Length; i < count; ++i)
            {
                _conditions[i].Next -= OnNext;
                _conditions[i].Unbind();
            }
        }

        private bool Evaluate()
        {
            return _any
                ? System.Array.Exists(_conditions, c => c.GetValue()) 
                : System.Array.TrueForAll(_conditions, c => c.GetValue());
        }

        private void OnNext()
        {
            UpdateValue(Evaluate());
        }
    }
}
