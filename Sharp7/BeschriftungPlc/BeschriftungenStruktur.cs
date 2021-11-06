using System;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace BeschriftungPlc
{
    public class BeschriftungenStruktur
    {
        public Da DaBeschriftungen { get; set; }
        public Di DiBeschriftungen { get; set; }

        internal void PlcDaBeschriftungen(string pfad)
        {
            try
            {
                DaBeschriftungen = JsonConvert.DeserializeObject<Da>(File.ReadAllText(pfad));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden:" + pfad + " --> " + ex);
            }
        }

        public void PlcDiBeschriftungen(string pfad)
        {
            try
            {
                DiBeschriftungen = JsonConvert.DeserializeObject<Di>(File.ReadAllText(pfad));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden:" + pfad + " --> " + ex);
            }
        }
    }
}