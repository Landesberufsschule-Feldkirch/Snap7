// ReSharper disable UnusedMember.Global
namespace WordClock.ViewModel
{
    using System.Windows.Input;
    using Commands;

    public class ViewModel
    {
        public readonly Model.Zeiten zeiten;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            zeiten = new Model.Zeiten();
            ViAnzeige = new VisuAnzeigen(mainWindow, zeiten);
        }

        public Model.Zeiten Zeiten => zeiten;

        #region BtnSetCurrentTime

        private ICommand _btnSetCurrentTime;

        public ICommand BtnSetCurrentTime
        {
            get
            {
                return _btnSetCurrentTime ??
                       (_btnSetCurrentTime = new RelayCommand(p => zeiten.SetCurrentTime(), p => true));
            }
        }

        #endregion BtnSetCurrentTime
    }
}