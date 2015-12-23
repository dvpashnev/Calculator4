using System;
using System.Collections.Generic;

namespace Calculator
{
  public delegate float binaryOperation(float a, float b);

  public class CalcProcessor
  {
    private binaryOperation Operation;

    private void SetOperation(Operations operation)
    {
      switch (operation)
      {
        case Operations.Plus:
          Operation = (a, b) => a + b;
          break;
        case Operations.Minus:
          Operation = (a, b) => a - b;
          break;
        case Operations.Multiple:
          Operation = (a, b) => a*b;
          break;
        case Operations.Divide:
          Operation = (a, b) =>
          {
            if (b == 0)
              throw new DivideByZeroException("Делить на ноль нельзя");
            return (float) a/b;
          };
          break;
      }
    }

    private Operations ObjToOper(object ch)
    {
      switch (ch.ToString())
      {
        case "+":
          return Operations.Plus;

        case "-":
          return Operations.Minus;

        case "/":
          return Operations.Divide;

        case "*":
          return Operations.Multiple;

        default:
          throw new ArgumentOutOfRangeException("Внутренняя ошибка " + ch.ToString());
      }
    }

    public float Calculate(List<object> operands)
    {
      float result = Single.Parse(operands[0].ToString());

      for (int i = 1; i < operands.Count; i += 2)
      {
        var convertedOperator = ObjToOper(operands[i]);
        SetOperation(convertedOperator);
        result = Operation(result, Single.Parse(operands[i + 1].ToString()));
      }
      return result;
    }
  }
}
