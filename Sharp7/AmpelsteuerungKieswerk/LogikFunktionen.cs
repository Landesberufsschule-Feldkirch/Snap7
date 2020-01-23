using System;
using System.Threading;
using System.Windows.Controls;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {
        public static object lockit = new object();
        public bool B1, B2, B3, B4;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                Dispatcher.Invoke(() =>
                                   {
                                       lock (lockit)
                                       {
                                           B1 = false;
                                           B2 = false;
                                           B3 = false;
                                           B4 = false;

                                           foreach (Button btn in gAlleButton)
                                           {
                                               var lkw = btn.Tag as LKW;
                                               var (b1, b2, b3, b4) = lkw.LastwagenFahren();
                                               B1 |= b1;
                                               B2 |= b2;
                                               B3 |= b3;
                                               B4 |= b4;
                                           }
                                           OnSensoreChanged(new SensorenZustandArgs(B1, B2, B3, B4));
                                       }
                                   });
                Thread.Sleep(10);
            }
        }

        public event EventHandler<SensorenZustandArgs> SensorenChanged;

        protected virtual void OnSensoreChanged(SensorenZustandArgs e)
        {
            SensorenChanged?.Invoke(this, e);
        }
    }
}