using LaborLinearachse.Commands;
using LaborLinearachse.Model;
using System.Windows.Input;

namespace LaborLinearachse.ViewModel;

public class ViewModel
{
    public Linearachse Linearachse { get; }
    public VisuAnzeigen ViAnz { get; set; }
    public ViewModel(MainWindow mainWindow)
    {
        Linearachse = new Linearachse();
        ViAnz = new VisuAnzeigen(mainWindow, Linearachse);
    }

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

    private ICommand _btnSchalter;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
}