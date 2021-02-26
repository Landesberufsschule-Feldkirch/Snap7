using DisplayPlc.Config;
using Kommunikation;
using PlcDatenTypen;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DisplayPlc
{
    public partial class DisplayPlc
    {
        public ViewModel.ViewModel ViewModel { get; set; }
        public Uint PlcEingaenge { get; set; }
        public Uint PlcAusgaenge { get; set; }
        public bool FensterAktiv { get; set; }
        public DisplayPlc(Datenstruktur datenstruktur, ManualMode.ManualMode manualMode)
        {
            PlcAusgaenge = new Uint("16#FFFF");
            PlcEingaenge = new Uint("16#FFFF");

            ViewModel = new ViewModel.ViewModel(datenstruktur, this);

            var plcGrid = new Grid { Name = "PlcGrid", MaxWidth = 700, MaxHeight = 1200, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            Content = plcGrid;

            PlcZeichnen(plcGrid, BackgroundProperty, manualMode);

            DataContext = ViewModel;
            Closing += (_, e) =>
            {
                e.Cancel = true;
                Schliessen();
            };
        }
        public void SetBetriebsartProjekt(Datenstruktur datenstruktur)
        {
            if (datenstruktur.BetriebsartProjekt == BetriebsartProjekt.AutomatischerSoftwareTest && GetPlcConfig != null)
            {
                PlcAusgaenge = GetPlcConfig.PlcBelegung.Ausgaenge;
                PlcEingaenge = GetPlcConfig.PlcBelegung.Eingaenge;
                return;
            }

            PlcAusgaenge = new Uint("16#FFFF");
            PlcEingaenge = new Uint("16#FFFF");
        }
        public void EventBeschriftungAktualisieren(Datenstruktur datenstruktur)
        {
            GetPlcConfig = new GetPlcConfig(new FileInfo(datenstruktur.TestProjektOrdner));
            PlcAusgaenge = GetPlcConfig.PlcBelegung.Ausgaenge;
            PlcEingaenge = GetPlcConfig.PlcBelegung.Eingaenge;
        }
        public void Schliessen()
        {
            FensterAktiv = false;
            Hide();
        }
        public void Oeffnen()
        {
            Show();
            Title = "PLC";
            MaxWidth = 700;
            FensterAktiv = true;
        }
    }
}