using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace laba1Cours
{
    public struct Three
    {
        public Token operand;
        public Token operand1;
        public Token action;
        public Three(Token ac,Token oper2,Token oper1)
        {
            operand = oper1;
            operand1 = oper2;
            action = ac;
        }
        
    }
    public class ComplexExpression
    {
        int index = 0;
        public List<Three> threes= new List<Three> ();
        List<Token> tokens = new List<Token> ();
        Stack<Token> E = new Stack<Token>();
        Stack<Token> T = new Stack<Token>();
        int nextLexem = 0;

        public ComplexExpression(List<Token> x)
        {
            tokens = x;
        }

        public ComplexExpression(int index, List<Token> x)
        {
            tokens= x;
            this.index=index;
        }

        public int LastIndex { get { return index; } }
        private Token GetLexeme(int nextLexem)
        {
            return tokens [nextLexem];
        }

        private void Operand()
        {
            E.Push(tokens[nextLexem]);
            nextLexem++;
        }


        private void Action()
        {
            if (E.Count < 2)
                throw new Exception("Невозможно выполнить операцию. Число операндов не удовлетворяют условию");

            Three i = new Three(T.Pop(),E.Pop(),E.Pop());
            threes.Add(i);

            Token ii = new Token(Token.TokenType.IDENTIFIER);
            ii.Value = $"M{index}";
            E.Push(ii);
            index++;

        }

        private void End()
        {
         
            {
                switch(T.Peek().Type)
                {
                    case Token.TokenType.LBRACKET:
                        D5();
                        break;
                    case Token.TokenType.PLUS:
                        D4();
                        break;
                    case Token.TokenType.MINUS:
                        D4();
                        break;
                    case Token.TokenType.MULTIPLICATION:
                        D4();
                        break;
                    case Token.TokenType.DIVISION:
                        D4();
                        break;
                        default:
                        Mistake("+, -, *, /, ( , )");
                        break;
                }
            }
        }

        private void LeftBracket()
        {
            if (T.Count == 0)
            {
                D1();

            }
            else
            {
                switch (T.Peek().Type)
                {
                    case Token.TokenType.LBRACKET:
                        D1();
                        break;
                    case Token.TokenType.PLUS:
                        D1();
                        break;
                    case Token.TokenType.MINUS:
                        D1();
                        break;
                    case Token.TokenType.MULTIPLICATION:
                        D1();
                        break;
                    case Token.TokenType.DIVISION:
                        D1();
                        break;
                    default:
                        Mistake("+, -, *, /, ( , )");
                        break;
                }
            }
        }

        private void PlusMunis()
        {
            if (T.Count == 0)
            {
                D1();

            }
            else
            {
                switch (T.Peek().Type)
                {
                    case Token.TokenType.LBRACKET:
                        D1();
                        break;
                    case Token.TokenType.PLUS:
                        D2();
                        break;
                    case Token.TokenType.MINUS:
                        D2();
                        break;
                    case Token.TokenType.MULTIPLICATION:
                        D4();
                        break;
                    case Token.TokenType.DIVISION:
                        D4();
                        break;
                    default:
                        Mistake("+, -, *, /, ( , )");
                        break;
                }
            }
        }

        private void MultiplicationDivision()
        {
            if (T.Count == 0)
            {
                D1();
            }
            else
            {
                switch (T.Peek().Type)
                {
                    case Token.TokenType.LBRACKET:
                        D1();
                        break;
                    case Token.TokenType.PLUS:
                        D1();
                        break;
                    case Token.TokenType.MINUS:
                        D1();
                        break;
                    case Token.TokenType.MULTIPLICATION:
                        D2();
                        break;
                    case Token.TokenType.DIVISION:
                        D2();
                        break;
                    default:
                        Mistake("+, -, *, /, ( , )");
                        break;
                }
            }
        }

        private void RightBracket()
        {
            if (T.Count == 0)
            {
                D5();
            }
            else
            {
                switch (T.Peek().Type)
                {
                    case Token.TokenType.LBRACKET:
                        D3();
                        break;
                    case Token.TokenType.PLUS:
                        D4();
                        break;
                    case Token.TokenType.MINUS:
                        D4();
                        break;
                    case Token.TokenType.MULTIPLICATION:
                        D4();
                        break;
                    case Token.TokenType.DIVISION:
                        D4();
                        break;
                    default:
                        Mistake("+, -, *, /, ( , )");
                        break;
                }
            }
        }
        
        private void Mistake(string i)
        {
            if (tokens[nextLexem].Type == Token.TokenType.NETERMINAL)
                throw new Exception($"Ожидалось {i}, но получено {tokens[nextLexem].Value}");
        }
        private void D1()
        {
            T.Push(tokens[nextLexem]);
            nextLexem++;
        }
        private void D2()
        {
            Action();
            T.Push(tokens[nextLexem]);
            nextLexem++;
        }
        private void D3()
        {
            T.Pop();
            nextLexem++;
        }
        private void D4()
        {
            Action();
        }
        private void D5()
        {
            throw new Exception("Ошибка!");
        }

        public void Start()
        {
            if(nextLexem==tokens.Count)
            {
                if(T.Count==0)
                {
                    return;
                }
                else
                {
                    End();
                }
            }
            else
            {
                switch(GetLexeme(nextLexem).Type)
                {
                    case Token.TokenType.IDENTIFIER:Operand();break;

                    case Token.TokenType.NUMBER:Operand();break;

                    case Token.TokenType.PLUS:PlusMunis();break;

                    case Token.TokenType.MINUS:PlusMunis();break;

                    case Token.TokenType.MULTIPLICATION: MultiplicationDivision();break;

                    case Token.TokenType.DIVISION: MultiplicationDivision();break;

                    case Token.TokenType.LBRACKET: LeftBracket();break;

                    case Token.TokenType.RBRACKET: RightBracket();break;

                    default:Mistake("+,-,*,/,(,) , идентификатор или литерал");break;
                }
            }
            Start();
        }
    }
}
