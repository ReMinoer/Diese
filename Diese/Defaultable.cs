namespace Diese
{
    public struct Defaultable<T> : IDefaultable<T>
    {
        private bool _userDefined;
        private T _userValue;
        public T DefaultValue { get; set; }

        public T Value
        {
            get => _userDefined ? _userValue : DefaultValue;
            set
            {
                _userValue = value;
                _userDefined = true;
            }
        }

        public void SetToDefault()
        {
            _userValue = default(T);
            _userDefined = false;
        }

        static public implicit operator T(Defaultable<T> defaultable)
        {
            return defaultable.Value;
        }
    }
}