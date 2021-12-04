using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace LAP_2018_1_Silosteuerung.ViewModel;

public class VisuAnzeigen : INotifyPropertyChanged
{
    private const int MaterialSiloHoehe = 8 * 35;

    private readonly MainWindow _mainWindow;
    public readonly Model.Silosteuerung Silosteuerung;

    public VisuAnzeigen(MainWindow mw, Model.Silosteuerung st)
    {
        _mainWindow = mw;
        Silosteuerung = st;

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

        TxtLagerSiloVoll = "Rutsche Voll";

        Margin1 = new Thickness(0, MaterialSiloHoehe * 0.1, 0, 0);


        PosWagenBeschriftungLeft = 74;
        PosWagenBeschriftungTop = 106;

        PosWagenLeft = 0;
        PosWagenTop = 0;

        PosWagenInhaltLeft = 12;
        PosWagenInhaltTop = 10;

        WagenFuellstand = 88;

        System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
    }

    private void VisuAnzeigenTask()
    {
        while (true)
        {
            FuellstandSilo(Silosteuerung.Silo.GetFuellstand());

            FarbeUmschalten(Silosteuerung.F1, 3, Brushes.LawnGreen, Brushes.Red);
            FarbeUmschalten(Silosteuerung.F2, 4, Brushes.LawnGreen, Brushes.Red);

            FarbeUmschalten(Silosteuerung.P1, 5, Brushes.LawnGreen, Brushes.White);
            FarbeUmschalten(Silosteuerung.P2, 6, Brushes.Red, Brushes.White);
            FarbeUmschalten(Silosteuerung.Q1, 7, Brushes.LawnGreen, Brushes.White);

            FarbeUmschalten(Silosteuerung.S2, 12, Brushes.LawnGreen, Brushes.Red);

            FarbeUmschalten(Silosteuerung.RutscheVoll, 32, Brushes.Firebrick, Brushes.LightGray);

            SichtbarkeitUmschalten(Silosteuerung.B1, 1);
            SichtbarkeitUmschalten(Silosteuerung.B2, 2);
            SichtbarkeitUmschalten(Silosteuerung.Q1, 7);
            SichtbarkeitUmschalten(Silosteuerung.Q2, 8);
            SichtbarkeitUmschalten(Silosteuerung.Y1, 20);
            SichtbarkeitUmschalten(Silosteuerung.Silo.GetFuellstand() > 0.01, 30);
            SichtbarkeitUmschalten(Silosteuerung.Silo.GetFuellstand() > 0.01 && Silosteuerung.Y1, 31);

            TxtLagerSiloVoll = Silosteuerung.RutscheVoll ? "LagerSilo Voll" : "LagerSilo Leer";

            PositionWagenBeschriftung(Silosteuerung.Wagen.GetPosition());
            PositionWagen(Silosteuerung.Wagen.GetPosition());
            PositionWagenInhalt(Silosteuerung.Wagen.GetPosition(), Silosteuerung.Wagen.GetFuellstand());
            WagenFuellstand = Math.Floor(Silosteuerung.Wagen.GetFuellstand());

            FuellstandProzent = (100 * Silosteuerung.Silo.GetFuellstand()).ToString("F0") + "%";

            if (_mainWindow.AnimationGestartet)
            {
                if (Silosteuerung.Q2) _mainWindow.Controller.Play(); else _mainWindow.Controller.Pause();
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
            case 10: Silosteuerung.S0 = !gedrueckt; break;
            case 11: Silosteuerung.S1 = gedrueckt; break;
            case 13: Silosteuerung.S3 = gedrueckt; break;
            case 14: Silosteuerung.WagenNachLinks(); break;
            case 15: Silosteuerung.WagenNachRechts(); break;
            default: throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
    internal void Schalter(object id)
    {
        if (id is not string ascii) return;

        var schalterId = short.Parse(ascii);

        switch (schalterId)
        {
            case 3: Silosteuerung.F1 = !Silosteuerung.F1; break;
            case 4: Silosteuerung.F2 = !Silosteuerung.F2; break;
            case 12: Silosteuerung.S2 = !Silosteuerung.S2; break;
            case 40: Silosteuerung.RutscheVoll = !Silosteuerung.RutscheVoll; break;
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

    #region Füllstand Silo
    private string _txtRutscheVoll;
    public string TxtLagerSiloVoll
    {
        get => _txtRutscheVoll;
        set
        {
            _txtRutscheVoll = value;
            OnPropertyChanged(nameof(TxtLagerSiloVoll));
        }
    }

    private string _fuellstandProzent;
    public string FuellstandProzent
    {
        get => _fuellstandProzent;
        set
        {
            _fuellstandProzent = value;
            OnPropertyChanged(nameof(FuellstandProzent));
        }
    }

    public void FuellstandSilo(double pegel)
    {
        Margin1 = new Thickness(0, MaterialSiloHoehe * (1 - pegel), 0, 0);
    }

    private Thickness _margin1;

    public Thickness Margin1
    {
        get => _margin1;
        set
        {
            _margin1 = value;
            OnPropertyChanged(nameof(Margin1));
        }
    }
    #endregion

    #region Wagenpublic
    private void PositionWagenBeschriftung(Punkt pos)
    {
        PosWagenBeschriftungLeft = pos.X + 74;
        PosWagenBeschriftungTop = pos.Y + 106;
    }
    private double _posWagenBeschriftungLeft;
    public double PosWagenBeschriftungLeft
    {
        get => _posWagenBeschriftungLeft;
        set
        {
            _posWagenBeschriftungLeft = value;
            OnPropertyChanged(nameof(PosWagenBeschriftungLeft));
        }
    }

    private double _posWagenBeschriftungTop;
    public double PosWagenBeschriftungTop
    {
        get => _posWagenBeschriftungTop;
        set
        {
            _posWagenBeschriftungTop = value;
            OnPropertyChanged(nameof(PosWagenBeschriftungTop));
        }
    }

    public void PositionWagen(Punkt pos)
    {
        PosWagenLeft = pos.X;
        PosWagenTop = pos.Y;
    }
    private double _posWagenLeft;
    public double PosWagenLeft
    {
        get => _posWagenLeft;
        set
        {
            _posWagenLeft = value;
            OnPropertyChanged(nameof(PosWagenLeft));
        }
    }

    private double _posWagenTop;
    public double PosWagenTop
    {
        get => _posWagenTop;
        set
        {
            _posWagenTop = value;
            OnPropertyChanged(nameof(PosWagenTop));
        }
    }

    public void PositionWagenInhalt(Punkt pos, double fuellstand)
    {
        PosWagenInhaltLeft = pos.X + 12;
        PosWagenInhaltTop = pos.Y + 10 + 88 - fuellstand;
    }
    private double _posWagenInhaltLeft;
    public double PosWagenInhaltLeft
    {
        get => _posWagenInhaltLeft;
        set
        {
            _posWagenInhaltLeft = value;
            OnPropertyChanged(nameof(PosWagenInhaltLeft));
        }
    }

    private double _posWagenInhaltTop;
    public double PosWagenInhaltTop
    {
        get => _posWagenInhaltTop;
        set
        {
            _posWagenInhaltTop = value;
            OnPropertyChanged(nameof(PosWagenInhaltTop));
        }
    }


    private double _wagenFuellstand;
    public double WagenFuellstand
    {
        get => _wagenFuellstand;
        set
        {
            _wagenFuellstand = value;
            OnPropertyChanged(nameof(WagenFuellstand));
        }
    }
    #endregion Wagenpublic 

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