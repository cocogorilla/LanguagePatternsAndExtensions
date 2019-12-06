using System;
using System.Collections.Generic;

namespace LanguagePatternsAndExtensions
{
    public struct Outcome<TValue> : IEquatable<Outcome<TValue>>
    {
        public bool Equals(Outcome<TValue> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value)
                   && string.Equals(ErrorMessage, other.ErrorMessage, StringComparison.InvariantCulture)
                   && Succeeded == other.Succeeded;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Outcome<TValue> && Equals((Outcome<TValue>)obj);
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

        public TResult Traverse<TResult>(Func<TValue, TResult> success, Func<TValue, string, TResult> error)
        {
            if (success == null) throw new ArgumentNullException(nameof(success));
            if (error == null) throw new ArgumentNullException(nameof(error));

            if (Succeeded) return success(Value);
            return error(Value, ErrorMessage);
        }

        public Unit Traverse(Action<TValue> success, Action<TValue, string> error)
        {
            if (success == null) throw new ArgumentNullException(nameof(success));
            if (error == null) throw new ArgumentNullException(nameof(error));

            if (Succeeded) success(Value);
            else error(Value, ErrorMessage);

            return Unit.Default;
        }

        private readonly TValue Value;
        private readonly string ErrorMessage;
        public bool Succeeded { get; }
    }
}