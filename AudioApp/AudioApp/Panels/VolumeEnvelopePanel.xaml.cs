using AudioApp.Models;
using System.Windows.Controls;

namespace AudioApp.Panels
{
    /// <summary>
    /// Interaction logic for VolumeEnvelopePanel.xaml
    /// </summary>
    /// 

    public partial class VolumeEnvelopePanel : UserControl
    {

        public VolumeEnvelopeWrapper VolumeEnvelope;
        public VolumeEnvelopePanel()
        {
            InitializeComponent();
            VolumeEnvelope = new();
            DataContext = VolumeEnvelope;
        }
    }
}
