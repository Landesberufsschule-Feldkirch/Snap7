using DigitalerZwillingMitAutoTests;

namespace Kata;

public partial class MainWindow
{
    public DtAutoTests DtAutoTests { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        DtAutoTests = new DtAutoTests();
        DtAutoTests.SetInputOutput(1, 1, 0, 0);
        DtAutoTests.SetVersionLokal("Kata" + " " + "V2.0");
        DtAutoTests.SetConfigPlc("./ConfigPlc");
        DtAutoTests.SetConfigTests("./ConfigTests/");
        DtAutoTests.SetTabItemAutoTests(TabItemAutomatischerSoftwareTest);
        DtAutoTests.Starten();

        var kata = new Model.Kata();
        kata.SetDtAutoTests(DtAutoTests);

        var viewModel = new ViewModel.ViewModel(this);
        viewModel.SetModel(kata);
        DataContext = viewModel;

        Closing += (_, e) =>
        {
            e.Cancel = true;
            Schliessen();
        };
    }
}