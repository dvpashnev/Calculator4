using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Calculator
{
    public delegate float binaryOperation(int a, int b);
    public class Calculator
    {
        binaryOperation Operation;
        void SetOperation(Operations operation)
        {
            switch (operation)
            {
                case Operations.Plus:
                    Operation = plus;
                    break;
                case Operations.Minus:
                    Operation = minus;
                    break;
                case Operations.Multiple:
                    Operation = multiple;
                    break;
                case Operations.Divide:
                    Operation = divide;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        string ObjToOper(object ch)
      {

        switch (ch.ToString())
        {
          case "+" :
            return "Plus";
          case "-":
            return "Minus";
          case "/":
            return "Divide";
        }
          return "Multiple";
      }

        public float Calculate(List<object> operands)
        {
            float result = Int32.Parse(operands[0].ToString());

            for(int i = 1; i < operands.Count; i +=2)
            {
              string str = ObjToOper(operands[i]);
                SetOperation((Operations)Enum.Parse(typeof(Operations), str));
                result = Operation((int)result, Int32.Parse(operands[i + 1].ToString()));
            }
            return result;
        }

        float plus(int a, int b)
        {
            return a + b;
        }

        float minus(int a, int b)
        {
            return a - b;
        }
        float multiple(int a, int b)
        {
            return a * b;
        }

        float divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException();
            return (float)a / b;
        }
    }
}
