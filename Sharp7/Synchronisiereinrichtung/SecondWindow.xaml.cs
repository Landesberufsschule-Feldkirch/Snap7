using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class SecondWindow : Window
    {

        public bool Q1;
        public bool Reset;
        
        public SecondWindow()
        {
            InitializeComponent();
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
