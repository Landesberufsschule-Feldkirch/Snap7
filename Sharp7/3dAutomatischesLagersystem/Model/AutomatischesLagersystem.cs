namespace AutomatischesLagersystem.Model
{
    public class AutomatischesLagersystem
    {
        readonly MainWindow mainWindow;

      
        public AutomatischesLagersystem(MainWindow mw)
        {
            mainWindow = mw;

       
        }

        public char Zeichen { get; internal set; }

      
    }
}
