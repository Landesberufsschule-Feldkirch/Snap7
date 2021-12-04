namespace AmpelsteuerungKieswerk.ViewModel;

using Commands;
using System.Windows.Input;

public class ViewModel
{
    public Model.AlleLastKraftWagen AlleLastKraftWagen { get; }

    public VisuAnzeigen ViAnz { get; set; }

    public ViewModel(MainWindow mainWindow)
    {
        AlleLastKraftWagen = new Model.AlleLastKraftWagen();
        ViAnz = new VisuAnzeigen(mainWindow, AlleLastKraftWagen);
    }

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
}