using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Caculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OperatorSelected _operatorSelected;
        private double _lastNumber, _currentNumber;


        public MainWindow()
        {
            InitializeComponent();
            ResultDisplay.Text = "0";
        }


        private void OperatorInput_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.Assert(sender != null, nameof(sender) + " != null");
            var input = ((Button) sender).Content.ToString();
            _lastNumber = double.Parse(ResultDisplay.Text);

            ResultDisplay.Text = input;

            switch (input)
            {
                case "+":
                    _operatorSelected = OperatorSelected.Addition;
                    break;
                case "-":
                    _operatorSelected = OperatorSelected.Substaction;
                    break;
                case "*":
                    _operatorSelected = OperatorSelected.Multilplication;
                    break;
                case "/":
                    _operatorSelected = OperatorSelected.Division;
                    break;
            }
        }

        private void NumberInput_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.Assert(sender != null, nameof(sender) + " != null");
            var input = ((Button) sender).Content.ToString();

            if (ResultDisplay.Text == "0" || ResultDisplay.Text == "+" || ResultDisplay.Text == "-" ||
                ResultDisplay.Text == "*" || ResultDisplay.Text == "/")
            {
                ResultDisplay.Text = input;
                return;
            }

            ResultDisplay.Text = $"{ResultDisplay.Text}{input}";
        }

        private void Equal_OnClick(object sender, RoutedEventArgs e)
        {
            _currentNumber = double.Parse(ResultDisplay.Text);

            ResultDisplay.Text = Calculating();
        }



        private string Calculating()
        {
            switch (_operatorSelected)
            {
                case OperatorSelected.Addition:

                    return SimpleMath.Addition(_currentNumber, _lastNumber).ToString(CultureInfo.InvariantCulture);

                case OperatorSelected.Substaction:

                    return SimpleMath.Subtraction(_lastNumber, _currentNumber).ToString(CultureInfo.InvariantCulture);

                case OperatorSelected.Multilplication:

                    return SimpleMath.Multiplication(_currentNumber, _lastNumber).ToString(CultureInfo.InvariantCulture);

                case OperatorSelected.Division:

                    return SimpleMath.Division(_lastNumber, _currentNumber).ToString(CultureInfo.InvariantCulture);
                default:
                    return "";

            }
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "0";
        }
    }

    public class SimpleMath
    {
        public static double Addition(double a, double b)
        {
            return a + b;
        }

        public static double Subtraction(double a, double b)
        {
            return a - b;
        }

        public static double Multiplication(double a, double b)
        {
            return a * b;
        }

        public static double Division(double a, double b)
        {
            return a / b;
        }
    }

    public enum OperatorSelected
    {
        Addition,
        Substaction,
        Multilplication,
        Division
    }
}