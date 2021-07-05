namespace Parkhaus.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Parkhaus Parkhaus { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Parkhaus = new Model.Parkhaus();
            ViAnz = new VisuAnzeigen(mainWindow, Parkhaus);
        }


        private ICommand _btnAuto;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto => _btnAuto ??= new RelayCommand(ViAnz.ClickAuto);


        private ICommand _btnTasterZufall;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterZufall => _btnTasterZufall ??= new RelayCommand(_ => ViAnz.TasterZufall(), _ => true);
    }
}