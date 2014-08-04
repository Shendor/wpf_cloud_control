// WPF User Control - ElementsCloud
// Version 1.0
// Developed by ShenSoft Copyright (c) 2010

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ElementsCloud
{ 
    /// <summary>
    /// User Control - "ElementsCloud"
    /// </summary>
    public class ElementsCloud : Grid
    {
        private readonly RotateTransform3D rotateTransform;
        private bool isRunRotation;
        private double slowDownCounter;
        private List<ElementsCloudItem> elementsCollection;
        private RotationType rotationType;
        private Point rotateDirection;
        private Canvas canvas;

        private double scaleRatio;
        private double opacityRatio;

        public ElementsCloud()
        {
            this.Background = Brushes.Transparent;

            canvas = new Canvas(){
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
            };
            this.Children.Add(canvas);
            rotateTransform = new RotateTransform3D();

            SizeChanged += OnPageSizeChanged;

            rotationType = RotationType.Mouse;
            rotateDirection = new Point(100, 0);
            slowDownCounter = 500;
            elementsCollection = new List<ElementsCloudItem>();
            scaleRatio = 0.09;
            opacityRatio = 1.3;
        }

        #region Properties

        #region RotateDirectionProperty

        public static DependencyProperty RotateDirectionProperty =
            DependencyProperty.Register("RotateDirection", typeof(Point), typeof(ElementsCloud), new FrameworkPropertyMetadata(new Point(100, 0),
                                                                                                                                new PropertyChangedCallback(OnRotateDirectionChanged)));

        private static void OnRotateDirectionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ElementsCloud ElementsCloud = (ElementsCloud)sender;
            ElementsCloud.rotateDirection = (Point)e.NewValue;
        }
        //===================================================================================================================
        /// <summary>
        /// Defines the direction of rotation
        /// </summary>
        public Point RotateDirection
        {
            get { return (Point)GetValue(RotateDirectionProperty); }
            set
            {
                SetValue(RotateDirectionProperty, value);
                SetRotateTransform(value);
            }
        }

        #endregion

        #region ScaleRatioProperty

        public static DependencyProperty ScaleRatioProperty =
         DependencyProperty.Register("ScaleRatio", typeof(double), typeof(ElementsCloud), new FrameworkPropertyMetadata(0.09, new PropertyChangedCallback(OnScaleRatioChanged)));

        private static void OnScaleRatioChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ElementsCloud elementsCloud = (ElementsCloud)sender;
            elementsCloud.scaleRatio = (double)e.NewValue;
        }
        //===================================================================================================================
        /// <summary>
        /// Defines a scaling of ElementsCloudItems when they stays further than other elements
        /// </summary>
        public double ScaleRatio
        {
            get { return (double)GetValue(ScaleRatioProperty); }
            set { SetValue(ScaleRatioProperty, value); }
        }

        #endregion

        #region OpacityRatioProperty

        public static DependencyProperty OpacityRatioProperty =
            DependencyProperty.Register("OpacityRatio", typeof(double), typeof(ElementsCloud), new FrameworkPropertyMetadata(1.3, new PropertyChangedCallback(OnOpacityRatioChanged)));

        private static void OnOpacityRatioChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ElementsCloud elementsCloud = (ElementsCloud)sender;
            elementsCloud.opacityRatio = (double)e.NewValue;
        }
        //===================================================================================================================
        /// <summary>
        /// Defines a strengh of opacity when ElementsCloudItem stays behind other elements
        /// </summary>
        public double OpacityRatio
        {
            get { return (double)GetValue(OpacityRatioProperty); }
            set { SetValue(OpacityRatioProperty, value); }
        }

        #endregion

        #region Other properties
        //===================================================================================================================
        /// <summary>
        /// Allow to switch between manual or mouse rotation
        /// </summary>
        public RotationType RotationType
        {
            get { return rotationType; }
            set { rotationType = value; }
        }

        //===================================================================================================================
        /// <summary>
        /// Collection of elements
        /// </summary>
        public List<ElementsCloudItem> ElementsCollection
        {
            get { return elementsCollection; }
            set { elementsCollection = value; }
        }
        #endregion

        #endregion

        #region Methods
        //===================================================================================================================
        /// <summary>
        /// Stop rotation
        /// </summary>
        public void Stop()
        {
            if (isRunRotation == true)
            {
                CompositionTarget.Rendering -= OnCompositionTargetRendering;
                this.MouseEnter -= OnGridMouseEnter;
                this.MouseLeave -= OnGridMouseLeave;

                slowDownCounter = 500.0;
                isRunRotation = false;
                rotateTransform.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
            }

        }
        //===================================================================================================================
        /// <summary>
        /// Start rotation
        /// </summary>
        public void Run()
        {
            if (isRunRotation == false)
            {
                CompositionTarget.Rendering += OnCompositionTargetRendering;
                this.MouseEnter += OnGridMouseEnter;
                this.MouseLeave += OnGridMouseLeave;
                this.MouseMove += OnGridMouseMove;
                slowDownCounter = 500.0;
                isRunRotation = true;
                rotateTransform.Rotation = new AxisAngleRotation3D(new Vector3D(0.8, 0.6, 0), 0.5);

                SetRotateTransform(rotateDirection);
                RedrawElements();
            }
        }


        #region Private Methods
        //===================================================================================================================
        /// <summary>
        /// Configure rotate transformation
        /// </summary>
        ///<param name="position">Defines the direction of rotation</param>
        private void SetRotateTransform(Point position)
        {
            ElementsCloudItemSize size = GetElementsSize();

            double x = (position.X - size.XOffset) / size.XRadius;
            double y = (position.Y - size.YOffset) / size.YRadius;
            double angle = Math.Sqrt(x * x + y * y);
            rotateTransform.Rotation = new AxisAngleRotation3D(new Vector3D(-y, -x, 0.0), angle);
        }
        //===================================================================================================================
        /// <summary>
        /// Redraw all elements in Canvas
        /// </summary>
        private void RedrawElements()
        {
            canvas.Children.Clear();

            int length = elementsCollection.Count;
            for (int i = 0; i < length; i++)
            {
                double a = Math.Acos(-1.0 + (2.0 * i) / length);
                double d = Math.Sqrt(length * Math.PI) * a;
                double x = Math.Cos(d) * Math.Sin(a);
                double y = Math.Sin(d) * Math.Sin(a);
                double z = Math.Cos(a);

                elementsCollection[i].CenterPoint = new Point3D(x, y, z);
                canvas.Children.Add(elementsCollection[i]);
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Rotate blocks
        /// </summary>
        private void RotateBlocks()
        {
            ElementsCloudItemSize size = GetElementsSize();

            foreach (ElementsCloudItem ElementsCloudItem in elementsCollection)
            {
                Point3D point3D;
                if (rotateTransform.TryTransform(ElementsCloudItem.CenterPoint, out point3D))
                {
                    ElementsCloudItem.CenterPoint = point3D;
                    ElementsCloudItem.Redraw(size, scaleRatio, opacityRatio);
                }
            }
        }

        //===================================================================================================================
        /// <summary>
        /// Get new size for all elements depending of screen resolution
        /// </summary>
        private ElementsCloudItemSize GetElementsSize()
        {
            return new ElementsCloudItemSize
            {
                XOffset = canvas.ActualWidth / 2.0,
                YOffset = canvas.ActualHeight / 2.0
            };
        }
        #endregion

        #endregion

        #region Events
        //===================================================================================================================
        /// <summary>
        /// Redraw buttons with new size
        /// </summary>
        private void OnPageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (elementsCollection != null)
            {
                ElementsCloudItemSize size = GetElementsSize();

                foreach (ElementsCloudItem button in elementsCollection)
                {
                    button.Redraw(size, scaleRatio, opacityRatio);
                }
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Rendering
        /// </summary>
        private void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            if (!(isRunRotation || (slowDownCounter <= 0)))
            {
                AxisAngleRotation3D axis = (AxisAngleRotation3D)rotateTransform.Rotation;
                axis.Angle *= slowDownCounter / 500;
                rotateTransform.Rotation = axis;
                slowDownCounter--;
            }
            if (((AxisAngleRotation3D)rotateTransform.Rotation).Angle > 0.05)
            {
                RotateBlocks();
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Attach new event to grid when mouse enter to grid
        /// </summary>
        private void OnGridMouseEnter(object sender, MouseEventArgs e)
        {
            if (rotationType == RotationType.Mouse && isRunRotation == false)
            {
                this.MouseMove += OnGridMouseMove;
                isRunRotation = true;
                slowDownCounter = 500.0;
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Detach event when mouse leave grid
        /// </summary>
        private void OnGridMouseLeave(object sender, MouseEventArgs e)
        {
            if (rotationType == RotationType.Mouse && isRunRotation == true)
            {
                this.MouseMove -= OnGridMouseMove;
                isRunRotation = false;
                GC.Collect();
            }
        }
        //===================================================================================================================
        /// <summary>
        /// Move and rotate buttons when mouse position changed
        /// </summary>
        private void OnGridMouseMove(object sender, MouseEventArgs e)
        {
            if (rotationType == RotationType.Mouse) rotateDirection = e.GetPosition(canvas);
            SetRotateTransform(rotateDirection);
        }
        #endregion

    }
}
