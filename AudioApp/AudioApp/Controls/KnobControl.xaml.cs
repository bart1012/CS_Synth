using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace AudioApp.Controls
{
    /// <summary>
    /// Interaction logic for KnobControl.xaml
    /// </summary>
    public partial class KnobControl : UserControl
    {

        private bool _isDragged = false;
        private double _currentAngle = 320;
        private Point _lastMousePosition;

        public static readonly DependencyProperty LabelTextProperty =
           DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(KnobControl), new PropertyMetadata("Label"));

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }
        public KnobControl()
        {
            InitializeComponent();

        }

        private void _innerCaption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDragged = true;
            _lastMousePosition = e.GetPosition(Window.GetWindow(this));
            Mouse.Capture(sender as UIElement);
            Window.GetWindow(this).PreviewMouseUp += Window_PreviewMouseUp;
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragged = false;
            Mouse.Capture(null);
            Window.GetWindow(this).PreviewMouseUp -= Window_PreviewMouseUp;

        }


        private void _innerCaption_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragged) return;

            Point currentMousePos = e.GetPosition(Window.GetWindow(this));
            double mouseX = currentMousePos.X;


            bool movingRight = mouseX > _lastMousePosition.X;
            bool movingLeft = mouseX < _lastMousePosition.X;


            if (movingRight && _currentAngle < 580)
            {
                double amount = mouseX - _lastMousePosition.X;
                _currentAngle += amount; // Adjust increment as needed
            }
            else if (movingLeft && _currentAngle > 320)
            {
                double amount = mouseX - _lastMousePosition.X;
                _currentAngle += amount;
            }

            // Apply rotation transform
            RotateTransform rotateTransform = new RotateTransform(_currentAngle);
            rotateTransform.CenterX = InnerDialMarker.ActualWidth / 2;
            rotateTransform.CenterY = InnerDialMarker.ActualHeight / 2;
            InnerDialMarker.RenderTransform = rotateTransform;

            // Store current position for next move event
            _lastMousePosition = currentMousePos;


        }

    }
}
