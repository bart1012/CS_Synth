using AudioApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AudioApp.Controls
{
    /// <summary>
    /// Interaction logic for PianoKeyboardControl.xaml
    /// </summary>
    public partial class PianoKeyboardControl : UserControl
    {
        public ObservableCollection<PianoKey> PianoKeys { get; set; }

        public PianoKeyboardControl()
        {
            InitializeComponent();
            PianoKeys = GenerateKeyList();
            KeysContainer.ItemsSource = PianoKeys;
        }

        private ObservableCollection<PianoKey> GenerateKeyList()
        {
            var keyList = new ObservableCollection<PianoKey>();
            int octaves = 7;
            int index = 1;
            string[] notes = ["C", "C#", "D", "D#", "E", "F", "G", "G#", "A", "A#", "B"];
            for (int i = 1; i <= octaves; i++)
            {
                for (int j = 0; j <= notes.Length; j++)
                {
                    bool isBlack = notes[j].Contains('#');
                    PianoKey note = new PianoKey() { Note = notes[j] + i, isBlack = isBlack, Index = index };
                    keyList.Add(note);
                    index++;
                    Console.WriteLine(note.ToString());
                }
            }

            return keyList;
        }
    }
}
