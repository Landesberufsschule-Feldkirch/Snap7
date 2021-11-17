using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace BehaelterSteuerung.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const double HoeheFuellBalken = 200.0;
        private readonly Model.AlleBehaelter _alleBehaelter;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.AlleBehaelter aB)
        {
            _mainWindow = mw;
            _alleBehaelter = aB;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            AktivePermutation = "0000";

            VisibilityVentilQ1Ein = Visibility.Hidden;
            VisibilityVentilQ2Ein = Visibility.Hidden;
            VisibilityVentilQ3Ein = Visibility.Hidden;
            VisibilityVentilQ4Ein = Visibility.Hidden;
            VisibilityVentilQ5Ein = Visibility.Hidden;
            VisibilityVentilQ6Ein = Visibility.Hidden;
            VisibilityVentilQ7Ein = Visibility.Hidden;
            VisibilityVentilQ8Ein = Visibility.Hidden;

            VisibilityVentilQ1Aus = Visibility.Visible;
            VisibilityVentilQ2Aus = Visibility.Visible;
            VisibilityVentilQ3Aus = Visibility.Visible;
            VisibilityVentilQ4Aus = Visibility.Visible;
            VisibilityVentilQ5Aus = Visibility.Visible;
            VisibilityVentilQ6Aus = Visibility.Visible;
            VisibilityVentilQ7Aus = Visibility.Visible;
            VisibilityVentilQ8Aus = Visibility.Visible;

            ColorZuleitung1B = Brushes.Blue;
            ColorZuleitung2B = Brushes.Blue;
            ColorZuleitung3B = Brushes.Blue;
            ColorZuleitung4B = Brushes.Blue;

            ColorAbleitung1A = Brushes.Blue;
            ColorAbleitung2A = Brushes.Blue;
            ColorAbleitung3A = Brushes.Blue;
            ColorAbleitung4A = Brushes.Blue;

            ColorAbleitung1A = Brushes.Blue;
            ColorAbleitung1B = Brushes.Blue;
            ColorAbleitung2A = Brushes.Blue;
            ColorAbleitung2B = Brushes.Blue;
            ColorAbleitung3A = Brushes.Blue;
            ColorAbleitung3B = Brushes.Blue;
            ColorAbleitung4A = Brushes.Blue;
            ColorAbleitung4B = Brushes.Blue;

            ColorAbleitungGesamt = Brushes.Blue;

            ColorLabelB1 = Brushes.Red;
            ColorLabelB2 = Brushes.Red;
            ColorLabelB3 = Brushes.Red;
            ColorLabelB4 = Brushes.Red;
            ColorLabelB5 = Brushes.Red;
            ColorLabelB6 = Brushes.Red;
            ColorLabelB7 = Brushes.Red;
            ColorLabelB8 = Brushes.Red;

            DropDownEnabled = "true";

            ColorCircleP1 = Brushes.LightGray;

            Margin1 = new Thickness(0, 30, 0, 0);
            Margin2 = new Thickness(0, 50, 0, 0);
            Margin3 = new Thickness(0, 70, 0, 0);
            Margin4 = new Thickness(0, 90, 0, 0);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                VisibilityVentilQ1(_alleBehaelter.AlleMeineBehaelter[0].VentilOben);
                VisibilityVentilQ3(_alleBehaelter.AlleMeineBehaelter[1].VentilOben);
                VisibilityVentilQ5(_alleBehaelter.AlleMeineBehaelter[2].VentilOben);
                VisibilityVentilQ7(_alleBehaelter.AlleMeineBehaelter[3].VentilOben);

                FarbeZuleitung1B(_alleBehaelter.AlleMeineBehaelter[0].VentilOben);
                FarbeZuleitung2B(_alleBehaelter.AlleMeineBehaelter[1].VentilOben);
                FarbeZuleitung3B(_alleBehaelter.AlleMeineBehaelter[2].VentilOben);
                FarbeZuleitung4B(_alleBehaelter.AlleMeineBehaelter[3].VentilOben);

                Margin_1(_alleBehaelter.AlleMeineBehaelter[0].Pegel);
                Margin_2(_alleBehaelter.AlleMeineBehaelter[1].Pegel);
                Margin_3(_alleBehaelter.AlleMeineBehaelter[2].Pegel);
                Margin_4(_alleBehaelter.AlleMeineBehaelter[3].Pegel);

                FarbeAbleitung1A(_alleBehaelter.AlleMeineBehaelter[0].Pegel > 0.01);
                FarbeAbleitung2A(_alleBehaelter.AlleMeineBehaelter[1].Pegel > 0.01);
                FarbeAbleitung3A(_alleBehaelter.AlleMeineBehaelter[2].Pegel > 0.01);
                FarbeAbleitung4A(_alleBehaelter.AlleMeineBehaelter[3].Pegel > 0.01);

                var ableitungUnten1 = _alleBehaelter.AlleMeineBehaelter[0].Pegel > 0.01 && _alleBehaelter.AlleMeineBehaelter[0].VentilUnten;
                var ableitungUnten2 = _alleBehaelter.AlleMeineBehaelter[1].Pegel > 0.01 && _alleBehaelter.AlleMeineBehaelter[1].VentilUnten;
                var ableitungUnten3 = _alleBehaelter.AlleMeineBehaelter[2].Pegel > 0.01 && _alleBehaelter.AlleMeineBehaelter[2].VentilUnten;
                var ableitungUnten4 = _alleBehaelter.AlleMeineBehaelter[3].Pegel > 0.01 && _alleBehaelter.AlleMeineBehaelter[3].VentilUnten;
                var ableitungenUnten = ableitungUnten1 || ableitungUnten2 || ableitungUnten3 || ableitungUnten4;

                VisibilityVentilQ2(ableitungUnten1);
                VisibilityVentilQ4(ableitungUnten2);
                VisibilityVentilQ6(ableitungUnten3);
                VisibilityVentilQ8(ableitungUnten4);

                FarbeAbleitung1B(ableitungenUnten);
                FarbeAbleitung2B(ableitungenUnten);
                FarbeAbleitung3B(ableitungenUnten);
                FarbeAbleitung4B(ableitungenUnten);
                FarbeAbleitungGesamt(ableitungenUnten);

                FarbeLabelB1(_alleBehaelter.AlleMeineBehaelter[0].SchwimmerschalterOben);
                FarbeLabelB2(_alleBehaelter.AlleMeineBehaelter[0].SchwimmerschalterUnten);
                FarbeLabelB3(_alleBehaelter.AlleMeineBehaelter[1].SchwimmerschalterOben);
                FarbeLabelB4(_alleBehaelter.AlleMeineBehaelter[1].SchwimmerschalterUnten);
                FarbeLabelB5(_alleBehaelter.AlleMeineBehaelter[2].SchwimmerschalterOben);
                FarbeLabelB6(_alleBehaelter.AlleMeineBehaelter[2].SchwimmerschalterUnten);
                FarbeLabelB7(_alleBehaelter.AlleMeineBehaelter[3].SchwimmerschalterOben);
                FarbeLabelB8(_alleBehaelter.AlleMeineBehaelter[3].SchwimmerschalterUnten);

                FarbeCircle_P1(_alleBehaelter.P1);

                DropDownEnabled = _alleBehaelter.AutomatikModusAktiv() ? "false" : "true";

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

        private string _dropDownEnabled;
        public string DropDownEnabled
        {
            get => _dropDownEnabled;
            set
            {
                _dropDownEnabled = value;
                OnPropertyChanged(nameof(DropDownEnabled));
            }
        }


        private string _aktivePermutation;
        public string AktivePermutation
        {
            get => _aktivePermutation;
            set
            {
                _aktivePermutation = value;
                _alleBehaelter.AutomatikBetriebStarten(_aktivePermutation);
                OnPropertyChanged(nameof(AktivePermutation));
            }
        }

        #region Visibility Ventil Q1

        public void VisibilityVentilQ1(bool val)
        {
            if (val)
            {
                VisibilityVentilQ1Ein = Visibility.Visible;
                VisibilityVentilQ1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ1Ein = Visibility.Hidden;
                VisibilityVentilQ1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ1Ein;
        public Visibility VisibilityVentilQ1Ein
        {
            get => _visibilityVentilQ1Ein;
            set
            {
                _visibilityVentilQ1Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ1Ein));
            }
        }

        private Visibility _visibilityVentilQ1Aus;
        public Visibility VisibilityVentilQ1Aus
        {
            get => _visibilityVentilQ1Aus;
            set
            {
                _visibilityVentilQ1Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ1Aus));
            }
        }

        #endregion Visibility Ventil Q1

        #region Visibility Ventil Q2

        public void VisibilityVentilQ2(bool val)
        {
            if (val)
            {
                VisibilityVentilQ2Ein = Visibility.Visible;
                VisibilityVentilQ2Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ2Ein = Visibility.Hidden;
                VisibilityVentilQ2Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ2Ein;
        public Visibility VisibilityVentilQ2Ein
        {
            get => _visibilityVentilQ2Ein;
            set
            {
                _visibilityVentilQ2Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ2Ein));
            }
        }

        private Visibility _visibilityVentilQ2Aus;
        public Visibility VisibilityVentilQ2Aus
        {
            get => _visibilityVentilQ2Aus;
            set
            {
                _visibilityVentilQ2Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ2Aus));
            }
        }

        #endregion Visibility Ventil Q2

        #region Visibility Ventil Q3

        public void VisibilityVentilQ3(bool val)
        {
            if (val)
            {
                VisibilityVentilQ3Ein = Visibility.Visible;
                VisibilityVentilQ3Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ3Ein = Visibility.Hidden;
                VisibilityVentilQ3Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ3Ein;
        public Visibility VisibilityVentilQ3Ein
        {
            get => _visibilityVentilQ3Ein;
            set
            {
                _visibilityVentilQ3Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ3Ein));
            }
        }

        private Visibility _visibilityVentilQ3Aus;
        public Visibility VisibilityVentilQ3Aus
        {
            get => _visibilityVentilQ3Aus;
            set
            {
                _visibilityVentilQ3Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ3Aus));
            }
        }

        #endregion Visibility Ventil Q3

        #region Visibility Ventil Q4

        public void VisibilityVentilQ4(bool val)
        {
            if (val)
            {
                VisibilityVentilQ4Ein = Visibility.Visible;
                VisibilityVentilQ4Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ4Ein = Visibility.Hidden;
                VisibilityVentilQ4Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ4Ein;
        public Visibility VisibilityVentilQ4Ein
        {
            get => _visibilityVentilQ4Ein;
            set
            {
                _visibilityVentilQ4Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ4Ein));
            }
        }

        private Visibility _visibilityVentilQ4Aus;
        public Visibility VisibilityVentilQ4Aus
        {
            get => _visibilityVentilQ4Aus;
            set
            {
                _visibilityVentilQ4Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ4Aus));
            }
        }

        #endregion Visibility Ventil Q4

        #region Visibility Ventil Q5

        public void VisibilityVentilQ5(bool val)
        {
            if (val)
            {
                VisibilityVentilQ5Ein = Visibility.Visible;
                VisibilityVentilQ5Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ5Ein = Visibility.Hidden;
                VisibilityVentilQ5Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ5Ein;
        public Visibility VisibilityVentilQ5Ein
        {
            get => _visibilityVentilQ5Ein;
            set
            {
                _visibilityVentilQ5Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ5Ein));
            }
        }

        private Visibility _visibilityVentilQ5Aus;
        public Visibility VisibilityVentilQ5Aus
        {
            get => _visibilityVentilQ5Aus;
            set
            {
                _visibilityVentilQ5Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ5Aus));
            }
        }

        #endregion Visibility Ventil Q5

        #region Visibility Ventil Q6

        public void VisibilityVentilQ6(bool val)
        {
            if (val)
            {
                VisibilityVentilQ6Ein = Visibility.Visible;
                VisibilityVentilQ6Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ6Ein = Visibility.Hidden;
                VisibilityVentilQ6Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ6Ein;
        public Visibility VisibilityVentilQ6Ein
        {
            get => _visibilityVentilQ6Ein;
            set
            {
                _visibilityVentilQ6Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ6Ein));
            }
        }

        private Visibility _visibilityVentilQ6Aus;
        public Visibility VisibilityVentilQ6Aus
        {
            get => _visibilityVentilQ6Aus;
            set
            {
                _visibilityVentilQ6Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ6Aus));
            }
        }

        #endregion Visibility Ventil Q6

        #region Visibility Ventil Q7

        public void VisibilityVentilQ7(bool val)
        {
            if (val)
            {
                VisibilityVentilQ7Ein = Visibility.Visible;
                VisibilityVentilQ7Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ7Ein = Visibility.Hidden;
                VisibilityVentilQ7Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ7Ein;
        public Visibility VisibilityVentilQ7Ein
        {
            get => _visibilityVentilQ7Ein;
            set
            {
                _visibilityVentilQ7Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ7Ein));
            }
        }

        private Visibility _visibilityVentilQ7Aus;
        public Visibility VisibilityVentilQ7Aus
        {
            get => _visibilityVentilQ7Aus;
            set
            {
                _visibilityVentilQ7Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ7Aus));
            }
        }

        #endregion Visibility Ventil Q7

        #region Visibility Ventil Q8

        public void VisibilityVentilQ8(bool val)
        {
            if (val)
            {
                VisibilityVentilQ8Ein = Visibility.Visible;
                VisibilityVentilQ8Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityVentilQ8Ein = Visibility.Hidden;
                VisibilityVentilQ8Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityVentilQ8Ein;
        public Visibility VisibilityVentilQ8Ein
        {
            get => _visibilityVentilQ8Ein;
            set
            {
                _visibilityVentilQ8Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ8Ein));
            }
        }

        private Visibility _visibilityVentilQ8Aus;
        public Visibility VisibilityVentilQ8Aus
        {
            get => _visibilityVentilQ8Aus;
            set
            {
                _visibilityVentilQ8Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ8Aus));
            }
        }

        #endregion Visibility Ventil Q8

        #region Color Zuleitung_1b

        public void FarbeZuleitung1B(bool val) => ColorZuleitung1B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitung1B;
        public Brush ColorZuleitung1B
        {
            get => _colorZuleitung1B;
            set
            {
                _colorZuleitung1B = value;
                OnPropertyChanged(nameof(ColorZuleitung1B));
            }
        }

        #endregion Color Zuleitung_1b

        #region Color Zuleitung_2b

        public void FarbeZuleitung2B(bool val) => ColorZuleitung2B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitung2B;
        public Brush ColorZuleitung2B
        {
            get => _colorZuleitung2B;
            set
            {
                _colorZuleitung2B = value;
                OnPropertyChanged(nameof(ColorZuleitung2B));
            }
        }

        #endregion Color Zuleitung_2b

        #region Color Zuleitung_3b

        public void FarbeZuleitung3B(bool val) => ColorZuleitung3B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitung3B;
        public Brush ColorZuleitung3B
        {
            get => _colorZuleitung3B;
            set
            {
                _colorZuleitung3B = value;
                OnPropertyChanged(nameof(ColorZuleitung3B));
            }
        }

        #endregion Color Zuleitung_3b

        #region Color Zuleitung_4b

        public void FarbeZuleitung4B(bool val) => ColorZuleitung4B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorZuleitung4B;
        public Brush ColorZuleitung4B
        {
            get => _colorZuleitung4B;
            set
            {
                _colorZuleitung4B = value;
                OnPropertyChanged(nameof(ColorZuleitung4B));
            }
        }

        #endregion Color Zuleitung_4b

        #region Color Ableitung_1a

        public void FarbeAbleitung1A(bool val) => ColorAbleitung1A = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung1A;
        public Brush ColorAbleitung1A
        {
            get => _colorAbleitung1A;
            set
            {
                _colorAbleitung1A = value;
                OnPropertyChanged(nameof(ColorAbleitung1A));
            }
        }

        #endregion Color Ableitung_1a

        #region Color Ableitung_2a

        public void FarbeAbleitung2A(bool val) => ColorAbleitung2A = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung2A;
        public Brush ColorAbleitung2A
        {
            get => _colorAbleitung2A;
            set
            {
                _colorAbleitung2A = value;
                OnPropertyChanged(nameof(ColorAbleitung2A));
            }
        }

        #endregion Color Ableitung_2a

        #region Color Ableitung_3a

        public void FarbeAbleitung3A(bool val) => ColorAbleitung3A = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung3A;
        public Brush ColorAbleitung3A
        {
            get => _colorAbleitung3A;
            set
            {
                _colorAbleitung3A = value;
                OnPropertyChanged(nameof(ColorAbleitung3A));
            }
        }

        #endregion Color Ableitung_3a

        #region Color Ableitung_4a

        public void FarbeAbleitung4A(bool val) => ColorAbleitung4A = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung4A;
        public Brush ColorAbleitung4A
        {
            get => _colorAbleitung4A;
            set
            {
                _colorAbleitung4A = value;
                OnPropertyChanged(nameof(ColorAbleitung4A));
            }
        }

        #endregion Color Ableitung_4a

        #region Color Ableitung_1b

        public void FarbeAbleitung1B(bool val) => ColorAbleitung1B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung1B;
        public Brush ColorAbleitung1B
        {
            get => _colorAbleitung1B;
            set
            {
                _colorAbleitung1B = value;
                OnPropertyChanged(nameof(ColorAbleitung1B));
            }
        }

        #endregion Color Ableitung_1b

        #region Color Ableitung_2b

        public void FarbeAbleitung2B(bool val) => ColorAbleitung2B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung2B;
        public Brush ColorAbleitung2B
        {
            get => _colorAbleitung2B;
            set
            {
                _colorAbleitung2B = value;
                OnPropertyChanged(nameof(ColorAbleitung2B));
            }
        }

        #endregion Color Ableitung_2b

        #region Color Ableitung_3b

        public void FarbeAbleitung3B(bool val) => ColorAbleitung3B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung3B;
        public Brush ColorAbleitung3B
        {
            get => _colorAbleitung3B;
            set
            {
                _colorAbleitung3B = value;
                OnPropertyChanged(nameof(ColorAbleitung3B));
            }
        }

        #endregion Color Ableitung_3b

        #region Color Ableitung_4b

        public void FarbeAbleitung4B(bool val) => ColorAbleitung4B = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitung4B;
        public Brush ColorAbleitung4B
        {
            get => _colorAbleitung4B;
            set
            {
                _colorAbleitung4B = value;
                OnPropertyChanged(nameof(ColorAbleitung4B));
            }
        }

        #endregion Color Ableitung_4b

        #region Color Ableitung_Gesamt

        public void FarbeAbleitungGesamt(bool val) => ColorAbleitungGesamt = val ? Brushes.Blue : Brushes.LightBlue;

        private Brush _colorAbleitungGesamt;
        public Brush ColorAbleitungGesamt
        {
            get => _colorAbleitungGesamt;
            set
            {
                _colorAbleitungGesamt = value;
                OnPropertyChanged(nameof(ColorAbleitungGesamt));
            }
        }

        #endregion Color Ableitung_Gesamt

        #region Color LabelB1

        public void FarbeLabelB1(bool val) => ColorLabelB1 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB1;

        public Brush ColorLabelB1
        {
            get => _colorLabelB1;
            set
            {
                _colorLabelB1 = value;
                OnPropertyChanged(nameof(ColorLabelB1));
            }
        }

        #endregion Color LabelB1

        #region Color LabelB2

        public void FarbeLabelB2(bool val) => ColorLabelB2 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB2;

        public Brush ColorLabelB2
        {
            get => _colorLabelB2;
            set
            {
                _colorLabelB2 = value;
                OnPropertyChanged(nameof(ColorLabelB2));
            }
        }

        #endregion Color LabelB2

        #region Color LabelB3

        public void FarbeLabelB3(bool val) => ColorLabelB3 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB3;

        public Brush ColorLabelB3
        {
            get => _colorLabelB3;
            set
            {
                _colorLabelB3 = value;
                OnPropertyChanged(nameof(ColorLabelB3));
            }
        }

        #endregion Color LabelB3

        #region Color LabelB4

        public void FarbeLabelB4(bool val) => ColorLabelB4 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB4;

        public Brush ColorLabelB4
        {
            get => _colorLabelB4;
            set
            {
                _colorLabelB4 = value;
                OnPropertyChanged(nameof(ColorLabelB4));
            }
        }

        #endregion Color LabelB4

        #region Color LabelB5

        public void FarbeLabelB5(bool val) => ColorLabelB5 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB5;

        public Brush ColorLabelB5
        {
            get => _colorLabelB5;
            set
            {
                _colorLabelB5 = value;
                OnPropertyChanged(nameof(ColorLabelB5));
            }
        }

        #endregion Color LabelB5

        #region Color LabelB6

        public void FarbeLabelB6(bool val) => ColorLabelB6 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB6;

        public Brush ColorLabelB6
        {
            get => _colorLabelB6;
            set
            {
                _colorLabelB6 = value;
                OnPropertyChanged(nameof(ColorLabelB6));
            }
        }

        #endregion Color LabelB6

        #region Color LabelB7

        public void FarbeLabelB7(bool val) => ColorLabelB7 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB7;

        public Brush ColorLabelB7
        {
            get => _colorLabelB7;
            set
            {
                _colorLabelB7 = value;
                OnPropertyChanged(nameof(ColorLabelB7));
            }
        }

        #endregion Color LabelB7

        #region Color LabelB8

        public void FarbeLabelB8(bool val) => ColorLabelB8 = val ? Brushes.Red : Brushes.LawnGreen;

        private Brush _colorLabelB8;

        public Brush ColorLabelB8
        {
            get => _colorLabelB8;
            set
            {
                _colorLabelB8 = value;
                OnPropertyChanged(nameof(ColorLabelB8));
            }
        }

        #endregion Color LabelB8

        #region Color P1

        public void FarbeCircle_P1(bool val) => ColorCircleP1 = val ? Brushes.LawnGreen : Brushes.LightGray;

        private Brush _colorCircleP1;

        public Brush ColorCircleP1
        {
            get => _colorCircleP1;
            set
            {
                _colorCircleP1 = value;
                OnPropertyChanged(nameof(ColorCircleP1));
            }
        }

        #endregion Color P1

        #region Margin1

        public void Margin_1(double pegel) => Margin1 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);

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

        #endregion Margin1

        #region Margin2

        public void Margin_2(double pegel) => Margin2 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);

        private Thickness _margin2;

        public Thickness Margin2
        {
            get => _margin2;
            set
            {
                _margin2 = value;
                OnPropertyChanged(nameof(Margin2));
            }
        }

        #endregion Margin2

        #region Margin3

        public void Margin_3(double pegel) => Margin3 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);

        private Thickness _margin3;

        public Thickness Margin3
        {
            get => _margin3;
            set
            {
                _margin3 = value;
                OnPropertyChanged(nameof(Margin3));
            }
        }

        #endregion Margin3

        #region Margin4

        public void Margin_4(double pegel) => Margin4 = new Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);

        private Thickness _margin4;

        public Thickness Margin4
        {
            get => _margin4;
            set
            {
                _margin4 = value;
                OnPropertyChanged(nameof(Margin4));
            }
        }
        #endregion Margin4

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}