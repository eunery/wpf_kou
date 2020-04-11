using System;
using System.Collections.Generic;
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

namespace ResultBoxes
{
    public partial class MainWindow : Window
    {
        int count = 0;
        Dictionary<TextBox, ComboBox> KeyByPair = new Dictionary<TextBox, ComboBox>();
        public MainWindow()
        {
            InitializeComponent();
            button.Click += button_Click;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            TextBox BoxNew = new TextBox()
            {
                Width = 60,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            ComboBox operations = CreateComboBox(new string[] { "+", "-", "*", "/" });
            KeyByPair.Add(BoxNew,operations);
            StackPanel Stack = new StackPanel()
            {
                Name = "mStack" + count,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            Button button = new Button()
            {
                Name = "button" + count,
                Content = "Delete",
            };
            count++;
            Stack.Children.Add(operations);
            Stack.Children.Add(BoxNew);
            Stack.Children.Add(button);
            mStack.Children.Add(Stack);
            operations.SelectionChanged += ChangeLabelText;
            BoxNew.TextChanged += ChangeLabelText;
        }
        private void ChangeLabelText(object sender, EventArgs e)
        {
            
            double result = 0;
            foreach (var item in mStack.Children)
            {
                if (item is StackPanel sp)
                    foreach (object obj in sp.Children)
                    {
                        if (obj is TextBox tb)
                        {
                            bool isDouble = double.TryParse(tb.Text, out double a);
                            if (isDouble)
                            {
                                ComboBox comboBox = KeyByPair[tb];
                                a = Convert.ToDouble(tb.Text);
                                result = Count(result, a, comboBox.Text);
                                errorLabel.Content = "";
                            }
                            else errorLabel.Content = "Ошибка";
                        }
                    }
            }
            Label.Content = "Result " + result;
        }
        private ComboBox CreateComboBox(string[] parametr)
        {
            ComboBox box = new ComboBox();
            foreach (string item in parametr)
            {
                ComboBoxItem cbItem = new ComboBoxItem
                {
                    Content = item
                };
                box.Items.Add(cbItem);
            }
            return box;
        }
        private double Count (double result, double a, string oper)
        {
            switch (oper)
            {
                case "+":
                    result += a;
                    break;
                case "-":
                    result -= a;
                    break;
                case "*":
                    result *= a;
                    break;
                case "/":
                    result /= a;
                    break;
            }
            return result;
        }
        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            Button DeleteButton = (Button)sender;
            string ButtonName = DeleteButton.Name.Substring(6);
            foreach  (object obj in mStack.Children)
            {
                if (obj is StackPanel stackpanel)
                    if (stackpanel.Name.Substring(6).Equals(ButtonName))
                    {
                        mStack.Children.Remove(stackpanel);
                        break;
                    }
            }
            ChangeLabelText(sender, e);
        }
    }

}