using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElementsCloudTest
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonsCloud bc = new ButtonsCloud();
            bc.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PanelsCloud pc = new PanelsCloud();
            pc.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextCloud tc = new TextCloud();
            tc.Show();
        }
    }
}
