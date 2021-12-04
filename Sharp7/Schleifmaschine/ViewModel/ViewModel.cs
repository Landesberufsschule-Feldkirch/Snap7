using Schleifmaschine.Commands;
using System.Windows.Input;

namespace Schleifmaschine.ViewModel;

public class ViewModel
{
    public Model.Schleifmaschine Schleifmaschine { get; }
    public VisuAnzeigen ViAnz { get; set; }

    public ViewModel(MainWindow mainWindow)
    {
        Schleifmaschine = new Model.Schleifmaschine();
        ViAnz = new VisuAnzeigen(mainWindow, Schleifmaschine);
    }

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

    private ICommand _btnSchalter;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
}