using AudioApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace AudioApp.Controls
{
    public partial class PianoKeyboardControl : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<PianoKey> _pianoKeys;
        public ObservableCollection<PianoKey> PianoKeys
        {
            get => _pianoKeys;
            set
            {
                _pianoKeys = value;
                OnPropertyChanged(nameof(PianoKeys)); // Notify UI
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public PianoKeyboardControl()
        {
            InitializeComponent();
            DataContext = this; // Important to bind properties in XAML
            PianoKeys = GenerateKeyList(); // Now UI will update
        }

        private ObservableCollection<PianoKey> GenerateKeyList()
        {
            var keyList = new ObservableCollection<PianoKey>();
            int octaves = 7;
            int index = 1;
            string[] notes = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];
            for (int i = 1; i <= octaves; i++)
            {
                for (int j = 0; j < notes.Length; j++)
                {
                    bool isBlack = notes[j].Contains('#');
                    var note = new PianoKey { Note = notes[j], IsBlack = isBlack, Index = index, Octave = i };
                    //Console.WriteLine(note);
                    keyList.Add(note);
                    index++;
                }
            }
            return keyList;
        }
    }
}
