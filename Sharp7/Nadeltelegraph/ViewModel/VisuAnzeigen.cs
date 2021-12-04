using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nadeltelegraph.ViewModel;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

public class VisuAnzeigen : INotifyPropertyChanged
{
    private readonly Model.Nadeltelegraph _nadeltelegraph;
    private readonly MainWindow _mainWindow;

    public VisuAnzeigen(MainWindow mw, Model.Nadeltelegraph nt)
    {
        _mainWindow = mw;
        _nadeltelegraph = nt;

        SpsSichtbar = Visibility.Hidden;
        SpsVersionLokal = "fehlt";
        SpsVersionEntfernt = "fehlt";
        SpsStatus = "x";
        SpsColor = Brushes.LightBlue;

        for (var i = 0; i < 100; i++)
        {
            ClkMode.Add(ClickMode.Press);
            AlleWinkel.Add(0);
            AlleBreiten.Add(0);
        }

        ColorP0 = Brushes.LightGray;

        System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
    }

    private void VisuAnzeigenTask()
    {
        while (true)
        {
            FarbeP0(_nadeltelegraph.P0);

            _nadeltelegraph.AlleZeiger[1].SetPosition(_nadeltelegraph.P1R, _nadeltelegraph.P1L);
            _nadeltelegraph.AlleZeiger[2].SetPosition(_nadeltelegraph.P2R, _nadeltelegraph.P2L);
            _nadeltelegraph.AlleZeiger[3].SetPosition(_nadeltelegraph.P3R, _nadeltelegraph.P3L);
            _nadeltelegraph.AlleZeiger[4].SetPosition(_nadeltelegraph.P4R, _nadeltelegraph.P4L);
            _nadeltelegraph.AlleZeiger[5].SetPosition(_nadeltelegraph.P5R, _nadeltelegraph.P5L);

            for (var i = 1; i < 6; i++)
            {
                AlleWinkel[i] = _nadeltelegraph.AlleZeiger[i].GetWinkel();
                AlleBreiten[10 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteUpLeft();
                AlleBreiten[20 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteUpRight();
                AlleBreiten[30 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteDownLeft();
                AlleBreiten[40 + i] = _nadeltelegraph.AlleZeiger[i].GetBreiteDownRight();
            }

            if (_mainWindow.PlcDaemon != null && _mainWindow.PlcDaemon.Plc != null)
            {
                SpsVersionLokal = _mainWindow.VersionInfoLokal;
                SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + _mainWindow.VersionInfoLokal;
            }

            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    #region SPS Version, Status und Farbe
    private string fensterTitel;
    public string FensterTitel
    {
        get => fensterTitel;
        set
        {
            fensterTitel = value;
            OnPropertyChanged(nameof(FensterTitel));
        }
    }
    private string _spsVersionLokal;
    public string SpsVersionLokal
    {
        get => _spsVersionLokal;
        set
        {
            _spsVersionLokal = value;
            OnPropertyChanged(nameof(SpsVersionLokal));
        }
    }

    private string _spsVersionEntfernt;
    public string SpsVersionEntfernt
    {
        get => _spsVersionEntfernt;
        set
        {
            _spsVersionEntfernt = value;
            OnPropertyChanged(nameof(SpsVersionEntfernt));
        }
    }

    private Visibility _spsSichtbar;
    public Visibility SpsSichtbar
    {
        get => _spsSichtbar;
        set
        {
            _spsSichtbar = value;
            OnPropertyChanged(nameof(SpsSichtbar));
        }
    }

    private string _spsStatus;

    public string SpsStatus
    {
        get => _spsStatus;
        set
        {
            _spsStatus = value;
            OnPropertyChanged(nameof(SpsStatus));
        }
    }

    private Brush _spsColor;

    public Brush SpsColor
    {
        get => _spsColor;
        set
        {
            _spsColor = value;
            OnPropertyChanged(nameof(SpsColor));
        }
    }

    #endregion SPS Versionsinfo, Status und Farbe

    private ObservableCollection<int> _alleWinkel = new();
    public ObservableCollection<int> AlleWinkel
    {
        get => _alleWinkel;
        set
        {
            _alleWinkel = value;
            OnPropertyChanged(nameof(AlleWinkel));
        }
    }

    private ObservableCollection<int> _alleBreiten = new();
    public ObservableCollection<int> AlleBreiten
    {
        get => _alleBreiten;
        set
        {
            _alleBreiten = value;
            OnPropertyChanged(nameof(AlleBreiten));
        }
    }

    #region Color P0
    public void FarbeP0(bool val) => ColorP0 = val ? Brushes.Red : Brushes.LightGray;
    private Brush _colorP0;
    public Brush ColorP0
    {
        get => _colorP0;
        set
        {
            _colorP0 = value;
            OnPropertyChanged(nameof(ColorP0));
        }
    }
    #endregion Color P0        

    #region Taster/Schalter
    internal void Taster(object id)
    {
        if (id is not string ascii) return;

        var asciiCode = ascii[0];
        _nadeltelegraph.Zeichen = ClickModeButton(asciiCode) ? asciiCode : ' ';
    }

    public bool ClickModeButton(int tasterId)
    {
        if (ClkMode[tasterId] == ClickMode.Press)
        {
            ClkMode[tasterId] = ClickMode.Release;
            return true;
        }

        ClkMode[tasterId] = ClickMode.Press;
        return false;
    }

    private ObservableCollection<ClickMode> _clkMode = new();
    public ObservableCollection<ClickMode> ClkMode
    {
        get => _clkMode;
        set
        {
            _clkMode = value;
            OnPropertyChanged(nameof(ClkMode));
        }
    }
    #endregion Taster/Schalter

    #region iNotifyPeropertyChanged Members
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion iNotifyPeropertyChanged Members
}