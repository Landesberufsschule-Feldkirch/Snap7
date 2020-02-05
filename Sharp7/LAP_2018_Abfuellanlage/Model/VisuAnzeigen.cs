namespace LAP_2018_Abfuellanlage.Model
{
    using System.ComponentModel;
    using System.Windows;


    public class VisuAnzeigen : INotifyPropertyChanged
    {


        public VisuAnzeigen()
        {
            //
        }


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members

    }
}
