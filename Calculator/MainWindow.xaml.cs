using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Calculator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private StringBuilder _argument = new StringBuilder();

    private string _operations = "/+-*";
    private DispatcherTimer _timer;
    private const int Interval = 25;

    private bool _rezultShow; //Shows that rezult is in TextBox. Only the "Clear" operation is possible

    public MainWindow()
    {
      InitializeComponent();
      _timer = new DispatcherTimer();
      _timer.Tick += timer_Tick;
      _timer.Interval = new TimeSpan(0, 0, Interval);

      _rezultShow = false;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      Button_Click_Clear(null, null);
    }

    private void TimerRestart()
    {
      _timer.Interval = new TimeSpan(0, 0, Interval);
      _timer.Start();
    }

    private void Button_Click_Digit(object sender, RoutedEventArgs e)
    {
      TimerRestart();

      if (_rezultShow) return;

      Button btn = sender as Button;

      if (btn != null)
      {
        string btnContent = btn.Content.ToString();

        _argument.Append(btnContent);

        if (Verification.Check(_argument.ToString()))
        {
          Digits.Text += btnContent;
        }
      }
    }

    private void Button_Click_Operation(object sender, RoutedEventArgs e)
    {
      TimerRestart();

      if (_rezultShow) return;

      Button btn = sender as Button;

      if (Digits.Text.Length > 0 && btn != null)
      {
        if (!_operations.Contains(Digits.Text.Substring(Digits.Text.Length - 1, 1)))
        {
          Digits.Text += btn.Content;
        }
      }

      _argument.Clear();
    }

    private void Button_Click_Is(object sender, RoutedEventArgs e)
    {
      TimerRestart();

      if (_rezultShow) return;

      List<object> listObj = InputParser.ParseString(Digits.Text);
      CalcProcessor calc = new CalcProcessor();

      //В случае деления на ноль выводится сообщение
      try
      {
        Digits.Text = calc.Calculate(listObj).ToString();
      }
      catch (DivideByZeroException ex)
      {
        Digits.Text = ex.Message;
      }

      _rezultShow = true;
    }

    private void Button_Click_BackSpace(object sender, RoutedEventArgs e)
    {
      TimerRestart();

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

      if (_rezultShow)
        _rezultShow = false;

      _timer.Stop();
    }
  }
}
