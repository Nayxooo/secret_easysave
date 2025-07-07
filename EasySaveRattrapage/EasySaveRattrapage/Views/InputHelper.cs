using System;

namespace Views
{
    public static class InputHelper
    {
        public static string AskNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(input));

            return input;
        }
    }
}
