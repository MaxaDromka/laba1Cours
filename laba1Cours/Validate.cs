using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace laba1Cours
{
    public class Validate
    {
        private char [] _NumberOfSeparators = { '+', '=', '^', '*', '/', '\n','(',')' };
        public bool IsLetters(char symbol)
        {
            string NumberOfLetters = " " + symbol;
            return Regex.IsMatch(NumberOfLetters, "[a-zA-z]+$");
        }

        public bool IsNumbers(char symbol)
        {
            string SeriesOfNumbers = " " + symbol;
            return Regex.IsMatch(SeriesOfNumbers, "[0-9]+$");
        }

        public bool IsSeparator(char symbol)
        {
            char i = symbol;
            return _NumberOfSeparators.Contains(i);
        }
    }
}

