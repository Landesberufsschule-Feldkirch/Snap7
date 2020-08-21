using System.Windows.Input;

namespace Nadeltelegraph.SetManual.View
{
    public class View
    {
        private readonly Model.Nadeltelegraph _nadeltelegraph;
        public Model.Nadeltelegraph Nadeltelegraph => _nadeltelegraph;
        public Visu Visu { get; set; }
        public View(MainWindow mainWindow)
        {
            _nadeltelegraph = new Model.Nadeltelegraph();
            Visu = new Visu(mainWindow, _nadeltelegraph);
        }

        private ICommand _btnP1L;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnP1L => _btnP1L ?? (_btnP1L = new Command.Command(p => Visu.SetP1L(), p => true));
    }
}