namespace Parkhaus.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Parkhaus Parkhaus { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Parkhaus = new Model.Parkhaus();
            ViAnzeige = new VisuAnzeigen(mainWindow, Parkhaus);
        }


        private ICommand _btnAuto;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto => _btnAuto ??= new RelayCommand(ViAnzeige.ClickAuto);


        private ICommand _btnTasterZufall;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterZufall => _btnTasterZufall ??= new RelayCommand(p => ViAnzeige.TasterZufall(), p => true);
    }
}