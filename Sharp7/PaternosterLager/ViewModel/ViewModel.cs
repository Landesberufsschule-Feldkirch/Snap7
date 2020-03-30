namespace PaternosterLager.ViewModel
{
    public class ViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly PaternosterLager.Model.Paternosterlager paternosterlager;

        public ViewModel(MainWindow mainWindow)
        {
            paternosterlager = new Model.Paternosterlager();
            ViAnzeige = new VisuAnzeigen(mainWindow, paternosterlager);
        }

        public Model.Paternosterlager Paternosterlager{ get { return paternosterlager; } }



    }
}
