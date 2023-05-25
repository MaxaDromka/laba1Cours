using System.Collections.Generic;
using System.Linq;

namespace laba1Cours
{
    public class Token
    {
        public TokenType Type;
        public string Value;
        public Token(TokenType type)
        {
            Type = type;
        }
        public override string ToString()
        {
            return string.Format("{0}, {1}", Type, Value);
        }


        public enum TokenType
        {
            DIM, AS, SELECT, CASE, INTEGER, LONG, DOUBLE, IDENTIFIER, NUMBER,
            ELSE, END, PLUS, EQUAL, LBRACKET, RBRACKET, MINUS, MULTIPLICATION, DIVISION, COMMA, DELEMITOR, ENTER, TO,NETERMINAL,EXPR
        }

        public static Dictionary<string, TokenType> SpecialWords = new Dictionary<string, TokenType>()
        {
         { "Dim", TokenType.DIM },
         { "as", TokenType.AS },
         {"integer",TokenType.INTEGER },
         {"single",TokenType.LONG },
         {"double",TokenType.DOUBLE },
         { "select", TokenType.SELECT },
         { "case", TokenType.CASE },
         { "else", TokenType.ELSE },
         { "end", TokenType.END },
         { "to", TokenType.TO }
          
        };

        static TokenType[] Delimiters = new TokenType[]
        {
            TokenType.MULTIPLICATION, TokenType.PLUS, TokenType.MINUS,
        TokenType.EQUAL, TokenType.RBRACKET, TokenType.LBRACKET, TokenType.DIVISION,TokenType.COMMA
        };

        private static bool IsDelimiter(Token token)
        {
            return Delimiters.Contains(token.Type);
        }

        public static bool IsSpecialWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return SpecialWords.ContainsKey(word);
        }
        public static Dictionary<char, TokenType> SpecialSymbols = new Dictionary<char, TokenType>()
        {
          { '(', TokenType.LBRACKET },
          { ')', TokenType.RBRACKET },
          { '+', TokenType.PLUS },
          { '-', TokenType.MINUS },
          { '=', TokenType.EQUAL },
          { '*', TokenType.MULTIPLICATION},
          { '/',  TokenType.DIVISION },
          { ',',  TokenType.COMMA },
          { '#', TokenType.DELEMITOR },
          { '\n', TokenType.ENTER }
        };
        public static bool IsSpecialSymbol(char ch)
        {
            return SpecialSymbols.ContainsKey(ch);
        }
    }
}
