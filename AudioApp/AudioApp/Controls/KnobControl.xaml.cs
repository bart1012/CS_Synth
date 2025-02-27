using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public static readonly DependencyProperty CaptionProperty =
           DependencyProperty.Register(nameof(Caption), typeof(string), typeof(KnobControl), new PropertyMetadata("Tremolo"));

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }
        public KnobControl()
        {
            InitializeComponent();
        }

        private void _innerCaption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            textBlock.Text = $"{_amount}";
            _isDragged = true;
        }

        private void _innerCaption_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragged) return;
            var textBlock = sender as TextBlock;
            Point position = Mouse.GetPosition(_ellipseOuterDial);
            _amount = Math.Clamp((int)position.X, 0, 100);
            textBlock.Text = $"{_amount}";
        }

        private void _innerCaption_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragged = false;
            var textBlock = sender as TextBlock;
            textBlock.Text = Caption;
        }
    }
}
