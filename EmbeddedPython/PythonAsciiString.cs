namespace EmbeddedPython
{
    using System;

    public class PythonAsciiString : IEquatable<PythonAsciiString>
    {
        private readonly string _value;

        public PythonAsciiString(string value)
        {
            this._value = value;
        }

        public static PythonAsciiString Empty
        {
            get
            {
                return new PythonAsciiString(string.Empty);
            }
        }

        public static PythonAsciiString FromString(string value)
        {
            return new PythonAsciiString(value);
        }

        public static implicit operator string(PythonAsciiString pys)
        {
            return pys.ToString();
        }

        public static implicit operator PythonAsciiString(string s)
        {
            return new PythonAsciiString(s);
        }

        public bool Equals(PythonAsciiString other)
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

            if (!(obj is PythonAsciiString))
            {
                return false;
            }

            return this.Equals((PythonAsciiString)obj);
        }
    }
}