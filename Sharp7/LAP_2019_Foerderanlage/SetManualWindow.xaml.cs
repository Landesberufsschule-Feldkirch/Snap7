using System.Windows;

namespace LAP_2019_Foerderanlage
{
    public partial class SetManualWindow : Window
    {
        public bool M1_RL, M1_LL, M2, Y1;

        public SetManualWindow()
        {
            InitializeComponent();
        }

        private void BtnY1_Click(object sender, RoutedEventArgs e)
        {
            Y1 = !Y1;
        }

        private void BtnM1_LL_Click(object sender, RoutedEventArgs e)
        {
            M1_LL = !M1_LL;
        }

        private void BtnM1_RL_Click(object sender, RoutedEventArgs e)
        {
            M1_RL = !M1_RL;
        }

        private void BtnM2_Click(object sender, RoutedEventArgs e)
        {
            M2 = !M2;
        }
    }
}
