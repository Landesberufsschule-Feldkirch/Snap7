using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Blinker.ViewModel;

public class VisuAnzeigen : INotifyPropertyChanged
{
    private readonly MainWindow _mainWindow;
    private readonly Model.Blinker _blinker;
    public VisuAnzeigen(MainWindow mw, Model.Blinker blinker)
    {
        _mainWindow = mw;
        _blinker = blinker;

        SpsSichtbar = Visibility.Hidden;
        SpsVersionLokal = "fehlt";
        SpsVersionEntfernt = "fehlt";
        SpsStatus = "x";
        SpsColor = Brushes.LightBlue;

        for (var i = 0; i < 100; i++)
        {
            ClkMode.Add(ClickMode.Press);
            Farbe.Add(Brushes.White);
        }

        FrequenzAnzeige = "Frequenz: 0Hz";
        TastverhaeltnisAnzeige = "Tastverhältnis: 50%";
        EinZeitAnzeige = "Einzeit: 500ms";
        AusZeitAnzeige = "Auszeit: 500ms";

        System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
    }

    private void VisuAnzeigenTask()
    {
        while (true)
        {
            if (_mainWindow.PlcDaemon != null && _mainWindow.PlcDaemon.Plc != null)
            {
                SpsVersionLokal = _mainWindow.VersionInfoLokal;
                SpsVersionEntfernt = _mainWindow.PlcDaemon.Plc.GetVersion();
                SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                SpsColor = _mainWindow.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                SpsStatus = _mainWindow.PlcDaemon.Plc?.GetSpsStatus();

                FensterTitel = _mainWindow.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + _mainWindow.VersionInfoLokal;
            }

            FrequenzAnzeige = "Frequenz: " + _blinker.Frequenz.ToString("F1") + "Hz";
            TastverhaeltnisAnzeige = "Tastverhältnis: " + _blinker.Tastverhaeltnis.ToString("F1") + "%";
            EinZeitAnzeige = "Einzeit: " + _blinker.EinZeit + "ms";
            AusZeitAnzeige = "Auszeit: " + _blinker.AusZeit + "ms";

            FarbeUmschalten(_blinker.P1, 1, Brushes.LawnGreen, Brushes.LightGray);

            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }


    internal void FarbeUmschalten(bool val, int i, Brush farbe1, Brush farbe2) => Farbe[i] = val ? farbe1 : farbe2;

    internal void Taster(object id)
    {
        if (id is not string ascii) return;

        var tasterId = short.Parse(ascii);
        var gedrueckt = ClickModeButton(tasterId);

        switch (tasterId)
        {
            case 11: _blinker.S1 = gedrueckt; break;
            case 12: _blinker.S2 = gedrueckt; break;
            case 13: _blinker.S3 = gedrueckt; break;
            case 14: _blinker.S4 = gedrueckt; break;
            case 15: _blinker.S5 = gedrueckt; break;
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

    #region Texte

    private string _frequenzAnzeige;
    public string FrequenzAnzeige
    {
        get => _frequenzAnzeige;
        set
        {
            _frequenzAnzeige = value;
            OnPropertyChanged(nameof(FrequenzAnzeige));
        }
    }

    private string _tastverhaeltnisAnzeige;
    public string TastverhaeltnisAnzeige
    {
        get => _tastverhaeltnisAnzeige;
        set
        {
            _tastverhaeltnisAnzeige = value;
            OnPropertyChanged(nameof(TastverhaeltnisAnzeige));
        }
    }

    private string _einZeitAnzeige;
    public string EinZeitAnzeige
    {
        get => _einZeitAnzeige;
        set
        {
            _einZeitAnzeige = value;
            OnPropertyChanged(nameof(EinZeitAnzeige));
        }
    }

    private string _ausZeitAnzeige;
    public string AusZeitAnzeige
    {
        get => _ausZeitAnzeige;
        set
        {
            _ausZeitAnzeige = value;
            OnPropertyChanged(nameof(AusZeitAnzeige));
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