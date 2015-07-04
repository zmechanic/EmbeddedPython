namespace EmbeddedPython
{
    using System;

    public class PythonUnicodeString : IEquatable<PythonUnicodeString>
    {
        private readonly string _value;

        public PythonUnicodeString(string value)
        {
            this._value = value;
        }

        public static PythonUnicodeString Empty
        {
            get
            {
                return new PythonUnicodeString(string.Empty);
            }
        }

        public static PythonUnicodeString FromString(string value)
        {
            return new PythonUnicodeString(value);
        }

        public static implicit operator string(PythonUnicodeString pys)
        {
            return pys.ToString();
        }

        public static implicit operator PythonUnicodeString(string s)
        {
            return new PythonUnicodeString(s);
        }

        public bool Equals(PythonUnicodeString other)
        {
            if (other == null)
            {
                return false;
            }

            return this._value == other._value;
        }

        public override string ToString()
        {
            return _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is PythonUnicodeString))
            {
                return false;
            }

            return this.Equals((PythonUnicodeString)obj);
        }
    }
}