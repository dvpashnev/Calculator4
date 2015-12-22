using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Calculator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private StringBuilder _argument = new StringBuilder();

    private string _operations =  "/+-*";
    DispatcherTimer _timer;
    const int _interval = 25;

    private bool _rezultShow; //Shows that rezult is in TextBox. Only the "Clear" operation is possible
    
    public MainWindow()
    {
      InitializeComponent();
      _timer = new DispatcherTimer();
      _timer.Tick += timer_Tick;
      _timer.Interval = new TimeSpan(0, 0, _interval);
      //timer.Start();

      _rezultShow = false;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      Button_Click_Clear(null, null);
      //timer.Stop();
    }



    private void Button_Click_Digit(object sender, RoutedEventArgs e)
    {
      _timer.Interval = new TimeSpan(0, 0, _interval);
      _timer.Start();

      if (_rezultShow) return;

      if ((sender as Button) != null)
      {
          _argument.Append((sender as Button).Content);
          if (Verification.Check(_argument.ToString()))
          {
              Digits.Text += (sender as Button).Content;
          }
      }
    }

    private void Button_Click_Operation(object sender, RoutedEventArgs e)
    {
      _timer.Interval = new TimeSpan(0, 0, _interval);
      _timer.Start();

      if (_rezultShow) return;

      if (Digits.Text.Length > 0)
      {
        if (!_operations.Contains(Digits.Text.Substring(Digits.Text.Length - 1, 1)))
        {
          Digits.Text += (sender as Button).Content;
        }
      }
      
      _argument.Clear();
    }

    private void Button_Click_Is(object sender, RoutedEventArgs e)
    {
      _timer.Interval = new TimeSpan(0, 0, _interval);
      _timer.Start();

      if (_rezultShow) return;

      _argument.Clear();
      List<object> listObj = InputParser.ParseString(Digits.Text);
      Calculator calc = new Calculator();
      Digits.Text = calc.Calculate(listObj).ToString();

      _rezultShow = true;
    }

    private void Button_Click_BackSpace(object sender, RoutedEventArgs e)
    {
      _timer.Interval = new TimeSpan(0, 0, _interval);
      _timer.Start();

      if (_rezultShow) return;

      if (Digits.Text.Length > 0)
      {
        Digits.Text = Digits.Text.Substring(0, Digits.Text.Length - 1);
      }
    }

    private void Button_Click_Clear(object sender, RoutedEventArgs e)
    {
      Digits.Clear();
      _argument.Clear();

      if (_rezultShow) _rezultShow = false;

      _timer.Stop();
    }

    private void Digits_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
  }
}
