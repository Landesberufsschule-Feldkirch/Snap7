namespace LaborLinearachse.ViewModel;

public partial class ViewModel
{

    private double _positionSchlitten;
    public double PositionSchlitten
    {
        get => _positionSchlitten;
        set
        {
            _positionSchlitten = value;
            OnPropertyChanged(nameof(PositionSchlitten));
        }
    }

}
