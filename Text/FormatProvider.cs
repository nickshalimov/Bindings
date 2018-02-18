using UnityEngine;

namespace Bindings.Text
{
    public class FormatProvider: MonoBehaviour, IFormatProvider
    {
        [SerializeField] private string _format;

        string IFormatProvider.Format(params object[] args)
        {
            var result = string.Empty;

            try
            {
                result = string.Format(_format, args);
            }
            catch (System.FormatException e)
            {
                Debug.LogErrorFormat("[FormatProvider] {0}", e);
            }

            return result;
        }
    }
}
