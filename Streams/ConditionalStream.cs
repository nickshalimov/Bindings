using System.Collections.Generic;
using UnityEngine;

namespace Bindings.Streams
{
    public class ConditionalStream: BooleanStream
    {
        [SerializeField] private Condition[] _conditions = {};

        private HashSet<ValueStream> _streams;

        private void Awake()
        {
            _streams = new HashSet<ValueStream>();
            for (int i = 0, count = _conditions.Length; i < count; ++i)
            {
                var condition = _conditions[i];

                if (condition.Stream == null)
                {
                    continue;
                }

                _streams.Add(condition.Stream);

                if (condition.OtherStream != null)
                {
                    _streams.Add(condition.OtherStream);
                }
            }
        }

        private void OnDestroy()
        {
            _streams.Clear();
        }

        private void OnEnable()
        {
            foreach (var stream in _streams)
            {
                stream.Next += OnNext;
            }

            OnNext();
        }

        private void OnDisable()
        {
            foreach (var stream in _streams)
            {
                stream.Next -= OnNext;
            }
        }

        private bool Evaluate()
        {
            return System.Array.TrueForAll(_conditions, c => c.IsTrue);
            //System.Array.Exists(_conditions, c => c.IsTrue);
        }

        private void OnNext()
        {
            UpdateValue(Evaluate());
        }

        [System.Serializable]
        private class Condition
        {
            [SerializeField] private ValueStream _stream;

            [SerializeField] private bool _inverse;
            [SerializeField] private Streams.Condition _condition;

            [SerializeField] private int _int;
            [SerializeField] private float _float;
            [SerializeField] private string _string;
            [SerializeField] private ValueStream _otherStream;

            public ValueStream Stream
            {
                get { return _stream; }
            }

            public ValueStream OtherStream
            {
                get { return _otherStream; }
            }

            public bool IsTrue
            {
                get { return _inverse ^ Evaluate(); }
            }

            private bool Evaluate()
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

                var asString = _stream as MutableStringStream;
                if (asString != null)
                {
                    return asString.EvaluateCondition(_condition, _string);
                }

                return false;
            }
        }
    }
}
