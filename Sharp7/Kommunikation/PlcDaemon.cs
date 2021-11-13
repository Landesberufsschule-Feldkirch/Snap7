using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;

namespace Kommunikation
{
    public class PlcDaemon
    {
        public IPlc Plc { get; set; }


        private readonly IpAdressenSiemens _spsS7_1200;
        private readonly IpAdressenBeckhoff _spsCx9020;
        private PlcDaemonStatus _status;

        private readonly Action<Datenstruktur, bool> _callbackRangieren;

        private readonly Datenstruktur _datenstruktur;


        private enum PlcDaemonStatus
        {
            SpsPingen = 0,
            SpsBeckhoff = 1,
            SpsSiemens = 2,
            SpsAktiv = 3
        }

        public PlcDaemon(Datenstruktur datenstruktur, Action<Datenstruktur, bool> cbRangieren)
        {
            Plc = new KeineSps();
            _datenstruktur = datenstruktur;
            _callbackRangieren = cbRangieren;

            try
            {
                _spsS7_1200 = JsonConvert.DeserializeObject<IpAdressenSiemens>(File.ReadAllText("IpAdressenSiemens.json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden: IpAdressenSiemens.json" + " --> " + ex);
            }

            try
            {
                _spsCx9020 = JsonConvert.DeserializeObject<IpAdressenBeckhoff>(File.ReadAllText("IpAdressenBeckhoff.json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden: IpAdressenBeckhoff.json" + " --> " + ex);
            }

            System.Threading.Tasks.Task.Run(PlcDaemonTask);
        }

        private void PlcDaemonTask()
        {

            Ping pingBeckhoff = new Ping();
            Ping pingSiemens = new Ping();

            while (true)
            {
                switch (_status)
                {
                    case PlcDaemonStatus.SpsPingen:
                        try
                        {
                            var replyBeckhoff = pingBeckhoff.Send(_spsCx9020.IpAdresse);
                            var replySiemens = pingBeckhoff.Send(_spsS7_1200.Adress);

                            if (replyBeckhoff.Status == IPStatus.Success) _status = PlcDaemonStatus.SpsBeckhoff;
                            if (replySiemens.Status == IPStatus.Success) _status = PlcDaemonStatus.SpsSiemens;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Problem beim pingen:" + " --> " + ex);
                        }
                        break;

                    case PlcDaemonStatus.SpsBeckhoff:
                        Plc = new Cx9020(_spsCx9020, _datenstruktur, _callbackRangieren);
                        _status = PlcDaemonStatus.SpsAktiv;
                        break;

                    case PlcDaemonStatus.SpsSiemens:
                        Plc = new S71200(_spsS7_1200, _datenstruktur, _callbackRangieren);
                        _status = PlcDaemonStatus.SpsAktiv;
                        break;

                    case PlcDaemonStatus.SpsAktiv:
                        break;
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}