using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Kata.Commands;
using System.Windows.Input;
using System.Windows.Media;

namespace Kata.ViewModel;

public partial class ViewModel : INotifyPropertyChanged
{
    private readonly MainWindow _mainWindow;
    private Model.Kata _kata;
        
    public ViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;

        SpsSichtbar = Visibility.Hidden;
        SpsVersionLokal = "fehlt";
        SpsVersionEntfernt = "fehlt";
        SpsStatus = "x";
        SpsColor = Brushes.LightBlue;

        for (var i = 0; i < 100; i++)
        {
            ClkMode.Add(ClickMode.Press);
            SichtbarEin.Add(Visibility.Hidden);
            SichtbarAus.Add(Visibility.Visible);
            Farbe.Add(Brushes.White);
        }

        System.Threading.Tasks.Task.Run(ViewModelTask);
    }

    public void SetModel(Model.Kata kata) => _kata = kata;

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