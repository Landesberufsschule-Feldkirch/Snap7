using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LaborLinearachse.ViewModel;

public partial class ViewModel
{



    internal void Taster(object id)
    {
        if (id is not string ascii) return;

        var tasterId = short.Parse(ascii);
        var gedrueckt = ClickModeButton(tasterId);

        switch (tasterId)
        {
            case 11: _linearachse.S1 = gedrueckt; break;
            case 12: _linearachse.S2 = !gedrueckt; break;
            case 13: _linearachse.S3 = gedrueckt; break;
            case 14: _linearachse.S4 = gedrueckt; break;
            case 15: _linearachse.S5 = gedrueckt; break;
            case 16: _linearachse.S6 = gedrueckt; break;
            case 17: _linearachse.S7 = gedrueckt; break;
            case 18: _linearachse.S8 = gedrueckt; break;
            case 19: _linearachse.S9 = !gedrueckt; break;
            default: throw new ArgumentOutOfRangeException(nameof(id));
        }
    }
    internal void Schalter(object id)
    {
        if (id is not string ascii) return;

        var schalterId = short.Parse(ascii);

        switch (schalterId)
        {
            case 20:
                _linearachse.S10 = !_linearachse.S10;
                _linearachse.S11 = !_linearachse.S10;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(id));
        }
    }


    #region Taster/Schalter
    public bool ClickModeButton(int tasterId)
    {
        if (ClkMode[tasterId] == ClickMode.Press)
        {
            ClkMode[tasterId] = ClickMode.Release;
            return true;
        }

        ClkMode[tasterId] = ClickMode.Press;
        return false;
    }

    private ObservableCollection<ClickMode> _clkMode = new();
    public ObservableCollection<ClickMode> ClkMode
    {
        get => _clkMode;
        set
        {
            _clkMode = value;
            OnPropertyChanged(nameof(ClkMode));
        }
    }
    #endregion Taster/Schalter

}