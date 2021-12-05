using DigitalerZwillingMitAutoTests;

namespace LaborLinearachse;

public partial class MainWindow
{
    public DtAutoTests DtAutoTests { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        DtAutoTests = new DtAutoTests();
        DtAutoTests.SetInputOutput(2, 1, 0, 0);
        DtAutoTests.SetVersionLokal("Labor Linearachse" + " " + "V2.0");
        DtAutoTests.SetConfigPlc("./ConfigPlc");
        DtAutoTests.SetConfigTests("./ConfigTests/");
        DtAutoTests.SetTabItemAutoTests(TabItemAutomatischerSoftwareTest);
        DtAutoTests.Starten();

        var linearachse = new Model.Linearachse();
        linearachse.SetDtAutoTests(DtAutoTests);

        var viewModel = new ViewModel.ViewModel(this);
        viewModel.SetModel(linearachse);
        DataContext = viewModel;

        Closing += (_, e) =>
        {
            e.Cancel = true;
            Schliessen();
        };
    }
}