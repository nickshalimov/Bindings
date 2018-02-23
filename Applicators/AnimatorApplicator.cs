using Bindings.Streams;
using UnityEngine;

namespace Bindings.Applicators
{
    public class AnimatorApplicator: Applicator
    {
        [SerializeField] private Parameter[] _parameters = {};

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void Bind()
        {
            for (int i = 0, count = _parameters.Length; i < count; ++i)
            { 
                _parameters[i].Bind(_animator);
            }
        }

        protected override void Unbind()
        {
            for (int i = 0, count = _parameters.Length; i < count; ++i)
            { 
                _parameters[i].Unbind();
            }
        }

        [System.Serializable]
        private class Parameter
        {
            [SerializeField] private string _name;
            [SerializeField] private Stream _stream;

            private System.Action _apply;

            public void Bind(Animator animator)
            {
                var parameter = System.Array.Find(animator.parameters, p => p.name == _name);

                if (parameter == null)
                {
                    return;
                }

                var nameHash = parameter.nameHash;

                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Bool:
                        var asBool = _stream as IValueReader<bool>;
                        if (asBool != null)
                        {
                            _apply = () => animator.SetBool(nameHash, asBool.GetValue());
                        }
                        break;

                    case AnimatorControllerParameterType.Int:
                        var asInt = _stream as IValueReader<int>;
                        if (asInt != null)
                        {
                            _apply = () => animator.SetInteger(nameHash, asInt.GetValue());
                        }
                        break;

                    case AnimatorControllerParameterType.Float:
                        var asFloat = _stream as IValueReader<float>;
                        if (asFloat != null)
                        {
                            _apply = () => animator.SetFloat(nameHash, asFloat.GetValue());
                        }
                        break;

                    case AnimatorControllerParameterType.Trigger:
                        _apply = () => animator.SetTrigger(nameHash);
                        break;
                }

                if (_apply != null)
                {
                    _stream.Next += _apply;
                }
            }

            public void Unbind()
            {
                if (_apply != null)
                {
                    _stream.Next -= _apply;
                    _apply = null;
                }
            }
        }
    }
}
