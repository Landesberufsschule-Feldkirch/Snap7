using BehaelterSteuerung.Commands;
using System.Windows.Input;

namespace BehaelterSteuerung.ViewModel;

public class ViewModel
{
    public Model.AlleBehaelter AlleBehaelter { get; }

    public VisuAnzeigen ViAnz { get; set; }

    public ViewModel(MainWindow mainWindow)
    {
        AlleBehaelter = new Model.AlleBehaelter();
        ViAnz = new VisuAnzeigen(mainWindow, AlleBehaelter);
    }


    private ICommand _btnVentilQ2;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnVentilQ2 => _btnVentilQ2 ??= new RelayCommand(_ => AlleBehaelter.VentilQ2(), _ => true);

    private ICommand _btnVentilQ4;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnVentilQ4 => _btnVentilQ4 ??= new RelayCommand(_ => AlleBehaelter.VentilQ4(), _ => true);

    private ICommand _btnVentilQ6;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnVentilQ6 => _btnVentilQ6 ??= new RelayCommand(_ => AlleBehaelter.VentilQ6(), _ => true);

    private ICommand _btnVentilQ8;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnVentilQ8 => _btnVentilQ8 ??= new RelayCommand(_ => AlleBehaelter.VentilQ8(), _ => true);
}