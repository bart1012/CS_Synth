using NAudio.Wave.SampleProviders;
using System.Windows.Input;

namespace AudioApp.Models
{
    public class MappedSignalGenerator : SignalGenerator
    {
        public Key Key { get; set; }

        public MappedSignalGenerator(Key e, SignalGeneratorType type)
        {
            Key = e;
            Frequency = e switch
            {
                Key.A => 261.63f,  // Middle C (C4)
                Key.W => 277.18f,  // C#4
                Key.S => 293.66f,  // D4
                Key.E => 311.13f,  // D#4
                Key.D => 329.63f,  // E4
                Key.F => 349.23f,  // F4
                Key.T => 369.99f,  // F#4
                Key.G => 392.00f,  // G4
                Key.Y => 415.30f,  // G#4
                Key.H => 440.00f,  // A4 (A4 is often used as the tuning standard)
                Key.U => 466.16f,  // A#4
                Key.J => 493.88f,  // B4
                Key.K => 523.25f,  // C5
                _ => 261.63f // C4
            };
            Type = type;

        }


    }
}
