using LAP_2010_1_Kompressoranlage.Commands;

namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Kompressoranlage kompressoranlage;

        public ViewModel(MainWindow mainWindow)
        {
            kompressoranlage = new Model.Kompressoranlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, kompressoranlage);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.Kompressoranlage Kompressoranlage => kompressoranlage;

        #region BtnF1

        private ICommand _btnF1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ?? (_btnF1 = new RelayCommand(p => kompressoranlage.BtnF1(), p => true));

        #endregion BtnF1

        #region BtnS1

        private ICommand _btnS1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true));

        #endregion BtnS2

        #region BtnB1

        private ICommand _btnB1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnB1 => _btnB1 ?? (_btnB1 = new RelayCommand(p => kompressoranlage.BtnB1(), p => true));

        #endregion BtnB1
    }
}