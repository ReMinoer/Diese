namespace Diese
{
    public class Defaultable<T>
    {
        private bool _userDefined;
        private T _userValue;
        public T DefaultValue { get; set; }

        public T Value
        {
            get { return _userDefined ? _userValue : DefaultValue; }
            set
            {
                _userValue = value;
                _userDefined = true;
            }
        }

        public Defaultable()
        {
        }

        public Defaultable(T defaultValue)
        {
            DefaultValue = defaultValue;
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