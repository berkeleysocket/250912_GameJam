using System;

namespace Ksy.Utility
{
    public class NotifyValue<T>
    {
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                T input = value;

                if (!_value.Equals(input))
                {
                    _value = input;
                    OnChangedValue?.Invoke(input);
                }
            }
        }
        public event Action<T> OnChangedValue;
    }
}