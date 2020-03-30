namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{


    public class ViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat hydraulikaggregat;

        public ViewModel(MainWindow mainWindow)
        {
            hydraulikaggregat = new LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat ();
            ViAnzeige = new VisuAnzeigen(mainWindow, hydraulikaggregat);
        }

        public LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat Hydraulikaggregat{ get { return hydraulikaggregat; } }



    }
}
