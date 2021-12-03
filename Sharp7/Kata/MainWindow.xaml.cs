using DigitalerZwillingMitAutoTests;


namespace Kata;

public partial class MainWindow
{
    public Model.Kata Kata { get; set; }
    public DtAutoTests DtAutoTests { get; set; }
      

    public MainWindow()
    {
        const string versionText = "Kata";
        const string versionNummer = "V2.0";

        const int anzByteDigInput = 1;
        const int anzByteDigOutput = 1;
        const int anzByteAnalogInput = 0;
        const int anzByteAnalogOutput = 0;

        var viewModel = new ViewModel.ViewModel(this);
        InitializeComponent();
        DataContext = viewModel;


        DtAutoTests = new DtAutoTests();
        DtAutoTests.SetVersionLokal(versionText + " " + versionNummer);
        DtAutoTests.SetTabItemAutoTests(TabItemAutomatischerSoftwareTest);
        DtAutoTests.SetConfigPlc("./ConfigPlc");
        DtAutoTests.SetConfigTests("./ConfigTests/");
        DtAutoTests.SetInputOutput(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);

        DtAutoTests.Starten();

        Kata = new Model.Kata();
        Kata.SetDtAutoTests(DtAutoTests);

        viewModel.SetModel(Kata);

        Closing += (_, e) =>
        {
            e.Cancel = true;
            Schliessen();
        };
    }
}