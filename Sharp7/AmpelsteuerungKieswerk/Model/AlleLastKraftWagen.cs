﻿namespace AmpelsteuerungKieswerk.Model
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class AlleLastKraftWagen
    {

        private readonly MainWindow mainWindow;


        public readonly List<LKW> AlleLkw = new List<LKW>();
        private readonly int AnzahlLkw;

        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool B3 { get; set; }
        public bool B4 { get; set; }


        public VisuAnzeigen ViAnzeige { get; set; }

        public AlleLastKraftWagen(MainWindow mw)
        {
            mainWindow = mw;
            ViAnzeige = new VisuAnzeigen();


            AlleLkw.Add(new LKW(AnzahlLkw++));
            AlleLkw.Add(new LKW(AnzahlLkw++));
            AlleLkw.Add(new LKW(AnzahlLkw++));
            AlleLkw.Add(new LKW(AnzahlLkw++));
            AlleLkw.Add(new LKW(AnzahlLkw++));

            System.Threading.Tasks.Task.Run(() => AlleLastKraftWagenTask());
        }

        private void AlleLastKraftWagenTask()
        {
            while (true)
            {

                B1 = false;
                B2 = false;
                B3 = false;
                B4 = false;
                foreach (LKW lkw in AlleLkw)
                {
                    var (b1, b2, b3, b4) = lkw.LastwagenFahren();
                    B1 |= b1;
                    B2 |= b2;
                    B3 |= b3;
                    B4 |= b4;
                }

                AnzeigeAktualisieren();
                Thread.Sleep(10);
            }
        }

        private void AnzeigeAktualisieren()
        {
            ViAnzeige.FarbeB1(B1);
            ViAnzeige.FarbeB2(B2);
            ViAnzeige.FarbeB3(B3);
            ViAnzeige.FarbeB4(B4);

            ViAnzeige.PositionLkw1(AlleLkw[0].AktuellePosition);
            ViAnzeige.PositionLkw2(AlleLkw[1].AktuellePosition);
            ViAnzeige.PositionLkw3(AlleLkw[2].AktuellePosition);
            ViAnzeige.PositionLkw4(AlleLkw[3].AktuellePosition);
            ViAnzeige.PositionLkw5(AlleLkw[4].AktuellePosition);


        }

        internal void TasterLkw1() { AlleLkw[0].Losfahren(); }
        internal void TasterLkw2() { AlleLkw[1].Losfahren(); }
        internal void TasterLkw3() { AlleLkw[2].Losfahren(); }
        internal void TasterLkw4() { AlleLkw[3].Losfahren(); }
        internal void TasterLkw5() { AlleLkw[4].Losfahren(); }

        internal void TasterLinksParken() { foreach (LKW lkw in AlleLkw) lkw.LinksParken(); }
        internal void TasterRechtsParken() { foreach (LKW lkw in AlleLkw) lkw.RechtsParken(); }
    }
}

