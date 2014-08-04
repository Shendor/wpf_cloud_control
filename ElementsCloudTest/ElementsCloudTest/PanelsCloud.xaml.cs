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
    /// Interaction logic for PanelsCloud.xaml
    /// </summary>
    public partial class PanelsCloud : Window
    {
        public PanelsCloud()
        {
            InitializeComponent();
            cloud.Run();
        }

        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                TextBox text = e.OriginalSource as TextBox;

                switch (text.Name)
                {
                    case "xPoint": cloud.RotateDirection = new Point(Double.Parse(xPoint.Text), cloud.RotateDirection.Y); break;
                    case "yPoint": cloud.RotateDirection = new Point(cloud.RotateDirection.X, Double.Parse(yPoint.Text)); break;
                    case "scale": cloud.ScaleRatio = Double.Parse(scale.Text); break;
                    case "opacity": cloud.OpacityRatio = Double.Parse(opacity.Text); break;
                }
            }
            catch { }
        }


        private void RadioButtons_Click(object sender, RoutedEventArgs e)
        {
            RadioButton cb = e.OriginalSource as RadioButton;

            switch (cb.Name)
            {
                case "isManual": cloud.RotationType = ElementsCloud.RotationType.Manual ; break;
                case "isMouse": cloud.RotationType = ElementsCloud.RotationType.Mouse; break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;

            switch (btn.Name)
            {
                case "run": cloud.Run(); break;
                case "stop": cloud.Stop(); break;
            }
        }
    }
}
