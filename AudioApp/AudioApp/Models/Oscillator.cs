using NAudio.Wave.SampleProviders;
using System.Windows;

namespace AudioApp.Models
{
    public class Oscillator : DependencyObject
    {

        public static readonly DependencyProperty GainProperty =
            DependencyProperty.Register(nameof(Gain), typeof(double), typeof(Oscillator),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGainChanged));

        public double Gain
        {
            get => (double)GetValue(GainProperty);
            set => SetValue(GainProperty, value);
        }

        public static readonly DependencyProperty WaveformProperty =
            DependencyProperty.Register(nameof(Waveform), typeof(SignalGeneratorType), typeof(Oscillator),
                new FrameworkPropertyMetadata(SignalGeneratorType.Sin, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnWaveformChanged));

        public SignalGeneratorType Waveform
        {
            get => (SignalGeneratorType)GetValue(WaveformProperty);
            set => SetValue(WaveformProperty, value);
        }

        public static readonly DependencyProperty OctaveProperty =
              DependencyProperty.Register(nameof(Octave), typeof(int), typeof(Oscillator),
                  new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnOctaveChanged));

        public int Octave
        {
            get => (int)GetValue(OctaveProperty);
            set => SetValue(GainProperty, value);
        }


        public static readonly DependencyProperty ShapeProperty =
              DependencyProperty.Register(nameof(Shape), typeof(double), typeof(Oscillator),
                  new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnShapeChanged));

        public double Shape
        {
            get => (double)GetValue(ShapeProperty);
            set => SetValue(ShapeProperty, value);
        }


        public static readonly DependencyProperty DetuneProperty =
              DependencyProperty.Register(nameof(Detune), typeof(double), typeof(Oscillator),
                  new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDetuneChanged));

        public double Detune
        {
            get => (double)GetValue(DetuneProperty);
            set => SetValue(DetuneProperty, value);
        }




        private static void OnGainChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void OnWaveformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void OnOctaveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void OnShapeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void OnDetuneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }


    }
}
