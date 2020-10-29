using System.ComponentModel;
using System.Windows.Controls;

namespace SiebenSegmentAnzeige
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            //LedColor = System.Windows.;

            InitializeComponent();
            DataContext = this;
        }





        [Description("(7 Segment Anzeige) Farbe der LED . System.Windows.Media.Brush "), Category("SiebenSegmentAnzeige")]
        private System.Windows.Media.Brush _ledColor;
        public System.Windows.Media.Brush LedColor
        {
            get => _ledColor;
            set
            {
                _ledColor = value;
                OnPropertyChanged(nameof(LedColor));
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
