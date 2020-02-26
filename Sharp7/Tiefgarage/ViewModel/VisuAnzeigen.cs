namespace Tiefgarage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.AlleFahrzeugePersonen alleFahrzeugePersonen;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen( MainWindow mw, Model.AlleFahrzeugePersonen aFP)
        {
            mainWindow = mw;
            alleFahrzeugePersonen = aFP;

            SpsStatus = "-";
            SpsColor = "LightBlue";

            ColorB1 = "LightGray";
            ColorB2 = "LightGray";

            EnableAuto1 = true;
            EnableAuto2 = true;
            EnableAuto3 = true;
            EnableAuto4 = true;
            EnablePerson1 = true;
            EnablePerson2 = true;
            EnablePerson3 = true;
            EnablePerson4 = true;

            PosAuto1Left = 0;
            PosAuto1Top = 0;
            PosAuto2Left = 0;
            PosAuto2Top = 0;
            PosAuto3Left = 0;
            PosAuto3Top = 0;
            PosAuto4Left = 0;
            PosAuto4Top = 0;

            PosPerson1Left = 0;
            PosPerson1Top = 0;
            PosPerson2Left = 0;
            PosPerson2Top = 0;
            PosPerson3Left = 0;
            PosPerson3Top = 0;
            PosPerson4Left = 0;
            PosPerson4Top = 0;

            AnzahlAutos = "Autos in der Tiefgarage: 123";
            AnzahlPersonen = "Personen in der Tiefgarage: 123";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeB1(alleFahrzeugePersonen.B1);
                FarbeB2(alleFahrzeugePersonen.B2);

                AnzahlAutosInDerTiefgarage(alleFahrzeugePersonen.AnzahlAutos);
                AnzahlPersonenInDerTiefgarage(alleFahrzeugePersonen.AnzahlPersonen);

                PositionAuto1(alleFahrzeugePersonen.AllePkwPersonen[0].AktuellePosition);
                PositionAuto2(alleFahrzeugePersonen.AllePkwPersonen[1].AktuellePosition);
                PositionAuto3(alleFahrzeugePersonen.AllePkwPersonen[2].AktuellePosition);
                PositionAuto4(alleFahrzeugePersonen.AllePkwPersonen[3].AktuellePosition);
                PositionPerson1(alleFahrzeugePersonen.AllePkwPersonen[4].AktuellePosition);
                PositionPerson2(alleFahrzeugePersonen.AllePkwPersonen[5].AktuellePosition);
                PositionPerson3(alleFahrzeugePersonen.AllePkwPersonen[6].AktuellePosition);
                PositionPerson4(alleFahrzeugePersonen.AllePkwPersonen[7].AktuellePosition);

                EnableAuto1 = alleFahrzeugePersonen.AllesInParkposition;
                EnableAuto2 = alleFahrzeugePersonen.AllesInParkposition;
                EnableAuto3 = alleFahrzeugePersonen.AllesInParkposition;
                EnableAuto4 = alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson1 = alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson2 = alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson3 = alleFahrzeugePersonen.AllesInParkposition;
                EnablePerson4 = alleFahrzeugePersonen.AllesInParkposition;               

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
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }
        #endregion


        #region Color B1
        public void FarbeB1(bool val)
        {
            if (val) ColorB1 = "Red"; else ColorB1 = "LightGray";
        }

        private string _colorB1;
        public string ColorB1
        {
            get { return _colorB1; }
            set
            {
                _colorB1 = value;
                OnPropertyChanged(nameof(ColorB1));
            }
        }
        #endregion

        #region Color B2
        public void FarbeB2(bool val)
        {
            if (val) ColorB2 = "Red"; else ColorB2 = "LightGray";
        }

        private string _colorB2;
        public string ColorB2
        {
            get { return _colorB2; }
            set
            {
                _colorB2 = value;
                OnPropertyChanged(nameof(ColorB2));
            }
        }
        #endregion


        #region EnableAuto1
        private bool _enableAuto1;
        public bool EnableAuto1
        {
            get { return _enableAuto1; }
            set
            {
                _enableAuto1 = value;
                OnPropertyChanged(nameof(EnableAuto1));
            }
        }
        #endregion

        #region EnableAuto2
        private bool _enableAuto2;
        public bool EnableAuto2
        {
            get { return _enableAuto2; }
            set
            {
                _enableAuto2 = value;
                OnPropertyChanged(nameof(EnableAuto2));
            }
        }
        #endregion

        #region EnableAuto3
        private bool _enableAuto3;
        public bool EnableAuto3
        {
            get { return _enableAuto3; }
            set
            {
                _enableAuto3 = value;
                OnPropertyChanged(nameof(EnableAuto3));
            }
        }
        #endregion

        #region EnableAuto4
        private bool _enableAuto4;
        public bool EnableAuto4
        {
            get { return _enableAuto4; }
            set
            {
                _enableAuto4 = value;
                OnPropertyChanged(nameof(EnableAuto4));
            }
        }
        #endregion

        #region EnablePerson1
        private bool _enablePerson1;
        public bool EnablePerson1
        {
            get { return _enablePerson1; }
            set
            {
                _enablePerson1 = value;
                OnPropertyChanged(nameof(EnablePerson1));
            }
        }
        #endregion

        #region EnablePerson2
        private bool _enablePerson2;
        public bool EnablePerson2
        {
            get { return _enablePerson2; }
            set
            {
                _enablePerson2 = value;
                OnPropertyChanged(nameof(EnablePerson2));
            }
        }
        #endregion

        #region EnablePerson3
        private bool _enablePerson3;
        public bool EnablePerson3
        {
            get { return _enablePerson3; }
            set
            {
                _enablePerson3 = value;
                OnPropertyChanged(nameof(EnablePerson3));
            }
        }
        #endregion

        #region EnablePerson4
        private bool _enablePerson4;
        public bool EnablePerson4
        {
            get { return _enablePerson4; }
            set
            {
                _enablePerson4 = value;
                OnPropertyChanged(nameof(EnablePerson4));
            }
        }
        #endregion


        #region PositionAuto1
        public void PositionAuto1(Punkt pos)
        {
            PosAuto1Left = pos.X;
            PosAuto1Top = pos.Y;
        }

        private double _posAuto1Left;
        public double PosAuto1Left
        {
            get { return _posAuto1Left; }
            set
            {
                _posAuto1Left = value;
                OnPropertyChanged(nameof(PosAuto1Left));
            }
        }
        private double _posAuto1Top;
        public double PosAuto1Top
        {
            get { return _posAuto1Top; }
            set
            {
                _posAuto1Top = value;
                OnPropertyChanged(nameof(PosAuto1Top));
            }
        }
        #endregion

        #region PositionAuto2
        public void PositionAuto2(Punkt pos)
        {
            PosAuto2Left = pos.X;
            PosAuto2Top = pos.Y;
        }

        private double _posAuto2Left;
        public double PosAuto2Left
        {
            get { return _posAuto2Left; }
            set
            {
                _posAuto2Left = value;
                OnPropertyChanged(nameof(PosAuto2Left));
            }
        }
        private double _posAuto2Top;
        public double PosAuto2Top
        {
            get { return _posAuto2Top; }
            set
            {
                _posAuto2Top = value;
                OnPropertyChanged(nameof(PosAuto2Top));
            }
        }
        #endregion

        #region PositionAuto3
        public void PositionAuto3(Punkt pos)
        {
            PosAuto3Left = pos.X;
            PosAuto3Top = pos.Y;
        }

        private double _posAuto3Left;
        public double PosAuto3Left
        {
            get { return _posAuto3Left; }
            set
            {
                _posAuto3Left = value;
                OnPropertyChanged(nameof(PosAuto3Left));
            }
        }
        private double _posAuto3Top;
        public double PosAuto3Top
        {
            get { return _posAuto3Top; }
            set
            {
                _posAuto3Top = value;
                OnPropertyChanged(nameof(PosAuto3Top));
            }
        }
        #endregion

        #region PositionAuto4
        public void PositionAuto4(Punkt pos)
        {
            PosAuto4Left = pos.X;
            PosAuto4Top = pos.Y;
        }

        private double _posAuto4Left;
        public double PosAuto4Left
        {
            get { return _posAuto4Left; }
            set
            {
                _posAuto4Left = value;
                OnPropertyChanged(nameof(PosAuto4Left));
            }
        }
        private double _posAuto4Top;
        public double PosAuto4Top
        {
            get { return _posAuto4Top; }
            set
            {
                _posAuto4Top = value;
                OnPropertyChanged(nameof(PosAuto4Top));
            }
        }
        #endregion



        #region PositionPerson1
        public void PositionPerson1(Punkt pos)
        {
            PosPerson1Left = pos.X;
            PosPerson1Top = pos.Y;
        }

        private double _posPerson1Left;
        public double PosPerson1Left
        {
            get { return _posPerson1Left; }
            set
            {
                _posPerson1Left = value;
                OnPropertyChanged(nameof(PosPerson1Left));
            }
        }
        private double _posPerson1Top;
        public double PosPerson1Top
        {
            get { return _posPerson1Top; }
            set
            {
                _posPerson1Top = value;
                OnPropertyChanged(nameof(PosPerson1Top));
            }
        }
        #endregion

        #region PositionPerson2
        public void PositionPerson2(Punkt pos)
        {
            PosPerson2Left = pos.X;
            PosPerson2Top = pos.Y;
        }

        private double _posPerson2Left;
        public double PosPerson2Left
        {
            get { return _posPerson2Left; }
            set
            {
                _posPerson2Left = value;
                OnPropertyChanged(nameof(PosPerson2Left));
            }
        }
        private double _posPerson2Top;
        public double PosPerson2Top
        {
            get { return _posPerson2Top; }
            set
            {
                _posPerson2Top = value;
                OnPropertyChanged(nameof(PosPerson2Top));
            }
        }
        #endregion

        #region PositionPerson3
        public void PositionPerson3(Punkt pos)
        {
            PosPerson3Left = pos.X;
            PosPerson3Top = pos.Y;
        }

        private double _posPerson3Left;
        public double PosPerson3Left
        {
            get { return _posPerson3Left; }
            set
            {
                _posPerson3Left = value;
                OnPropertyChanged(nameof(PosPerson3Left));
            }
        }
        private double _posPerson3Top;
        public double PosPerson3Top
        {
            get { return _posPerson3Top; }
            set
            {
                _posPerson3Top = value;
                OnPropertyChanged(nameof(PosPerson3Top));
            }
        }
        #endregion

        #region PositionPerson4
        public void PositionPerson4(Punkt pos)
        {
            PosPerson4Left = pos.X;
            PosPerson4Top = pos.Y;
        }

        private double _posPerson4Left;
        public double PosPerson4Left
        {
            get { return _posPerson4Left; }
            set
            {
                _posPerson4Left = value;
                OnPropertyChanged(nameof(PosPerson4Left));
            }
        }
        private double _posPerson4Top;
        public double PosPerson4Top
        {
            get { return _posPerson4Top; }
            set
            {
                _posPerson4Top = value;
                OnPropertyChanged(nameof(PosPerson4Top));
            }
        }
        #endregion

        
        #region AnzahlAutos
        public void AnzahlAutosInDerTiefgarage(int val)
        {
            AnzahlAutos = "Autos in der Tiefgarage: " + val.ToString();
        }

        private string _anzahlAutos;
        public string AnzahlAutos
        {
            get { return _anzahlAutos; }
            set
            {
                _anzahlAutos = value;
                OnPropertyChanged(nameof(AnzahlAutos));
            }
        }
        #endregion
        
        #region AnzahlPersonen
        public void AnzahlPersonenInDerTiefgarage(int val)
        {
            AnzahlPersonen = "Personen in der Tiefgarage: " + val.ToString();
        }

        private string _anzahlPersonen;
        public string AnzahlPersonen
        {
            get { return _anzahlPersonen; }
            set
            {
                _anzahlPersonen = value;
                OnPropertyChanged(nameof(AnzahlPersonen));
            }
        }
        #endregion
                     


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members

    }
}
