using System.Windows.Input;
using Tiefgarage.Commands;

namespace Tiefgarage.ViewModel;

public class ViewModel
{
    public Model.AlleFahrzeugePersonen AlleFahrzeugePersonen { get; }

    public VisuAnzeigen ViAnz { get; set; }

    public ViewModel(MainWindow mainWindow)
    {
        AlleFahrzeugePersonen = new Model.AlleFahrzeugePersonen();
        ViAnz = new VisuAnzeigen(mainWindow, AlleFahrzeugePersonen);
    }

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
}