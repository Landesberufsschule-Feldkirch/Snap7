namespace LAP_2018_4_Niveauregelung.ViewModel;

using Commands;
using System.Windows.Input;

public class ViewModel
{
    public Model.NiveauRegelung NiveauRegelung { get; }
    public VisuAnzeigen ViAnz { get; set; }
    public ViewModel(MainWindow mainWindow)
    {
        NiveauRegelung = new Model.NiveauRegelung();
        ViAnz = new VisuAnzeigen(mainWindow, NiveauRegelung);
    }

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

    private ICommand _btnSchalter;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
}