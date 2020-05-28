namespace BehaelterSteuerung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly double HoeheFuellBalken = 200.0;
        private readonly Model.BehaelterSteuerung alleBehaelter;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.BehaelterSteuerung aB)
        {
            mainWindow = mw;
            alleBehaelter = aB;

            SpsStatus = "-";
            SpsColor = "LightBlue";

            VisibilityVentilQ1Ein = "hidden";
            VisibilityVentilQ2Ein = "hidden";
            VisibilityVentilQ3Ein = "hidden";
            VisibilityVentilQ4Ein = "hidden";
            VisibilityVentilQ5Ein = "hidden";
            VisibilityVentilQ6Ein = "hidden";
            VisibilityVentilQ7Ein = "hidden";
            VisibilityVentilQ8Ein = "hidden";

            VisibilityVentilQ1Aus = "visible";
            VisibilityVentilQ2Aus = "visible";
            VisibilityVentilQ3Aus = "visible";
            VisibilityVentilQ4Aus = "visible";
            VisibilityVentilQ5Aus = "visible";
            VisibilityVentilQ6Aus = "visible";
            VisibilityVentilQ7Aus = "visible";
            VisibilityVentilQ8Aus = "visible";

            ColorZuleitung_1b = "blue";
            ColorZuleitung_2b = "blue";
            ColorZuleitung_3b = "blue";
            ColorZuleitung_4b = "blue";

            ColorAbleitung_1a = "blue";
            ColorAbleitung_2a = "blue";
            ColorAbleitung_3a = "blue";
            ColorAbleitung_4a = "blue";

            ColorAbleitung_1a = "blue";
            ColorAbleitung_1b = "blue";
            ColorAbleitung_2a = "blue";
            ColorAbleitung_2b = "blue";
            ColorAbleitung_3a = "blue";
            ColorAbleitung_3b = "blue";
            ColorAbleitung_4a = "blue";
            ColorAbleitung_4b = "blue";

            ColorAbleitung_Gesamt = "blue";

            ColorLabelB1 = "red";
            ColorLabelB2 = "red";
            ColorLabelB3 = "red";
            ColorLabelB4 = "red";
            ColorLabelB5 = "red";
            ColorLabelB6 = "red";
            ColorLabelB7 = "red";
            ColorLabelB8 = "red";

            ColorCircle_P1 = "lightgray";

            EnableAutomatik1234 = true;
            EnableAutomatik1324 = true;
            EnableAutomatik1432 = true;
            EnableAutomatik4321 = true;

            Margin1 = new Thickness(0, 30, 0, 0);
            Margin2 = new Thickness(0, 50, 0, 0);
            Margin3 = new Thickness(0, 70, 0, 0);
            Margin4 = new Thickness(0, 90, 0, 0);

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                bool AbleitungenUnten;
                bool AbleitungUnten1;
                bool AbleitungUnten2;
                bool AbleitungUnten3;
                bool AbleitungUnten4;

                VisibilityVentilQ1(alleBehaelter.AlleBehaelter[0].VentilOben);
                VisibilityVentilQ3(alleBehaelter.AlleBehaelter[1].VentilOben);
                VisibilityVentilQ5(alleBehaelter.AlleBehaelter[2].VentilOben);
                VisibilityVentilQ7(alleBehaelter.AlleBehaelter[3].VentilOben);

                FarbeZuleitung1b(alleBehaelter.AlleBehaelter[0].VentilOben);
                FarbeZuleitung2b(alleBehaelter.AlleBehaelter[1].VentilOben);
                FarbeZuleitung3b(alleBehaelter.AlleBehaelter[2].VentilOben);
                FarbeZuleitung4b(alleBehaelter.AlleBehaelter[3].VentilOben);

                Margin_1(alleBehaelter.AlleBehaelter[0].Pegel);
                Margin_2(alleBehaelter.AlleBehaelter[1].Pegel);
                Margin_3(alleBehaelter.AlleBehaelter[2].Pegel);
                Margin_4(alleBehaelter.AlleBehaelter[3].Pegel);

                FarbeAbleitung1a(alleBehaelter.AlleBehaelter[0].Pegel > 0.01);
                FarbeAbleitung2a(alleBehaelter.AlleBehaelter[1].Pegel > 0.01);
                FarbeAbleitung3a(alleBehaelter.AlleBehaelter[2].Pegel > 0.01);
                FarbeAbleitung4a(alleBehaelter.AlleBehaelter[3].Pegel > 0.01);

                AbleitungUnten1 = alleBehaelter.AlleBehaelter[0].Pegel > 0.01 && alleBehaelter.AlleBehaelter[0].VentilUnten;
                AbleitungUnten2 = alleBehaelter.AlleBehaelter[1].Pegel > 0.01 && alleBehaelter.AlleBehaelter[1].VentilUnten;
                AbleitungUnten3 = alleBehaelter.AlleBehaelter[2].Pegel > 0.01 && alleBehaelter.AlleBehaelter[2].VentilUnten;
                AbleitungUnten4 = alleBehaelter.AlleBehaelter[3].Pegel > 0.01 && alleBehaelter.AlleBehaelter[3].VentilUnten;
                AbleitungenUnten = AbleitungUnten1 || AbleitungUnten2 || AbleitungUnten3 || AbleitungUnten4;

                VisibilityVentilQ2(AbleitungUnten1);
                VisibilityVentilQ4(AbleitungUnten2);
                VisibilityVentilQ6(AbleitungUnten3);
                VisibilityVentilQ8(AbleitungUnten4);

                FarbeAbleitung1b(AbleitungenUnten);
                FarbeAbleitung2b(AbleitungenUnten);
                FarbeAbleitung3b(AbleitungenUnten);
                FarbeAbleitung4b(AbleitungenUnten);
                FarbeAbleitungGesamt(AbleitungenUnten);

                FarbeLabelB1(alleBehaelter.AlleBehaelter[0].SchwimmerschalterOben);
                FarbeLabelB2(alleBehaelter.AlleBehaelter[0].SchwimmerschalterUnten);
                FarbeLabelB3(alleBehaelter.AlleBehaelter[1].SchwimmerschalterOben);
                FarbeLabelB4(alleBehaelter.AlleBehaelter[1].SchwimmerschalterUnten);
                FarbeLabelB5(alleBehaelter.AlleBehaelter[2].SchwimmerschalterOben);
                FarbeLabelB6(alleBehaelter.AlleBehaelter[2].SchwimmerschalterUnten);
                FarbeLabelB7(alleBehaelter.AlleBehaelter[3].SchwimmerschalterOben);
                FarbeLabelB8(alleBehaelter.AlleBehaelter[3].SchwimmerschalterUnten);

                FarbeCircle_P1(alleBehaelter.P1);

                EnableAutomatik1234 = !alleBehaelter.AutomatikModusAktiv;
                EnableAutomatik1324 = !alleBehaelter.AutomatikModusAktiv;
                EnableAutomatik1432 = !alleBehaelter.AutomatikModusAktiv;
                EnableAutomatik4321 = !alleBehaelter.AutomatikModusAktiv;

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        #region SPS Status und Farbe

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

        private string _spsColor;

        public string SpsColor
        {
            get => _spsColor;
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }

        #endregion SPS Status und Farbe

        #region Visibility Ventil Q1

        public void VisibilityVentilQ1(bool val)
        {
            if (val)
            {
                VisibilityVentilQ1Ein = "visible";
                VisibilityVentilQ1Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ1Ein = "hidden";
                VisibilityVentilQ1Aus = "visible";
            }
        }

        private string _visibilityVentilQ1Ein;

        public string VisibilityVentilQ1Ein
        {
            get { return _visibilityVentilQ1Ein; }
            set
            {
                _visibilityVentilQ1Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ1Ein));
            }
        }

        private string _visibilityVentilQ1Aus;

        public string VisibilityVentilQ1Aus
        {
            get { return _visibilityVentilQ1Aus; }
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
                VisibilityVentilQ2Ein = "visible";
                VisibilityVentilQ2Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ2Ein = "hidden";
                VisibilityVentilQ2Aus = "visible";
            }
        }

        private string _visibilityVentilQ2Ein;

        public string VisibilityVentilQ2Ein
        {
            get { return _visibilityVentilQ2Ein; }
            set
            {
                _visibilityVentilQ2Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ2Ein));
            }
        }

        private string _visibilityVentilQ2Aus;

        public string VisibilityVentilQ2Aus
        {
            get { return _visibilityVentilQ2Aus; }
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
                VisibilityVentilQ3Ein = "visible";
                VisibilityVentilQ3Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ3Ein = "hidden";
                VisibilityVentilQ3Aus = "visible";
            }
        }

        private string _visibilityVentilQ3Ein;

        public string VisibilityVentilQ3Ein
        {
            get { return _visibilityVentilQ3Ein; }
            set
            {
                _visibilityVentilQ3Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ3Ein));
            }
        }

        private string _visibilityVentilQ3Aus;

        public string VisibilityVentilQ3Aus
        {
            get { return _visibilityVentilQ3Aus; }
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
                VisibilityVentilQ4Ein = "visible";
                VisibilityVentilQ4Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ4Ein = "hidden";
                VisibilityVentilQ4Aus = "visible";
            }
        }

        private string _visibilityVentilQ4Ein;

        public string VisibilityVentilQ4Ein
        {
            get { return _visibilityVentilQ4Ein; }
            set
            {
                _visibilityVentilQ4Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ4Ein));
            }
        }

        private string _visibilityVentilQ4Aus;

        public string VisibilityVentilQ4Aus
        {
            get { return _visibilityVentilQ4Aus; }
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
                VisibilityVentilQ5Ein = "visible";
                VisibilityVentilQ5Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ5Ein = "hidden";
                VisibilityVentilQ5Aus = "visible";
            }
        }

        private string _visibilityVentilQ5Ein;

        public string VisibilityVentilQ5Ein
        {
            get { return _visibilityVentilQ5Ein; }
            set
            {
                _visibilityVentilQ5Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ5Ein));
            }
        }

        private string _visibilityVentilQ5Aus;

        public string VisibilityVentilQ5Aus
        {
            get { return _visibilityVentilQ5Aus; }
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
                VisibilityVentilQ6Ein = "visible";
                VisibilityVentilQ6Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ6Ein = "hidden";
                VisibilityVentilQ6Aus = "visible";
            }
        }

        private string _visibilityVentilQ6Ein;

        public string VisibilityVentilQ6Ein
        {
            get { return _visibilityVentilQ6Ein; }
            set
            {
                _visibilityVentilQ6Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ6Ein));
            }
        }

        private string _visibilityVentilQ6Aus;

        public string VisibilityVentilQ6Aus
        {
            get { return _visibilityVentilQ6Aus; }
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
                VisibilityVentilQ7Ein = "visible";
                VisibilityVentilQ7Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ7Ein = "hidden";
                VisibilityVentilQ7Aus = "visible";
            }
        }

        private string _visibilityVentilQ7Ein;

        public string VisibilityVentilQ7Ein
        {
            get { return _visibilityVentilQ7Ein; }
            set
            {
                _visibilityVentilQ7Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ7Ein));
            }
        }

        private string _visibilityVentilQ7Aus;

        public string VisibilityVentilQ7Aus
        {
            get { return _visibilityVentilQ7Aus; }
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
                VisibilityVentilQ8Ein = "visible";
                VisibilityVentilQ8Aus = "hidden";
            }
            else
            {
                VisibilityVentilQ8Ein = "hidden";
                VisibilityVentilQ8Aus = "visible";
            }
        }

        private string _visibilityVentilQ8Ein;

        public string VisibilityVentilQ8Ein
        {
            get { return _visibilityVentilQ8Ein; }
            set
            {
                _visibilityVentilQ8Ein = value;
                OnPropertyChanged(nameof(VisibilityVentilQ8Ein));
            }
        }

        private string _visibilityVentilQ8Aus;

        public string VisibilityVentilQ8Aus
        {
            get { return _visibilityVentilQ8Aus; }
            set
            {
                _visibilityVentilQ8Aus = value;
                OnPropertyChanged(nameof(VisibilityVentilQ8Aus));
            }
        }

        #endregion Visibility Ventil Q8

        #region Color Zuleitung_1b

        public void FarbeZuleitung1b(bool val)
        {
            if (val) ColorZuleitung_1b = "blue"; else ColorZuleitung_1b = "lightblue";
        }

        private string _colorZuleitung_1b;

        public string ColorZuleitung_1b
        {
            get { return _colorZuleitung_1b; }
            set
            {
                _colorZuleitung_1b = value;
                OnPropertyChanged(nameof(ColorZuleitung_1b));
            }
        }

        #endregion Color Zuleitung_1b

        #region Color Zuleitung_2b

        public void FarbeZuleitung2b(bool val)
        {
            if (val) ColorZuleitung_2b = "blue"; else ColorZuleitung_2b = "lightblue";
        }

        private string _colorZuleitung_2b;

        public string ColorZuleitung_2b
        {
            get { return _colorZuleitung_2b; }
            set
            {
                _colorZuleitung_2b = value;
                OnPropertyChanged(nameof(ColorZuleitung_2b));
            }
        }

        #endregion Color Zuleitung_2b

        #region Color Zuleitung_3b

        public void FarbeZuleitung3b(bool val)
        {
            if (val) ColorZuleitung_3b = "blue"; else ColorZuleitung_3b = "lightblue";
        }

        private string _colorZuleitung_3b;

        public string ColorZuleitung_3b
        {
            get { return _colorZuleitung_3b; }
            set
            {
                _colorZuleitung_3b = value;
                OnPropertyChanged(nameof(ColorZuleitung_3b));
            }
        }

        #endregion Color Zuleitung_3b

        #region Color Zuleitung_4b

        public void FarbeZuleitung4b(bool val)
        {
            if (val) ColorZuleitung_4b = "blue"; else ColorZuleitung_4b = "lightblue";
        }

        private string _colorZuleitung_4b;

        public string ColorZuleitung_4b
        {
            get { return _colorZuleitung_4b; }
            set
            {
                _colorZuleitung_4b = value;
                OnPropertyChanged(nameof(ColorZuleitung_4b));
            }
        }

        #endregion Color Zuleitung_4b

        #region Color Ableitung_1a

        public void FarbeAbleitung1a(bool val)
        {
            if (val) ColorAbleitung_1a = "blue"; else ColorAbleitung_1a = "lightblue";
        }

        private string _colorAbleitung_1a;

        public string ColorAbleitung_1a
        {
            get { return _colorAbleitung_1a; }
            set
            {
                _colorAbleitung_1a = value;
                OnPropertyChanged(nameof(ColorAbleitung_1a));
            }
        }

        #endregion Color Ableitung_1a

        #region Color Ableitung_2a

        public void FarbeAbleitung2a(bool val)
        {
            if (val) ColorAbleitung_2a = "blue"; else ColorAbleitung_2a = "lightblue";
        }

        private string _colorAbleitung_2a;

        public string ColorAbleitung_2a
        {
            get { return _colorAbleitung_2a; }
            set
            {
                _colorAbleitung_2a = value;
                OnPropertyChanged(nameof(ColorAbleitung_2a));
            }
        }

        #endregion Color Ableitung_2a

        #region Color Ableitung_3a

        public void FarbeAbleitung3a(bool val)
        {
            if (val) ColorAbleitung_3a = "blue"; else ColorAbleitung_3a = "lightblue";
        }

        private string _colorAbleitung_3a;

        public string ColorAbleitung_3a
        {
            get { return _colorAbleitung_3a; }
            set
            {
                _colorAbleitung_3a = value;
                OnPropertyChanged(nameof(ColorAbleitung_3a));
            }
        }

        #endregion Color Ableitung_3a

        #region Color Ableitung_4a

        public void FarbeAbleitung4a(bool val)
        {
            if (val) ColorAbleitung_4a = "blue"; else ColorAbleitung_4a = "lightblue";
        }

        private string _colorAbleitung_4a;

        public string ColorAbleitung_4a
        {
            get { return _colorAbleitung_4a; }
            set
            {
                _colorAbleitung_4a = value;
                OnPropertyChanged(nameof(ColorAbleitung_4a));
            }
        }

        #endregion Color Ableitung_4a

        #region Color Ableitung_1b

        public void FarbeAbleitung1b(bool val)
        {
            if (val) ColorAbleitung_1b = "blue"; else ColorAbleitung_1b = "lightblue";
        }

        private string _colorAbleitung_1b;

        public string ColorAbleitung_1b
        {
            get { return _colorAbleitung_1b; }
            set
            {
                _colorAbleitung_1b = value;
                OnPropertyChanged(nameof(ColorAbleitung_1b));
            }
        }

        #endregion Color Ableitung_1b

        #region Color Ableitung_2b

        public void FarbeAbleitung2b(bool val)
        {
            if (val) ColorAbleitung_2b = "blue"; else ColorAbleitung_2b = "lightblue";
        }

        private string _colorAbleitung_2b;

        public string ColorAbleitung_2b
        {
            get { return _colorAbleitung_2b; }
            set
            {
                _colorAbleitung_2b = value;
                OnPropertyChanged(nameof(ColorAbleitung_2b));
            }
        }

        #endregion Color Ableitung_2b

        #region Color Ableitung_3b

        public void FarbeAbleitung3b(bool val)
        {
            if (val) ColorAbleitung_3b = "blue"; else ColorAbleitung_3b = "lightblue";
        }

        private string _colorAbleitung_3b;

        public string ColorAbleitung_3b
        {
            get { return _colorAbleitung_3b; }
            set
            {
                _colorAbleitung_3b = value;
                OnPropertyChanged(nameof(ColorAbleitung_3b));
            }
        }

        #endregion Color Ableitung_3b

        #region Color Ableitung_4b

        public void FarbeAbleitung4b(bool val)
        {
            if (val) ColorAbleitung_4b = "blue"; else ColorAbleitung_4b = "lightblue";
        }

        private string _colorAbleitung_4b;

        public string ColorAbleitung_4b
        {
            get { return _colorAbleitung_4b; }
            set
            {
                _colorAbleitung_4b = value;
                OnPropertyChanged(nameof(ColorAbleitung_4b));
            }
        }

        #endregion Color Ableitung_4b

        #region Color Ableitung_Gesamt

        public void FarbeAbleitungGesamt(bool val)
        {
            if (val) ColorAbleitung_Gesamt = "blue"; else ColorAbleitung_Gesamt = "lightblue";
        }

        private string _colorAbleitung_Gesamt;

        public string ColorAbleitung_Gesamt
        {
            get { return _colorAbleitung_Gesamt; }
            set
            {
                _colorAbleitung_Gesamt = value;
                OnPropertyChanged(nameof(ColorAbleitung_Gesamt));
            }
        }

        #endregion Color Ableitung_Gesamt

        #region Color LabelB1

        public void FarbeLabelB1(bool val)
        {
            if (val) ColorLabelB1 = "red"; else ColorLabelB1 = "lawngreen";
        }

        private string _colorLabelB1;

        public string ColorLabelB1
        {
            get { return _colorLabelB1; }
            set
            {
                _colorLabelB1 = value;
                OnPropertyChanged(nameof(ColorLabelB1));
            }
        }

        #endregion Color LabelB1

        #region Color LabelB2

        public void FarbeLabelB2(bool val)
        {
            if (val) ColorLabelB2 = "red"; else ColorLabelB2 = "lawngreen";
        }

        private string _colorLabelB2;

        public string ColorLabelB2
        {
            get { return _colorLabelB2; }
            set
            {
                _colorLabelB2 = value;
                OnPropertyChanged(nameof(ColorLabelB2));
            }
        }

        #endregion Color LabelB2

        #region Color LabelB3

        public void FarbeLabelB3(bool val)
        {
            if (val) ColorLabelB3 = "red"; else ColorLabelB3 = "lawngreen";
        }

        private string _colorLabelB3;

        public string ColorLabelB3
        {
            get { return _colorLabelB3; }
            set
            {
                _colorLabelB3 = value;
                OnPropertyChanged(nameof(ColorLabelB3));
            }
        }

        #endregion Color LabelB3

        #region Color LabelB4

        public void FarbeLabelB4(bool val)
        {
            if (val) ColorLabelB4 = "red"; else ColorLabelB4 = "lawngreen";
        }

        private string _colorLabelB4;

        public string ColorLabelB4
        {
            get { return _colorLabelB4; }
            set
            {
                _colorLabelB4 = value;
                OnPropertyChanged(nameof(ColorLabelB4));
            }
        }

        #endregion Color LabelB4

        #region Color LabelB5

        public void FarbeLabelB5(bool val)
        {
            if (val) ColorLabelB5 = "red"; else ColorLabelB5 = "lawngreen";
        }

        private string _colorLabelB5;

        public string ColorLabelB5
        {
            get { return _colorLabelB5; }
            set
            {
                _colorLabelB5 = value;
                OnPropertyChanged(nameof(ColorLabelB5));
            }
        }

        #endregion Color LabelB5

        #region Color LabelB6

        public void FarbeLabelB6(bool val)
        {
            if (val) ColorLabelB6 = "red"; else ColorLabelB6 = "lawngreen";
        }

        private string _colorLabelB6;

        public string ColorLabelB6
        {
            get { return _colorLabelB6; }
            set
            {
                _colorLabelB6 = value;
                OnPropertyChanged(nameof(ColorLabelB6));
            }
        }

        #endregion Color LabelB6

        #region Color LabelB7

        public void FarbeLabelB7(bool val)
        {
            if (val) ColorLabelB7 = "red"; else ColorLabelB7 = "lawngreen";
        }

        private string _colorLabelB7;

        public string ColorLabelB7
        {
            get { return _colorLabelB7; }
            set
            {
                _colorLabelB7 = value;
                OnPropertyChanged(nameof(ColorLabelB7));
            }
        }

        #endregion Color LabelB7

        #region Color LabelB8

        public void FarbeLabelB8(bool val)
        {
            if (val) ColorLabelB8 = "red"; else ColorLabelB8 = "lawngreen";
        }

        private string _colorLabelB8;

        public string ColorLabelB8
        {
            get { return _colorLabelB8; }
            set
            {
                _colorLabelB8 = value;
                OnPropertyChanged(nameof(ColorLabelB8));
            }
        }

        #endregion Color LabelB8

        #region Color P1

        public void FarbeCircle_P1(bool val)
        {
            if (val) ColorCircle_P1 = "lawngreen"; else ColorCircle_P1 = "lightgray";
        }

        private string _colorCircle_P1;

        public string ColorCircle_P1
        {
            get { return _colorCircle_P1; }
            set
            {
                _colorCircle_P1 = value;
                OnPropertyChanged(nameof(ColorCircle_P1));
            }
        }

        #endregion Color P1

        #region EnableAutomatik1234

        private bool _enableAutomatik1234;

        public bool EnableAutomatik1234
        {
            get { return _enableAutomatik1234; }
            set
            {
                _enableAutomatik1234 = value;
                OnPropertyChanged(nameof(EnableAutomatik1234));
            }
        }

        #endregion EnableAutomatik1234

        #region EnableAutomatik1324

        private bool _enableAutomatik1324;

        public bool EnableAutomatik1324
        {
            get { return _enableAutomatik1324; }
            set
            {
                _enableAutomatik1324 = value;
                OnPropertyChanged(nameof(EnableAutomatik1324));
            }
        }

        #endregion EnableAutomatik1324

        #region EnableAutomatik1432

        private bool _enableAutomatik1432;

        public bool EnableAutomatik1432
        {
            get { return _enableAutomatik1432; }
            set
            {
                _enableAutomatik1432 = value;
                OnPropertyChanged(nameof(EnableAutomatik1432));
            }
        }

        #endregion EnableAutomatik1432

        #region EnableAutomatik4321

        private bool _enableAutomatik4321;

        public bool EnableAutomatik4321
        {
            get { return _enableAutomatik4321; }
            set
            {
                _enableAutomatik4321 = value;
                OnPropertyChanged(nameof(EnableAutomatik4321));
            }
        }

        #endregion EnableAutomatik4321

        #region Margin1

        public void Margin_1(double pegel)
        {
            Margin1 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _margin1;

        public Thickness Margin1
        {
            get { return _margin1; }
            set
            {
                _margin1 = value;
                OnPropertyChanged(nameof(Margin1));
            }
        }

        #endregion Margin1

        #region Margin2

        public void Margin_2(double pegel)
        {
            Margin2 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _margin2;

        public Thickness Margin2
        {
            get { return _margin2; }
            set
            {
                _margin2 = value;
                OnPropertyChanged(nameof(Margin2));
            }
        }

        #endregion Margin2

        #region Margin3

        public void Margin_3(double pegel)
        {
            Margin3 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _margin3;

        public Thickness Margin3
        {
            get { return _margin3; }
            set
            {
                _margin3 = value;
                OnPropertyChanged(nameof(Margin3));
            }
        }

        #endregion Margin3

        #region Margin4

        public void Margin_4(double pegel)
        {
            Margin4 = new System.Windows.Thickness(0, HoeheFuellBalken * (1 - pegel), 0, 0);
        }

        private Thickness _margin4;

        public Thickness Margin4
        {
            get { return _margin4; }
            set
            {
                _margin4 = value;
                OnPropertyChanged(nameof(Margin4));
            }
        }

        #endregion Margin4

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}