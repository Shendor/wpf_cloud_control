// ElementsCloudItem is children element of ElementsCloud user control. Enter at the ElementsCollection of ElementsCloud
// Version 1.0
// Developed by Shendor Copyright (c) 2011
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ElementsCloud
{
    /// <summary>
    /// ElementsCloudItem represent a grid whcih is able to contain any UIElements objects
    /// </summary>
    public class ElementsCloudItem : System.Windows.Controls.Grid
    {
        private ScaleTransform itemScaling;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point3D">Center point of the object</param>
        /// <param name="element">Object whcih will be added to Children collection and be shown at a window</param>
        public ElementsCloudItem(Point3D point3D, UIElement element)
        {
            CenterPoint = point3D;
            itemScaling = new ScaleTransform();
            this.LayoutTransform = itemScaling;
            Children.Add(element);
            itemScaling.CenterX = CenterPoint.X;
            itemScaling.CenterY = CenterPoint.Y;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ElementsCloudItem()
        {
            CenterPoint = new Point3D();
            itemScaling = new ScaleTransform();
            this.LayoutTransform = itemScaling;
            itemScaling.CenterX = CenterPoint.X;
            itemScaling.CenterY = CenterPoint.Y;
        }

        #region CenterPoint property

        public static DependencyProperty CenterPointProperty =
            DependencyProperty.Register("CenterPoint", typeof(Point3D), typeof(ElementsCloudItem), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCenterPointChanged)));

        private static void OnCenterPointChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ElementsCloudItem elementsCloudItem = (ElementsCloudItem)sender;
            elementsCloudItem.CenterPoint = (Point3D)e.NewValue;
        }
        //================================================================================================================
        /// <summary>
        /// Center point of the item
        /// </summary>
        public Point3D CenterPoint
        {
            get { return (Point3D)GetValue(CenterPointProperty); }
            set { SetValue(CenterPointProperty, value); }
        }

        #endregion

        //================================================================================================================
        /// <summary>
        /// Update options of the item and redraw it
        /// </summary>
        public void Redraw(ElementsCloudItemSize size, double scaleRatio, double opacityRatio)
        {
            //UpdateLayout();
            itemScaling.ScaleX = itemScaling.ScaleY = Math.Abs((16 + CenterPoint.Z * 4) * scaleRatio);
            Opacity = CenterPoint.Z + opacityRatio;

            Canvas.SetLeft(this, (size.XOffset + CenterPoint.X * size.XRadius) - (ActualWidth / 2.0));
            Canvas.SetTop(this, (size.YOffset - CenterPoint.Y * size.YRadius) - (ActualHeight / 2.0));
            Canvas.SetZIndex(this, (int)(CenterPoint.Z * Math.Min(size.XRadius, size.YRadius)));
        }

    }
}
