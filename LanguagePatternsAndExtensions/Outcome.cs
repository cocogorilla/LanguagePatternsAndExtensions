using System;
using System.Collections.Generic;

namespace LanguagePatternsAndExtensions
{
    public class Outcome<TValue> : IEquatable<Outcome<TValue>>
    {
        public bool Equals(Outcome<TValue> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value) &&
                string.Equals(ErrorMessage, other.ErrorMessage, StringComparison.InvariantCulture)
                && Succeeded == other.Succeeded;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Outcome<TValue>;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<TValue>.Default.GetHashCode(Value);
                hashCode = (hashCode * 397) ^ (ErrorMessage != null ? StringComparer.InvariantCulture.GetHashCode(ErrorMessage) : 0);
                hashCode = (hashCode * 397) ^ Succeeded.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Outcome<TValue> left, Outcome<TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Outcome<TValue> left, Outcome<TValue> right)
        {
            return !Equals(left, right);
        }

        public Outcome(TValue value, bool succeeded, string errorMessage = "")
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));
            Value = value;
            ErrorMessage = errorMessage;
            Succeeded = succeeded;
        }

        public TValue Value { get; }
        public string ErrorMessage { get; }
        public bool Succeeded { get; }
    }

    public static class Success
    {
        public static Outcome<T> Of<T>(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new Outcome<T>(value, true);
        }

        public static Outcome<Unit> Ok()
        {
            return new Outcome<Unit>(Unit.Default, true);
        }
    }

    public static class Failure
    {
        public static Outcome<T> Of<T>(T value, string errorMessage)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));
            return new Outcome<T>(value, false, errorMessage);
        }

        public static Outcome<Unit> Nok(string message)
        {
            return new Outcome<Unit>(Unit.Default, false, message);
        }
    }
}
