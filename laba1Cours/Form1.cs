using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace laba1Cours
{
    public partial class Form1 : Form
    {
        Validate validate;
        public Form1()
        {
            validate = new Validate();
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            StreamReader reader = new StreamReader(path);
            string line = reader.ReadToEnd();
            textBox2.Text = line;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        List<string> list;
        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            Validate validate = new Validate();
            char n = '#';
            char[] Data = textBox2.Text.ToCharArray();
            list = new List<string>();
            bool p = true;
            for (int i = 0; i < Data.Length; i++)
            {
                if (validate.IsLetters(Data[i]) == p)
                {
                    string let = "";
                    while (validate.IsLetters(Data[i]) == p)
                    {
                        let += Data[i];
                        i++;
                    }
                    textBox3.Text += $"{let} - Идентификатор; \n";
                    list.Add($"{let} I");
                    textBox3.Text += Environment.NewLine;
                    if(let.Length > 8)
                    {
                        MessageBox.Show("Длина идентификатора превышает 8 символов. Проверьте свой код.");
                    }
                }
                if (validate.IsNumbers(Data[i]) == p)
                {
                    string num = "";
                    while (validate.IsNumbers(Data[i]) == p)
                    {
                        num += Data[i];
                        i++;
                    }
                     
                    textBox3.Text += $" {num} - Литерал; \n";
                    list.Add($"{num} L");
                    textBox3.Text += Environment.NewLine;
                }
                if (validate.IsSeparator(Data[i]) == p)
                {
                    if (Data[i] == '\n')
                    {
                        textBox3.Text += $" {n} - Конец строки; \n";
                        list.Add($"{Data[i]} #");
                        textBox3.Text += Environment.NewLine;
                    }
                    else
                    {
                        textBox3.Text += $" {Data[i]} - Разделитель; \n";
                        list.Add($"{Data[i]} R");
                        textBox3.Text += Environment.NewLine;
                    }
                }

            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        List<Token> tok = new List<Token>();
        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            Token token;
            string str;
            string type;
            //int ii = 0;
            for (int i = 0; i < list.Count; i++)
            {
                str = list[i].Split(' ')[0];
                type = list[i].Split(' ')[1];
                if (type == "I")
                {
                    if (Token.IsSpecialWord(str))
                    {
                        token = new Token(Token.SpecialWords[str]);
                        tok.Add(token);       
                        textBox4.Text += $" {token}";
                        textBox4.Text += Environment.NewLine;
                        continue;
                    }                   
                    else
                    {
                        token = new Token(Token.TokenType.IDENTIFIER);
                        token.Value = str;
                        tok.Add(token);
                        textBox4.Text += $" {token}";
                        textBox4.Text += Environment.NewLine;
                        continue;
                    }
                }
                else if (type == "L")
                {
                    token = new Token(Token.TokenType.NUMBER);
                    token.Value = str;
                    tok.Add(token);
                    textBox4.Text += $" {token}";
                    textBox4.Text += Environment.NewLine;
                    continue;
                }
                else if (type == "R")
                {
                    token = new Token(Token.SpecialSymbols[str[0]]);
                    token.Value=str;
                    tok.Add(token);
                    textBox4.Text += $" {token}";
                    textBox4.Text += Environment.NewLine;
                    continue;
                }
                else if (type == "#")
                {
                    token = new Token(Token.SpecialSymbols[str[0]]);
                    token.Value = str;
                    tok.Add(token);
                    textBox4.Text += $" {token}";
                    textBox4.Text += Environment.NewLine;
                    continue;
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Recognizer recognizer = new Recognizer(tok);
            ComplexExpression complexExpression = new ComplexExpression(tok);
            recognizer.Begin();

            int index = 0;
            listBox1.Items.Clear();
           listBox1.Items.Add(" Результат Действие       операнд1       операнд2   ");
            foreach(Three a in recognizer.tokens1)
            {
            listBox1.Items.Add($"M{index}                       {recognizer.Lexems(a.action)}                        {a.operand.Value}                      {a.operand1.Value} ");
                index++;
            }

            
            if (recognizer.Succes == true)
                MessageBox.Show("Анализ прошел успешно!");
            else
                MessageBox.Show("Анализ окончен!");
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
