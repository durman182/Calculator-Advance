using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator_Advance
{
    public class Resolve
    {

        public float Solve(string m_input)
        {
            
            char[] expr = m_input.ToCharArray();

            Stack<float> val = new Stack<float>();
            Stack<char> opr = new Stack<char>();

            for (int i = 0; i < expr.Length; i++)
            {
                if (expr[i] == ' ') continue;

                if (char.IsDigit(expr[i]))
                {
                    StringBuilder temp = new StringBuilder();
                    while (i < expr.Length && char.IsDigit(expr[i]))
                    {
                        temp.Append(expr[i]);
                        i++;
                    }
                    val.Push(float.Parse(temp.ToString()));
                }
                else if (expr[i] == '(')
                {
                    opr.Push(expr[i]);
                }
                else if (expr[i] == ')')
                {
                    while (opr.Peek() != '(')
                        val.Push(Calculate(opr.Pop(), val.Pop(), val.Pop()));
                    opr.Pop();
                }
                else if (expr[i] == '%' || expr[i] == '+' || expr[i] == '-' || expr[i] == '*' || expr[i] == '/' || expr[i] == '^' || expr[i] == '√')
                {
                    while (opr.Count > 0 && CheckPrecedence(expr[i], opr.Peek()))
                        val.Push(Calculate(opr.Pop(), val.Pop(), val.Pop()));
                    opr.Push(expr[i]);
                }
            }

            while (opr.Count > 0)
                val.Push(Calculate(opr.Pop(), val.Pop(), val.Pop()));

            return val.Pop();
        }

        public bool CheckPrecedence(char opr1, char opr2)
        {
            if (opr2 == '(' || opr2 == ')') return false;
            if ((opr1 == '^') && (opr2 == '*' || opr2 == '/' || opr2 == '+' || opr2 == '-' || opr2 == '%' || opr2 == '√')) return false;
            if ((opr1 == '√') && (opr2 == '*' || opr2 == '/' || opr2 == '+' || opr2 == '-' || opr2 == '%' || opr2 == '^')) return false;
            if ((opr1 == '*' || opr1 == '/') && (opr2 == '+' || opr2 == '-')) return false;
            return true;
        }

        public float Calculate(char op, float b, float a)
        {
            switch (op)
            {
                case '%': return a % b;
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/': if (b == 0) throw new InvalidOperationException("Cannot divide by zero"); return a / b;
                case '^': return (float)Math.Pow(b, a);
                case '√': return (float)Math.Pow(b, 1/a);

                default: return 0;
            }
           
        }
    }

}