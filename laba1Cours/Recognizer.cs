using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace laba1Cours
{  
    public class Recognizer
    {

        Token token;
        List<Token> tok1 = new List<Token>();
        public bool Succes;
        int i;
        public List<Three> tokens1 = new List<Three>();
        public Recognizer(List<Token> listtok)
        {
            tok1 = listtok;
        }
        public void Begin()
        {
            i = -1;
            try
            {
              Programm();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Код неверный!", ex.Message);
            }
        }
       
        public  string Lexems(Token token)
        {
            string s="";
            switch(token.Type)
            {
                case Token.TokenType.PLUS:
                    s += "+";
                    break;
                case Token.TokenType.MULTIPLICATION:
                    s += "*";
                    break;
                case Token.TokenType.DIVISION:
                    s += "/";
                    break;
                case Token.TokenType.MINUS:
                    s += "-";
                    break;
            }
            return s;
        }
        public void Programm()// программа
        {
            Succes = false;
            Next();
            if (token.Type != Token.TokenType.DIM)
            {
                Expression(Token.TokenType.DIM, token.Type);
            }
            Next();
            ListOfDescriptions();
            ListOfOperatoins();
            Succes = true;
        }

        public void ListOfDescriptions()// спис_опис
        {
            Descriptions();           
            AddDescriptions(); 
        }

        public void ListOfOperatoins()// спис_опер
        {
            Operator();
            Z();
        }

        public void Descriptions()// опис
        {
            ListOfVariables();         
            Type();
        }
        public void AddDescriptions()// доп_опер
        {
            switch (token.Type)
            {
                case Token.TokenType.ENTER:
                    Next();
                    break;
                case Token.TokenType.IDENTIFIER:
                    ListOfDescriptions();                   
                    break;
                default:
                    Expression(Token.TokenType.ENTER, token.Type);
                    break;
            }
        }
        public void ListOfVariables()// спис_перем
        {
            if (token.Type != Token.TokenType.IDENTIFIER)
            {
                Expression(Token.TokenType.IDENTIFIER, token.Type);
            }
            Next();
            U();
        }
        public void Type()// тип
        {
            if (token.Type != Token.TokenType.INTEGER &&token.Type != Token.TokenType.LONG && token.Type != Token.TokenType.DOUBLE)
            {
                Expression(Token.TokenType.INTEGER, token.Type);
            }
            Next();
        }
        public void U()// U
        {
            switch (token.Type)
            {
                case Token.TokenType.AS:
                    Next();
                    break;
                case Token.TokenType.COMMA:
                    Next();
                    ListOfVariables();
                    break;
                default:
                    Expression(Token.TokenType.AS, token.Type);                   
                    break;
            }
        }
        public void Operator()// опер
        {
            if(token.Type==Token.TokenType.ENTER)
            {
                Next();
            }
            switch(token.Type)
            {
                case Token.TokenType.SELECT:
                    Conditional();
                    break;
                case Token.TokenType.IDENTIFIER:                    
                    Assignment();                  
                    break;                    
                default:
                    Expression(Token.TokenType.SELECT, token.Type);
                    break;
            }
        }
        public void Conditional()// условн
        {
            if (token.Type != Token.TokenType.SELECT)
            {
                Expression(Token.TokenType.SELECT, token.Type);
            }
            Next();
            if (token.Type != Token.TokenType.CASE)
            {
                Expression(Token.TokenType.CASE, token.Type);
            }
            Next();
            if (token.Type != Token.TokenType.IDENTIFIER)
            {
                Expression(Token.TokenType.IDENTIFIER, token.Type);
            }
            Next();
            if (token.Type != Token.TokenType.ENTER)
            {
                Expression(Token.TokenType.ENTER, token.Type);
            }
            Next();
            if (token.Type != Token.TokenType.CASE)
            {
                Expression(Token.TokenType.CASE, token.Type);
            }
            Next();
            if (token.Type != Token.TokenType.NUMBER)
            {
                Expression(Token.TokenType.NUMBER, token.Type);
            }

            Next();
            W();
                       
        }
        public void Assignment()// присв
        {
            
            if (token.Type != Token.TokenType.IDENTIFIER )
            {
                Expression(Token.TokenType.IDENTIFIER, token.Type);
            }
            Next();
            if (token.Type != Token.TokenType.EQUAL)
            {
                Expression(Token.TokenType.EQUAL, token.Type);
            }
            Next();
            if(token.Type == Token.TokenType.IDENTIFIER || token.Type == Token.TokenType.NUMBER)
            {
                List<Token> tokens = new List<Token>();
                while (token.Type != Token.TokenType.ENTER)
                {
                    tokens.Add(token);
                    Next();
                }
                Skip(tokens);
                
                Next();
            }
            else
            {
                Expression(Token.TokenType.IDENTIFIER, token.Type);
            }         
        }
        public void Z()// Z
        {
            switch (token.Type)
            {
                case Token.TokenType.ELSE:                  
                    break;
                case Token.TokenType.CASE:
                    Option();                    
                    if(token.Type != Token.TokenType.NUMBER && token.Type != Token.TokenType.IDENTIFIER 
                        && token.Type != Token.TokenType.TO && token.Type != Token.TokenType.ELSE && token.Type != Token.TokenType.END)
                    {
                        Expression(Token.TokenType.NUMBER, token.Type);
                    }
                    Next();
                    if(token.Type==Token.TokenType.TO)
                    {
                        Next();
                        if(token.Type != Token.TokenType.NUMBER && token.Type != Token.TokenType.IDENTIFIER)
                        {
                            Expression(Token.TokenType.NUMBER, token.Type);
                        }
                        Next();
                        if(token.Type != Token.TokenType.ENTER)
                        {
                            Expression(Token.TokenType.NUMBER, token.Type);
                        }
                        Next();
                    }
                    if(token.Type == Token.TokenType.ELSE)
                    {
                        Next();
                    }
                    ListOfOperatoins();                                     
                break;
                case Token.TokenType.ENTER:
                    Next();
                    break;
                case Token.TokenType.SELECT:
                    ListOfOperatoins();
                    break;
                case Token.TokenType.IDENTIFIER:
                    ListOfOperatoins();
                    break;
                case Token.TokenType.END:
                    Next();
                    if(token.Type == Token.TokenType.SELECT)
                    {
                        Next();
                    }
                    else
                    {
                        Expression(Token.TokenType.SELECT, token.Type);
                    }
                    break;
                default:
                    Expression(Token.TokenType.ENTER, token.Type);
                    break;
            }           
        }
        public void W()// W
        {
            switch (token.Type)
            {
                case Token.TokenType.ENTER:
                    ListOfOperatoins();
                    break;
                case Token.TokenType.TO:
                    Next();
                    ListOfOperatoins();
                    break;
                default:
                    Expression(Token.TokenType.ENTER, token.Type);
                    break;
            }    
        }
        public void Option()// вариант
        {
            if (token.Type != Token.TokenType.CASE)
            {
                Expression(Token.TokenType.CASE, token.Type);
            }
            Next();
        }

       
        public void X()// X
        {
            switch (token.Type)
            {
                case Token.TokenType.END:
                    Next();
                    break;
                case Token.TokenType.CASE:
                    Next();
                    break;
                default:
                    Expression(Token.TokenType.CASE, token.Type);
                    break;
            }
        }
        public void S()// S
        {
            switch (token.Type)
            {
                case Token.TokenType.NUMBER:
                    W();
                    break;
                case Token.TokenType.ELSE:
                    Next();
                    break;
                default:
                    Expression(Token.TokenType.NUMBER, token.Type);
                    break;
            }
        }

        public void Sign()
        {
            if (token.Type != Token.TokenType.PLUS &&
            token.Type != Token.TokenType.MINUS &&
            token.Type != Token.TokenType.MULTIPLICATION &&
            token.Type != Token.TokenType.DIVISION)
                Expression(Token.TokenType.PLUS, token.Type);          
            Next();
        }
        public string Expression(Token.TokenType type, Token.TokenType type1)
        {           
            throw new Exception($"Ожидалось {type}, но  было получено {type1} {i} ");             
        }
        public void Next()
        {
            i++;         
            if(i<tok1.Count)
            {
                token = tok1[i];
            }
        }
            bool flag=true;
             int LastIndex = 0;

        public void Skip(List<Token> tokens)
        {
            if(flag=true)
            {
            ComplexExpression complexExpression = new ComplexExpression(tokens);
            complexExpression.Start();

                LastIndex = complexExpression.LastIndex;
            foreach (Three three in complexExpression.threes)
                tokens1.Add(three);
            }
            else
            {
                ComplexExpression complexExpression = new ComplexExpression(LastIndex,tokens);
                complexExpression.Start();
                LastIndex = complexExpression.LastIndex;
                foreach (Three three in complexExpression.threes)
                    tokens1.Add(three);
            }


        }
    }
}
