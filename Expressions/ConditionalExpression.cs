using Bindings.Streams;
using UnityEngine;

namespace Bindings.Expressions
{
    public enum Condition
    {
        Equals,
        Greater
    }

    [System.Serializable]
    public sealed class ConditionalExpression: IStream, IValueReader<bool>
    {
        [SerializeField] private ValueStream _stream;

        [SerializeField] private bool _inverse;
        [SerializeField] private Condition _condition;

        [SerializeField] private int _int;
        [SerializeField] private float _float;
        [SerializeField] private string _string;
        [SerializeField] private ValueStream _otherStream;

        public event System.Action Next;

        private bool _value;

        public bool GetValue()
        {
            return _value;
        }

        public void Bind()
        {
            if (_stream == null)
            {
                return;
            }

            _stream.Next += OnNext;
            
            if (_otherStream != null)
            {
                _otherStream.Next += OnNext;
            }

            OnNext();
        }

        public void Unbind()
        {
            if (_stream != null)
            {
                _stream.Next += OnNext;
            }

            if (_otherStream != null)
            {
                _otherStream.Next -= OnNext;
            }
        }

        private void OnNext()
        {
            if (_value == Evaluate())
            {
                return;
            }

            _value = !_value;
            if (Next != null)
            {
                Next();
            }
        }

        private bool Evaluate()
        {
            return _inverse ^ EvaluateDirect();
        }

        private bool EvaluateDirect()
        {
            if (_otherStream != null)
            {
                return _stream.EvaluateCondition(_condition, _otherStream);
            }

            var asBool = _stream as BooleanStream;
            if (asBool != null)
            {
                return asBool.GetValue();
            }

            var asInt = _stream as IntStream;
            if (asInt != null)
            {
                return asInt.EvaluateCondition(_condition, _int);
            }

            var asFloat = _stream as FloatStream;
            if (asFloat != null)
            {
                return asFloat.EvaluateCondition(_condition, _float);
            }

            var asString = _stream as StringStream;
            if (asString != null)
            {
                return asString.EvaluateCondition(_condition, _string);
            }

            return false;
        }
    }
}
