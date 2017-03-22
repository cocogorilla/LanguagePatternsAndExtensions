using System;

namespace LanguagePatternsAndExtensions
{
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