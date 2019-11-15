using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PlcConnect
{
    public partial class MainWindow
    {
        public void BilderEinlesen()
        {
            List<string> BilderListe = new List<string>();

            DirectoryInfo Pfad = new DirectoryInfo(@"Bilder");

            foreach (var file in Pfad.GetFiles("*"))
            {
                BilderListe.Add(file.Name);
            }

        }

    }
}
