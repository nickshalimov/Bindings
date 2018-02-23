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

        public event System.Action Next
        {
            add
            {
                if (_next == null)
                {
                    Bind();
                }

                _next += value;
            }

            remove
            {
                _next -= value;

                if (_next == null)
                {
                    Unbind();
                }
            }
        }

        private event System.Action _next;
        private bool _value;

        public bool GetValue()
        {
            return _value;
        }

        private void Bind()
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

        private void Unbind()
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
            if (_next != null)
            {
                _next();
            }
        }

        private bool Evaluate()
        {
            return _inverse ^ EvaluateDirect();
        }

        private static T ExtractValue<T>(Stream stream, T fallback)
        {
            var reader = stream as IValueReader<T>;
            return reader != null ? reader.GetValue() : fallback;
        }

        private bool EvaluateDirect()
        {
            var asBool = _stream as IValueReader<bool>;
            if (asBool != null)
            {
                //var value = ExtractValue(_otherStream, asBool.GetValue());
                return asBool.GetValue();
            }

            var asInt = _stream as IValueReader<int>;
            if (asInt != null)
            {
                var value = ExtractValue(_otherStream, _int);
                return _condition == Condition.Equals && asInt.GetValue() == value
                    || _condition == Condition.Greater && asInt.GetValue() > value;
            }

            var asFloat = _stream as IValueReader<float>;
            if (asFloat != null)
            {
                var value = ExtractValue(_otherStream, _float);
                return _condition == Condition.Greater && asFloat.GetValue() > value;
            }

            var asString = _stream as IValueReader<string>;
            if (asString != null)
            {
                var value = ExtractValue(_otherStream, _string);
                return _condition == Condition.Equals && asString.GetValue() == value;
            }

            return false;
        }
    }
}
