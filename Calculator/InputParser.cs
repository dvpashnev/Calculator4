using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator
{
  public class InputParser
  {
    static public List<object> ParseString(string expression)
    {
      List<object> listObj = new List<object>();
      //string inpupString = "int a, b; char c; a:=b+a; c := 'x'; ";
      //Regex regex = new Regex(@"(\d+)([-+*/])");
      Regex regexNum = new Regex(@"(\d+)");
      Regex regexOper = new Regex(@"([-+*/])");
      var matchesNum = regexNum.Matches(expression);
      var matchesOper = regexOper.Matches(expression);

      for (int i = 0; i < matchesNum.Count; i++)
      {
        listObj.Add(matchesNum[i].Value);
        if (i < matchesNum.Count - 1)
          listObj.Add(matchesOper[i].Value);
      }
      return listObj;
    }
  }
}
