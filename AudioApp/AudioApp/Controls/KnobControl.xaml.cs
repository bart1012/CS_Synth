using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            Point mousePos = e.GetPosition(_grid);

            double normalizedX = Math.Clamp(mousePos.X, 0, 100);
            double normalizedY = Math.Clamp(100 - mousePos.Y, 0, 100); // Invert Y (up = increase)

            // Combine X and Y influence
            AmountProgressBar.Value = Math.Clamp((normalizedX + normalizedY) / 2, 0, 100);


        }

        private void _innerCaption_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragged = false;
            var textBlock = sender as TextBlock;

        }
    }
}
