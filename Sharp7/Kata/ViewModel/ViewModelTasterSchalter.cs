using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Kata.ViewModel;

public partial class ViewModel
{
    internal void Taster(object id)
    {
        if (id is not string ascii) return;

        var tasterId = short.Parse(ascii);
        var gedrueckt = ClickModeButton(tasterId);

        switch (tasterId)
        {
            case 11:
                _kata.S1 = gedrueckt;
                break;
            case 12:
                _kata.S2 = gedrueckt;
                break;
            case 13:
                _kata.S3 = !gedrueckt;
                break;
            case 14:
                _kata.S4 = !gedrueckt;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(id));
        }
    }

    internal void Schalter(object id)
    {
        if (id is not string ascii) return;

        var schalterId = short.Parse(ascii);

        switch (schalterId)
        {
            case 15:
                _kata.S5 = !_kata.S5;
                break;
            case 16:
                _kata.S6 = !_kata.S6;
                break;
            case 17:
                _kata.S7 = !_kata.S7;
                break;
            case 18:
                _kata.S8 = !_kata.S8;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(id));
        }
    }

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
}