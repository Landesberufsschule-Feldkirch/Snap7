using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Schleifmaschine.ViewModel;

public class VisuAnzeigen : INotifyPropertyChanged
{
    private readonly Model.Schleifmaschine _schleifmaschine;
    private readonly MainWindow _mainWindow;

    public VisuAnzeigen(MainWindow mw, Model.Schleifmaschine schleifmaschine)
    {
        _mainWindow = mw;
        _schleifmaschine = schleifmaschine;

        SchleifmaschineDrehzahl = "n=0";

        WinkelSchleifmaschine = 10;
        AktuelleDrehzahl = 0;

        for (var i = 0; i < 100; i++)
        {
            ClkMode.Add(ClickMode.Press);
            SichtbarEin.Add(Visibility.Hidden);
            SichtbarAus.Add(Visibility.Visible);
            Farbe.Add(Brushes.White);
        }

        SpsSichtbar = Visibility.Hidden;
        SpsVersionLokal = "fehlt";
        SpsVersionEntfernt = "fehlt";
        SpsStatus = "x";
        SpsColor = Brushes.LightBlue;

        System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
    }
    private void VisuAnzeigenTask()
    {
        while (true)
        {
            WinkelSchleifmaschine = _schleifmaschine.WinkelSchleifmaschine;
            AktuelleDrehzahl = _schleifmaschine.DrehzahlSchleifmaschine;
            SchleifmaschineDrehzahl = "n=" + _schleifmaschine.DrehzahlSchleifmaschine;

            SichtbarkeitUmschalten(_schleifmaschine.B1, 20);

            FarbeUmschalten(_schleifmaschine.F1, 2, Brushes.LawnGreen, Brushes.Red);
            FarbeUmschalten(_schleifmaschine.F2, 3, Brushes.LawnGreen, Brushes.Red);

            FarbeUmschalten(_schleifmaschine.P1, 4, Brushes.White, Brushes.LightGray);
            FarbeUmschalten(_schleifmaschine.P2, 5, Brushes.LawnGreen, Brushes.LightGray);
            FarbeUmschalten(_schleifmaschine.P3, 6, Brushes.Red, Brushes.LightGray);
            FarbeUmschalten(_schleifmaschine.S3, 13, Brushes.LawnGreen, Brushes.Red);

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
    internal void FarbeUmschalten(bool val, int i, Brush farbe1, Brush farbe2) => Farbe[i] = val ? farbe1 : farbe2;
    internal void SichtbarkeitUmschalten(bool val, int i)
    {
        SichtbarEin[i] = val ? Visibility.Visible : Visibility.Collapsed;
        SichtbarAus[i] = val ? Visibility.Collapsed : Visibility.Visible;
    }
    internal void Taster(object id)
    {
        if (id is not string ascii) return;

        var tasterId = short.Parse(ascii);
        var gedrueckt = ClickModeButton(tasterId);

        switch (tasterId)
        {
            case 10: _schleifmaschine.S0 = !gedrueckt; break;
            case 11: _schleifmaschine.S1 = gedrueckt; break;
            case 12: _schleifmaschine.S2 = gedrueckt; break;
            case 14:
                _schleifmaschine.S4 = gedrueckt;
                _schleifmaschine.B1 = false;
                break;

            default: throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
    internal void Schalter(object id)
    {
        if (id is not string ascii) return;

        var schalterId = short.Parse(ascii);

        switch (schalterId)
        {
            case 2: _schleifmaschine.F1 = !_schleifmaschine.F1; break;
            case 3: _schleifmaschine.F2 = !_schleifmaschine.F2; break;
            case 13: _schleifmaschine.S3 = !_schleifmaschine.S3; break;
            default: throw new ArgumentOutOfRangeException(nameof(id));
        }
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

    private double _schleifmaschineDrehzahl;
    public string SchleifmaschineDrehzahl
    {
        get => "n=" + _schleifmaschineDrehzahl;
        set
        {
            _schleifmaschineDrehzahl = Math.Floor(Convert.ToDouble(value[2..]));
            OnPropertyChanged(nameof(SchleifmaschineDrehzahl));
        }
    }

    private double _winkelSchleifmaschine;
    public double WinkelSchleifmaschine
    {
        get => _winkelSchleifmaschine;
        set
        {
            _winkelSchleifmaschine = value;
            OnPropertyChanged(nameof(WinkelSchleifmaschine));
        }
    }

    private double _aktuelleDrehzahl;
    public double AktuelleDrehzahl
    {
        get => _aktuelleDrehzahl;
        set
        {
            _aktuelleDrehzahl = value;
            OnPropertyChanged(nameof(AktuelleDrehzahl));
        }
    }

    #region Sichtbarkeit
    private ObservableCollection<Visibility> _sichtbarEin = new();
    public ObservableCollection<Visibility> SichtbarEin
    {
        get => _sichtbarEin;
        set
        {
            _sichtbarEin = value;
            OnPropertyChanged(nameof(SichtbarEin));
        }
    }

    private ObservableCollection<Visibility> _sichtbarAus = new();
    public ObservableCollection<Visibility> SichtbarAus
    {
        get => _sichtbarAus;
        set
        {
            _sichtbarAus = value;
            OnPropertyChanged(nameof(SichtbarAus));
        }
    }
    #endregion

    #region Farbe
    private ObservableCollection<Brush> _farbe = new();
    public ObservableCollection<Brush> Farbe
    {
        get => _farbe;
        set
        {
            _farbe = value;
            OnPropertyChanged(nameof(Farbe));
        }
    }
    #endregion

    #region Taster/Schalter
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