using Kommunikation;
using PlcDatenTypen;

namespace Schleifmaschine;

public class DatenRangieren
{
    private readonly ViewModel.ViewModel _viewModel;
    private IPlc _plc;

    private enum BitPosAusgang
    {
        P1 = 0,     // 0.0  Meldeleuchte "Taster langsam"
        P2 = 1,     // 0.1  Meldeleuchte "Taster schnell"
        P3 = 2,     // 0.2  Meldeleuchte "Störung"
        Q1 = 3,     // 0.3  Schleifmaschine Langsam
        Q2 = 4      // 0.4  Schleifmaschine Schnell
    }

    private enum BitPosEingang
    {
        B1 = 0,     // 0.0 Störung Übersynchron
        F1 = 1,     // 0.1 Thermorelais langsame Drehzahl
        F2 = 2,     // 0.2 Thermorelais schnelle Drehzahl
        S0 = 3,     // 0.3 Taster ( ⓪ ) 
        S1 = 4,     // 0.4 Taster ( Ⅰ )  
        S2 = 5,     // 0.5 Taster ( Ⅱ )  
        S3 = 6,     // 0.6 Not-Halt
        S4 = 7      // 0.7 Störung quittieren
    }

    public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
    {
        if (eingaengeRangieren)
        {
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Schleifmaschine.B1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Schleifmaschine.F1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F2, _viewModel.Schleifmaschine.F2);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S0, _viewModel.Schleifmaschine.S0);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Schleifmaschine.S1);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Schleifmaschine.S2);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Schleifmaschine.S3);
            _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Schleifmaschine.S4);
        }

        _viewModel.Schleifmaschine.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
        _viewModel.Schleifmaschine.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
        _viewModel.Schleifmaschine.P3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
        _viewModel.Schleifmaschine.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
        _viewModel.Schleifmaschine.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);

        _plc.SetIntAt(datenstruktur.AnalogInput, 0, Simatic.Analog_2_Int16(_viewModel.Schleifmaschine.DrehzahlSchleifmaschine, 10000));
    }
    public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    public void ReferenzUebergeben(IPlc plc) => _plc = plc;
}