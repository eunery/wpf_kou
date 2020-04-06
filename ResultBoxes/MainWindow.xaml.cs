﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            button.Click += button_Click;
        }
        private int i = 50;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            TextBox BoxNew = new TextBox();
            BoxNew.HorizontalAlignment = HorizontalAlignment.Left;
            BoxNew.VerticalAlignment = VerticalAlignment.Top;
            BoxNew.Margin = new Thickness(35, i, 0, 0);
            gMain.Children.Add(BoxNew);
            i += 20;
            BoxNew.TextChanged += ChangeLabelText;
        }
        private void ChangeLabelText(object sender, EventArgs e)
        {
            
            double result = 0;
            foreach (var item in gMain.Children)
            {
                if (item is TextBox tb)
                {
                    bool isDouble = double.TryParse(tb.Text, out double a);
                    result += a;
                }
            }
            Label.Content = "Result " + result;
        }
    }

}
