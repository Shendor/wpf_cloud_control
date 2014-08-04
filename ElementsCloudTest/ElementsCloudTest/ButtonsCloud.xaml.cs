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
    /// Interaction logic for ButtonsCloud.xaml
    /// </summary>
    public partial class ButtonsCloud : Window
    {
        public ButtonsCloud()
        {
            InitializeComponent();
            cloud.Run();
        }
    }
}
