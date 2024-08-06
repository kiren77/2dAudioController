using System;

namespace ContextStateGroup
{
    public class TypeManager<T>
    {
        private T _currentType;
        private readonly Action<T> _onTypeChanged;

        public TypeManager(Action<T> onTypeChanged)
        {
            _onTypeChanged = onTypeChanged;
        }

        public T GetCurrentType()
        {
            return _currentType;
        }

        public void SetCurrentType(T newType)
        {
            if (!Equals(_currentType, newType))
            {
                _currentType = newType;
                _onTypeChanged?.Invoke(newType);
            }
        }
    }
}
