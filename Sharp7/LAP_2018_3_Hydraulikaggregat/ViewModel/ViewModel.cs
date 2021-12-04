namespace LAP_2018_3_Hydraulikaggregat.ViewModel;

using Commands;
using System.Windows.Input;

public class ViewModel
{
    public Model.Hydraulikaggregat Hydraulikaggregat { get; }
    public VisuAnzeigen ViAnz { get; set; }
    public ViewModel(MainWindow mainWindow)
    {
        Hydraulikaggregat = new Model.Hydraulikaggregat(mainWindow, this);
        ViAnz = new VisuAnzeigen(mainWindow, Hydraulikaggregat);
    }

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

    private ICommand _btnSchalter;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);

    private ICommand _erweiterungOelkuehler;
    // ReSharper disable once UnusedMember.Global
    public ICommand ErweiterungOelkuehler => _erweiterungOelkuehler ??= new RelayCommand(_ => Hydraulikaggregat.CheckErweiterungOelkuehler(), _ => true);

    private ICommand _erweiterungZylinder;
    // ReSharper disable once UnusedMember.Global
    public ICommand ErweiterungZylinder => _erweiterungZylinder ??= new RelayCommand(_ => Hydraulikaggregat.CheckErweiterungZylinder(), _ => true);

    private ICommand _erweiterungOelfilter;
    // ReSharper disable once UnusedMember.Global
    public ICommand ErweiterungOelfilter => _erweiterungOelfilter ??= new RelayCommand(_ => Hydraulikaggregat.CheckErweiterungOelfilter(), _ => true);
}