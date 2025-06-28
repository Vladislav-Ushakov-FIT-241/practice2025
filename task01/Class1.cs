using System;
using System.Linq;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var cleanString = input
                .ToLower()
                .Where(c => !char.IsPunctuation(c) && !char.IsWhiteSpace(c))
                .ToArray();

            for (int i = 0; i < cleanString.Length / 2; i++)
            {
                if (cleanString[i] != cleanString[cleanString.Length - 1 - i])
                    return false;
            }

            return true;
        }
    }
}
