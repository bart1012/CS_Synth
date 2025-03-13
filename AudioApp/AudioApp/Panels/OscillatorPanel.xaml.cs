namespace AudioApp.Panels
{
    /// <summary>
    /// Interaction logic for OscillatorPanel.xaml
    /// </summary>
    public partial class OscillatorPanel : System.Windows.Controls.UserControl
    {
        private double _gain { get; set; }
        public Action<double> GainChanged { get; set; }
        public double Gain
        {
            get => _gain;
            set
            {
                _gain = value;
                GainChanged?.Invoke(value);
            }
        }


        public OscillatorPanel()
        {
            InitializeComponent();
            GainControl.AmountChanged += (newAmount) =>
            {
                Gain = newAmount;
            };
        }



    }
}
