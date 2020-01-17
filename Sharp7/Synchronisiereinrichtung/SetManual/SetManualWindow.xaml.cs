using Synchronisiereinrichtung.Kraftwerk.ViewModels;
using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class SetManualWindow : Window
    {

        public bool Q1;
        public bool Reset;

        public Synchronisiereinrichtung.Kraftwerk.ViewModels.KraftwerkViewModel _kraftwerkViewModel;

        public SetManualWindow()
        {
            InitializeComponent();
            DataContext = _kraftwerkViewModel;
        }

        private void SchalterQ1_Click(object sender, RoutedEventArgs e)
        {
            Q1 = !Q1;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset = true;
        }
    }
}
