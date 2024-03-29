﻿using System.Windows.Controls;
using System.Windows.Media;

namespace LAP_2019_Foerderanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const int MaterialSiloHoehe = 8 * 35;
        private readonly MainWindow _mainWindow;
        public readonly Model.Foerderanlage Foerderanlage;

        public VisuAnzeigen(MainWindow mw, Model.Foerderanlage fa)
        {
            _mainWindow = mw;
            Foerderanlage = fa;

            SpsSichtbar = Visibility.Hidden;
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = Brushes.LightBlue;

            SelectedIndex = 0; // Automatikbetrieb

            Margin1 = new Thickness(0, MaterialSiloHoehe * 0.1, 0, 0);

            ClickModeBtnS0 = ClickMode.Press;
            ClickModeBtnS1 = ClickMode.Press;
            ClickModeBtnS5 = ClickMode.Press;
            ClickModeBtnS6 = ClickMode.Press;
            ClickModeBtnS7 = ClickMode.Press;
            ClickModeBtnS8 = ClickMode.Press;

            ClickModeBtnM1Rl = ClickMode.Press;
            ClickModeBtnM1Ll = ClickMode.Press;
            ClickModeBtnM2 = ClickMode.Press;
            ClickModeBtnK1 = ClickMode.Press;
            ClickModeBtnM1LlK1 = ClickMode.Press;

            VisibilityBtnSetManual = Visibility.Visible;

            VisibilityM1Ein = Visibility.Hidden;
            VisibilityM2Ein = Visibility.Hidden;

            VisibilityK1Ein = Visibility.Hidden;
            VisibilityK1Aus = Visibility.Visible;

            VisibilityB1Ein = Visibility.Visible;
            VisibilityB1Aus = Visibility.Hidden;

            VisibilityB2Ein = Visibility.Visible;
            VisibilityB2Aus = Visibility.Hidden;

            VisibilityMaterialOben = Visibility.Hidden;
            VisibilityMaterialUnten = Visibility.Hidden;

            VisibilityPfeilLinkslauf = Visibility.Hidden;
            VisibilityPfeilRechtslauf = Visibility.Hidden;

            VisibilityKurzschluss = Visibility.Hidden;

            ColorF1 = Brushes.LawnGreen;
            ColorP1 = Brushes.LawnGreen;
            ColorP2 = Brushes.LawnGreen;
            ColorS2 = Brushes.LawnGreen;

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
                FuellstandSilo(Foerderanlage.Silo.GetFuellstand());

                SichtbarkeitBtnSetManual(System.Diagnostics.Debugger.IsAttached);

                SichtbarkeitPfeilLinkslauf(Foerderanlage.Q2);
                SichtbarkeitPfeilRechtslauf(Foerderanlage.Q1);

                SichtbarkeitM1(Foerderanlage.Q1 || Foerderanlage.Q2);
                SichtbarkeitM2(Foerderanlage.T1);

                SichtbarkeitK1(Foerderanlage.K1);

                SichtbarkeitB1(Foerderanlage.Wagen.IstWagenRechts());
                SichtbarkeitB2(Foerderanlage.Wagen.IstWagenVoll());

                SichtbarkeitMaterialOben(Foerderanlage.Silo.GetFuellstand() > 0.01);
                SichtbarkeitMaterialUnten(Foerderanlage.Silo.GetFuellstand() > 0.01 && Foerderanlage.K1);

                if (Foerderanlage.Q1 && Foerderanlage.Q2) VisibilityKurzschluss = Visibility.Visible; else VisibilityKurzschluss = Visibility.Hidden;

                FarbeF1(Foerderanlage.F1);
                FarbeP1(Foerderanlage.P1);
                FarbeP2(Foerderanlage.P2);
                FarbeS2(Foerderanlage.S2);

                PositionWagenBeschriftung(Foerderanlage.Wagen.GetPosition());
                PositionWagen(Foerderanlage.Wagen.GetPosition());
                PositionWagenInhalt(Foerderanlage.Wagen.GetPosition(), Foerderanlage.Wagen.GetFuellstand());
                WagenFuellstand = Foerderanlage.Wagen.GetFuellstand();

                if (_mainWindow.AnimationGestartet)
                {
                    if (Foerderanlage.T1) _mainWindow.Controller.Play(); else _mainWindow.Controller.Pause();
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

        internal void SetS0() => Foerderanlage.S8 = ClickModeButtonS0();

        internal void SetS1() => Foerderanlage.S8 = ClickModeButtonS1();

        internal void SetS5() => Foerderanlage.S8 = ClickModeButtonS5();

        internal void SetS6() => Foerderanlage.S8 = ClickModeButtonS6();

        internal void SetS7() => Foerderanlage.S8 = ClickModeButtonS7();

        internal void SetS8() => Foerderanlage.S8 = ClickModeButtonS8();

        internal void SetManualM1_RL() => Foerderanlage.ManualM1Rl = ClickModeButtonM1_RL();

        internal void SetManualM1_LL() => Foerderanlage.ManualM1Ll = ClickModeButtonM1_LL();

        internal void SetManualM1_LL_K1()
        {
            var m1LlK1 = ClickModeButtonM1_LL_K1();
            Foerderanlage.ManualM1Ll = m1LlK1;
            Foerderanlage.ManualK1 = m1LlK1;
        }

        internal void SetManualM2() => Foerderanlage.ManualM2 = ClickModeButtonM2();

        internal void SetManualK1() => Foerderanlage.ManualK1 = ClickModeButtonK1();

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

        #region SelectedIndex

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));

                if (value == 0)
                {
                    Foerderanlage.S5 = true;    // Automatikbetrieb
                    Foerderanlage.S6 = false;    // Handbetrieb
                }
                else
                {
                    Foerderanlage.S5 = false;    // Automatikbetrieb
                    Foerderanlage.S6 = true;    // Handbetrieb
                }
            }
        }

        #endregion SelectedIndex

        #region ClickModeBtnS0

        public bool ClickModeButtonS0()
        {
            if (ClickModeBtnS0 == ClickMode.Press)
            {
                ClickModeBtnS0 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS0 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS0;

        public ClickMode ClickModeBtnS0
        {
            get => _clickModeBtnS0;
            set
            {
                _clickModeBtnS0 = value;
                OnPropertyChanged(nameof(ClickModeBtnS0));
            }
        }

        #endregion ClickModeBtnS0

        #region ClickModeBtnS1

        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 == ClickMode.Press)
            {
                ClickModeBtnS1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS1;

        public ClickMode ClickModeBtnS1
        {
            get => _clickModeBtnS1;
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }

        #endregion ClickModeBtnS1

        #region ClickModeBtnS5

        public bool ClickModeButtonS5()
        {
            if (ClickModeBtnS5 == ClickMode.Press)
            {
                ClickModeBtnS5 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS5 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS5;

        public ClickMode ClickModeBtnS5
        {
            get => _clickModeBtnS5;
            set
            {
                _clickModeBtnS5 = value;
                OnPropertyChanged(nameof(ClickModeBtnS5));
            }
        }

        #endregion ClickModeBtnS5

        #region ClickModeBtnS6

        public bool ClickModeButtonS6()
        {
            if (ClickModeBtnS6 == ClickMode.Press)
            {
                ClickModeBtnS6 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS6 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS6;

        public ClickMode ClickModeBtnS6
        {
            get => _clickModeBtnS6;
            set
            {
                _clickModeBtnS6 = value;
                OnPropertyChanged(nameof(ClickModeBtnS6));
            }
        }

        #endregion ClickModeBtnS6

        #region ClickModeBtnS7

        public bool ClickModeButtonS7()
        {
            if (ClickModeBtnS7 == ClickMode.Press)
            {
                ClickModeBtnS7 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS7 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS7;

        public ClickMode ClickModeBtnS7
        {
            get => _clickModeBtnS7;
            set
            {
                _clickModeBtnS7 = value;
                OnPropertyChanged(nameof(ClickModeBtnS7));
            }
        }

        #endregion ClickModeBtnS7

        #region ClickModeBtnS8

        public bool ClickModeButtonS8()
        {
            if (ClickModeBtnS8 == ClickMode.Press)
            {
                ClickModeBtnS8 = ClickMode.Release;
                return true;
            }

            ClickModeBtnS8 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnS8;

        public ClickMode ClickModeBtnS8
        {
            get => _clickModeBtnS8;
            set
            {
                _clickModeBtnS8 = value;
                OnPropertyChanged(nameof(ClickModeBtnS8));
            }
        }

        #endregion ClickModeBtnS8

        #region ClickModeBtnM1_RL

        public bool ClickModeButtonM1_RL()
        {
            if (ClickModeBtnM1Rl == ClickMode.Press)
            {
                ClickModeBtnM1Rl = ClickMode.Release;
                return true;
            }

            ClickModeBtnM1Rl = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnM1Rl;

        public ClickMode ClickModeBtnM1Rl
        {
            get => _clickModeBtnM1Rl;
            set
            {
                _clickModeBtnM1Rl = value;
                OnPropertyChanged(nameof(ClickModeBtnM1Rl));
            }
        }

        #endregion ClickModeBtnM1_RL

        #region ClickModeBtnM1_LL

        public bool ClickModeButtonM1_LL()
        {
            if (ClickModeBtnM1Ll == ClickMode.Press)
            {
                ClickModeBtnM1Ll = ClickMode.Release;
                return true;
            }

            ClickModeBtnM1Ll = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnM1Ll;

        public ClickMode ClickModeBtnM1Ll
        {
            get => _clickModeBtnM1Ll;
            set
            {
                _clickModeBtnM1Ll = value;
                OnPropertyChanged(nameof(ClickModeBtnM1Ll));
            }
        }

        #endregion ClickModeBtnM1_LL

        #region ClickModeBtnM2

        public bool ClickModeButtonM2()
        {
            if (ClickModeBtnM2 == ClickMode.Press)
            {
                ClickModeBtnM2 = ClickMode.Release;
                return true;
            }

            ClickModeBtnM2 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnM2;

        public ClickMode ClickModeBtnM2
        {
            get => _clickModeBtnM2;
            set
            {
                _clickModeBtnM2 = value;
                OnPropertyChanged(nameof(ClickModeBtnM2));
            }
        }

        #endregion ClickModeBtnM2

        #region ClickModeBtnK1

        public bool ClickModeButtonK1()
        {
            if (ClickModeBtnK1 == ClickMode.Press)
            {
                ClickModeBtnK1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnK1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnK1;

        public ClickMode ClickModeBtnK1
        {
            get => _clickModeBtnK1;
            set
            {
                _clickModeBtnK1 = value;
                OnPropertyChanged(nameof(ClickModeBtnK1));
            }
        }

        #endregion ClickModeBtnK1

        #region ClickModeBtnM1_LL_K1

        public bool ClickModeButtonM1_LL_K1()
        {
            if (ClickModeBtnM1LlK1 == ClickMode.Press)
            {
                ClickModeBtnM1LlK1 = ClickMode.Release;
                return true;
            }

            ClickModeBtnM1LlK1 = ClickMode.Press;
            return false;
        }

        private ClickMode _clickModeBtnM1LlK1;

        public ClickMode ClickModeBtnM1LlK1
        {
            get => _clickModeBtnM1LlK1;
            set
            {
                _clickModeBtnM1LlK1 = value;
                OnPropertyChanged(nameof(ClickModeBtnM1LlK1));
            }
        }

        #endregion ClickModeBtnM1_LL_K1

        #region FuellstandSilo

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

        #endregion FuellstandSilo

        #region Sichtbarkeit BtnSetManual

        public void SichtbarkeitBtnSetManual(bool val) => VisibilityBtnSetManual = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityBtnSetManual;

        public Visibility VisibilityBtnSetManual
        {
            get => _visibilityBtnSetManual;
            set
            {
                _visibilityBtnSetManual = value;
                OnPropertyChanged(nameof(VisibilityBtnSetManual));
            }
        }

        #endregion Sichtbarkeit BtnSetManual

        #region Sichtbarkeit PfeilLinkslauf

        public void SichtbarkeitPfeilLinkslauf(bool val) => VisibilityPfeilLinkslauf = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityPfeilLinkslauf;

        public Visibility VisibilityPfeilLinkslauf
        {
            get => _visibilityPfeilLinkslauf;
            set
            {
                _visibilityPfeilLinkslauf = value;
                OnPropertyChanged(nameof(VisibilityPfeilLinkslauf));
            }
        }

        #endregion Sichtbarkeit PfeilLinkslauf

        #region Sichtbarkeit PfeilRechtslauf

        public void SichtbarkeitPfeilRechtslauf(bool val) => VisibilityPfeilRechtslauf = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityPfeilRechtslauf;

        public Visibility VisibilityPfeilRechtslauf
        {
            get => _visibilityPfeilRechtslauf;
            set
            {
                _visibilityPfeilRechtslauf = value;
                OnPropertyChanged(nameof(VisibilityPfeilRechtslauf));
            }
        }

        #endregion Sichtbarkeit PfeilRechtslauf

        #region Sichtbarkeit M1

        public void SichtbarkeitM1(bool val) => VisibilityM1Ein = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityM1Ein;

        public Visibility VisibilityM1Ein
        {
            get => _visibilityM1Ein;
            set
            {
                _visibilityM1Ein = value;
                OnPropertyChanged(nameof(VisibilityM1Ein));
            }
        }

        #endregion Sichtbarkeit M1

        #region Sichtbarkeit M2

        public void SichtbarkeitM2(bool val) => VisibilityM2Ein = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityM2Ein;

        public Visibility VisibilityM2Ein
        {
            get => _visibilityM2Ein;
            set
            {
                _visibilityM2Ein = value;
                OnPropertyChanged(nameof(VisibilityM2Ein));
            }
        }

        #endregion Sichtbarkeit M2

        #region Sichtbarkeit K1

        public void SichtbarkeitK1(bool val)
        {
            if (val)
            {
                VisibilityK1Ein = Visibility.Visible;
                VisibilityK1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityK1Ein = Visibility.Hidden;
                VisibilityK1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityK1Ein;

        public Visibility VisibilityK1Ein
        {
            get => _visibilityK1Ein;
            set
            {
                _visibilityK1Ein = value;
                OnPropertyChanged(nameof(VisibilityK1Ein));
            }
        }

        private Visibility _visibilityK1Aus;

        public Visibility VisibilityK1Aus
        {
            get => _visibilityK1Aus;
            set
            {
                _visibilityK1Aus = value;
                OnPropertyChanged(nameof(VisibilityK1Aus));
            }
        }

        #endregion Sichtbarkeit K1

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = Visibility.Visible;
                VisibilityB1Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB1Ein = Visibility.Hidden;
                VisibilityB1Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB1Ein;

        public Visibility VisibilityB1Ein
        {
            get => _visibilityB1Ein;
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private Visibility _visibilityB1Aus;

        public Visibility VisibilityB1Aus
        {
            get => _visibilityB1Aus;
            set
            {
                _visibilityB1Aus = value;
                OnPropertyChanged(nameof(VisibilityB1Aus));
            }
        }

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

        public void SichtbarkeitB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = Visibility.Visible;
                VisibilityB2Aus = Visibility.Hidden;
            }
            else
            {
                VisibilityB2Ein = Visibility.Hidden;
                VisibilityB2Aus = Visibility.Visible;
            }
        }

        private Visibility _visibilityB2Ein;

        public Visibility VisibilityB2Ein
        {
            get => _visibilityB2Ein;
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private Visibility _visibilityB2Aus;

        public Visibility VisibilityB2Aus
        {
            get => _visibilityB2Aus;
            set
            {
                _visibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Sichtbarkeit B2

        #region Sichtbarkeit MaterialOben

        public void SichtbarkeitMaterialOben(bool val) => VisibilityMaterialOben = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityMaterialOben;

        public Visibility VisibilityMaterialOben
        {
            get => _visibilityMaterialOben;
            set
            {
                _visibilityMaterialOben = value;
                OnPropertyChanged(nameof(VisibilityMaterialOben));
            }
        }

        #endregion Sichtbarkeit MaterialOben

        #region Sichtbarkeit MaterialUnten

        public void SichtbarkeitMaterialUnten(bool val) => VisibilityMaterialUnten = val ? Visibility.Visible : Visibility.Hidden;

        private Visibility _visibilityMaterialUnten;

        public Visibility VisibilityMaterialUnten
        {
            get => _visibilityMaterialUnten;
            set
            {
                _visibilityMaterialUnten = value;
                OnPropertyChanged(nameof(VisibilityMaterialUnten));
            }
        }

        #endregion Sichtbarkeit MaterialUnten

        #region VisibilityKurzschluss

        private Visibility _visibilityKurzschluss;

        public Visibility VisibilityKurzschluss
        {
            get => _visibilityKurzschluss;
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }

        #endregion VisibilityKurzschluss

        #region Color F1

        public void FarbeF1(bool val) => ColorF1 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorF1;

        public Brush ColorF1
        {
            get => _colorF1;
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }

        #endregion Color F1

        #region Color P1

        public void FarbeP1(bool val) => ColorP1 = val ? Brushes.LawnGreen : Brushes.White;

        private Brush _colorP1;

        public Brush ColorP1
        {
            get => _colorP1;
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }

        #endregion Color P1

        #region Color P2

        public void FarbeP2(bool val) => ColorP2 = val ? Brushes.Red : Brushes.White;

        private Brush _colorP2;

        public Brush ColorP2
        {
            get => _colorP2;
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }

        #endregion Color P2

        #region Color S2

        public void FarbeS2(bool val) => ColorS2 = val ? Brushes.LawnGreen : Brushes.Red;

        private Brush _colorS2;

        public Brush ColorS2
        {
            get => _colorS2;
            set
            {
                _colorS2 = value;
                OnPropertyChanged(nameof(ColorS2));
            }
        }

        #endregion Color S2

        #region PositionWagenBeschriftung

        public void PositionWagenBeschriftung(Punkt pos)
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

        #endregion PositionWagenBeschriftung

        #region PositionWagen

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

        #endregion PositionWagen

        #region PositionWagenInhalt

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

        #endregion PositionWagenInhalt

        #region WagenFuellstand

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
        #endregion WagenFuellstand

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}