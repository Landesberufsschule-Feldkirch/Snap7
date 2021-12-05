using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using LaborLinearachse.Commands;
using System.Windows.Input;
using System.Windows.Media;

namespace LaborLinearachse.ViewModel;

public partial class ViewModel
{
    private readonly MainWindow _mainWindow;
    private Model.Linearachse _linearachse;
    public ViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;

        for (var i = 0; i < 100; i++)
        {
            ClkMode.Add(ClickMode.Press);
            SichtbarEin.Add(Visibility.Hidden);
            SichtbarAus.Add(Visibility.Visible);
            Farbe.Add(Brushes.White);
        }

        PositionSchlitten = 200;

        FensterTitel = "uups";
        SpsSichtbar = Visibility.Hidden;
        SpsVersionLokal = "fehlt";
        SpsVersionEntfernt = "fehlt";
        SpsStatus = "x";
        SpsColor = Brushes.LightBlue;

        System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
    }

    public void SetModel(Model.Linearachse linearachse) => _linearachse= linearachse;

    #region Taster, Schalter, OnPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private ICommand _btnTaster;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnTaster => _btnTaster ??= new RelayCommand(Taster);

    private ICommand _btnSchalter;
    // ReSharper disable once UnusedMember.Global
    public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(Schalter);
    #endregion
}