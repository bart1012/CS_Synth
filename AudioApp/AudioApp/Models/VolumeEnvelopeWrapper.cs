using System.Windows;

namespace AudioApp.Models
{
    public class VolumeEnvelopeWrapper : DependencyObject
    {
        public static readonly DependencyProperty AttackProperty =
           DependencyProperty.Register(nameof(Attack), typeof(float), typeof(VolumeEnvelopeWrapper),
               new FrameworkPropertyMetadata(0.0f, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public float Attack
        {
            get => (float)GetValue(AttackProperty);
            set => SetValue(AttackProperty, value);
        }

        public static readonly DependencyProperty DecayProperty =
            DependencyProperty.Register(nameof(Decay), typeof(float), typeof(VolumeEnvelopeWrapper),
                new FrameworkPropertyMetadata(0.0f, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public float Decay
        {
            get => (float)GetValue(DecayProperty);
            set => SetValue(DecayProperty, value);
        }

        public static readonly DependencyProperty SustainProperty =
        DependencyProperty.Register(nameof(Sustain), typeof(float), typeof(VolumeEnvelopeWrapper),
            new FrameworkPropertyMetadata(0.0f, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public float Sustain
        {
            get => (float)GetValue(SustainProperty);
            set => SetValue(SustainProperty, value);
        }

        public static readonly DependencyProperty ReleaseProperty =
        DependencyProperty.Register(nameof(Release), typeof(float), typeof(VolumeEnvelopeWrapper),
            new FrameworkPropertyMetadata(0.0f, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public float Release
        {
            get => (float)GetValue(ReleaseProperty);
            set => SetValue(ReleaseProperty, value);
        }

    }
}
