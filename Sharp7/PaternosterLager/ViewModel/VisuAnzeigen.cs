namespace PaternosterLager.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly PaternosterLager.Model.Paternosterlager paternosterlager;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, PaternosterLager.Model.Paternosterlager pa)
        {
            mainWindow = mw;
            paternosterlager = pa;


            SpsStatus = "-";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {
              
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



        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members

    }
}
