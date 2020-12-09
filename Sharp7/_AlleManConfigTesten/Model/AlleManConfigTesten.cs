using System;

namespace _AlleManConfigTesten.Model
{
    public class AlleManConfigTesten
    {
        private MainWindow _mainWindow;


        public AlleManConfigTesten(MainWindow mainWindow)
        {



        }

        public void Anzeigen(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        internal void Anzeigen()
        {
            throw new NotImplementedException();
        }
    }
}