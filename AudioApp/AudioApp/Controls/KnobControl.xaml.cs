using System.Windows;
using System.Windows.Controls;
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
        private int _amount = 0;
        public int Amount => _amount;
        private bool _isDragged = false;

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
        }

        private void _innerCaption_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragged) return;

            (double centerX, double centerY) = (75, 75); // Center of the circular path
            double radiusX = 45, radiusY = 45; // Ellipse radius for X and Y

            Point mousePos = e.GetPosition(_grid);

            // Convert mouse position to an angle relative to the center
            double angle = Math.Atan2(mousePos.Y - centerY, mousePos.X - centerX);
            if (angle < 0) angle += 2 * Math.PI; // Normalize to [0, 2π]

            // Define arc start and end angles
            double startAngle = Math.PI; // 180 degrees (leftmost)
            double endAngle = 2 * Math.PI; // 360 degrees (rightmost)

            // Map the angle to progress (0% to 100%)
            double progress = (angle - startAngle) / (endAngle - startAngle);
            progress = Math.Clamp(progress, 0, 1);

            // Compute the new endpoint for the progress arc
            double newX = centerX + radiusX * Math.Cos(startAngle + progress * (endAngle - startAngle));
            double newY = centerY + radiusY * Math.Sin(startAngle + progress * (endAngle - startAngle));

            // Update the fill arc's end point
            if (FillArcPath.Data is PathGeometry pathGeometry &&
                pathGeometry.Figures[0].Segments[0] is ArcSegment arcSegment)
            {
                arcSegment.Point = new Point(newX, newY);
                arcSegment.IsLargeArc = progress > 0.5; // Ensures correct rendering
            }

            Console.WriteLine($"Progress: {progress * 100:F1}%"); // Debugging output

        }

        private void _innerCaption_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragged = false;
            var textBlock = sender as TextBlock;

        }
    }
}
