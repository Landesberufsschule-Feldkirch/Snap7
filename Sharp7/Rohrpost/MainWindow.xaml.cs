namespace Rohrpost;

public partial class MainWindow
{
    private Logikfunktionen _logikfunktionen;
    private DatenRangieren _datenRangieren;

    public MainWindow()
    {
        _logikfunktionen = new Logikfunktionen(this);
        _datenRangieren = new DatenRangieren(this);

        InitializeComponent();
    }
}