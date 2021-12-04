namespace LAP_2010_5_Pumpensteuerung.ViewModel;

using Commands;
using System.Windows.Input;

public class ViewModel
{
    public Model.Pumpensteuerung Pumpensteuerung { get; }
    public VisuAnzeigen ViAnz { get; set; }
    public ViewModel(MainWindow mainWindow)
    {
        Pumpensteuerung = new Model.Pumpensteuerung();
        ViAnz = new VisuAnzeigen(mainWindow, Pumpensteuerung);
    }

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

    private ICommand _btnSchalter;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
}