namespace BehaelterSteuerung.Model
{
    using System.Collections.Generic;
    using System.Threading;

    // ReSharper disable once UnusedMember.Global
    public class AlleBehaelter
    {

        public bool P1 { get; set; }
        public List<Behaelter> AlleMeineBehaelter { get; set; }

        private bool _automatikModusAktiv;

        private string _aktuellePermutation;


        public AlleBehaelter()
        {
            _aktuellePermutation = "0000";

            AlleMeineBehaelter = new List<Behaelter>
            {
                new Behaelter(0.2), new Behaelter(0.4), new Behaelter(0.6), new Behaelter(0.8)
            };


            System.Threading.Tasks.Task.Run(AlleBehaelterTask);
        }

        private void AlleBehaelterTask()
        {
            while (true)
            {

                var automatikModusNochAktiv = false;

                foreach (var beh in AlleMeineBehaelter)
                {
                    beh.PegelUeberwachen();
                    if (!_automatikModusAktiv) continue;
                    beh.VentilUnten = beh.Pegel < 0.99 && beh.Pegel > 0.01;
                    if (beh.Pegel > 0.01) automatikModusNochAktiv = true;

                }
                if (_automatikModusAktiv && !automatikModusNochAktiv)
                {
                    // AlleAutomatikKnoepfeAktivieren();
                    _automatikModusAktiv = false;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        // ReSharper disable once UnusedMember.Global
        internal void VentilQ2() => AlleMeineBehaelter[0].VentilUntenUmschalten();
        // ReSharper disable once UnusedMember.Global
        internal void VentilQ4() => AlleMeineBehaelter[1].VentilUntenUmschalten();
        // ReSharper disable once UnusedMember.Global
        internal void VentilQ6() => AlleMeineBehaelter[2].VentilUntenUmschalten();
        // ReSharper disable once UnusedMember.Global
        internal void VentilQ8() => AlleMeineBehaelter[3].VentilUntenUmschalten();


        public void AutomatikBetriebStarten(string reihenfolge)
        {
            if (_aktuellePermutation == reihenfolge) return;
            _automatikModusAktiv = true;
            _aktuellePermutation = reihenfolge;

            EndleerreihenfolgeZuordnen(_aktuellePermutation[0], 1.2);
            EndleerreihenfolgeZuordnen(_aktuellePermutation[1], 2.4);
            EndleerreihenfolgeZuordnen(_aktuellePermutation[2], 3.6);
            EndleerreihenfolgeZuordnen(_aktuellePermutation[3], 4.8);
        }

        private void EndleerreihenfolgeZuordnen(char behaelter, double menge)
        {
            var b = behaelter - '1';
            AlleMeineBehaelter[b].AutomatikmodusStarten(menge);
        }

        public bool AutomatikModusAktiv() => _automatikModusAktiv;
    }
}
