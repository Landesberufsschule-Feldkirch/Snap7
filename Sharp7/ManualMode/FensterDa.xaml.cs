using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using ManualMode.Commands;
using ManualMode.ViewModel;

namespace ManualMode
{
    public partial class FensterDa
    {
        public FensterDa(Model.ConfigDa configDa,  ManualViewModel mvm)
        {
            var manualViewModel = mvm;
            InitializeComponent();


            var posTastenX = 10;
            var posToggelnX = 100;
            var posLabelX = 200;
            var posKommentarX = 300;

            var posUeberschriftY = 10;
            var posY = 40;
            var abstand = 30;

            var labelTasten = new Label
            {
                Content = "Tasten",
                FontFamily = new FontFamily("Arial"),
                FontSize = 12
            };
            Canvas.SetLeft(labelTasten, posTastenX);
            Canvas.SetTop(labelTasten, posUeberschriftY);
            Canvas.Children.Add(labelTasten);

            var labelTogeln = new Label
            {
                Content = "Toggeln",
                FontFamily = new FontFamily("Arial"),
                FontSize = 12
            };
            Canvas.SetLeft(labelTogeln, posToggelnX);
            Canvas.SetTop(labelTogeln, posUeberschriftY);
            Canvas.Children.Add(labelTogeln);

            var LaufenderZaehler = 0;

            foreach (var config in configDa.DigitaleAusgaenge)
            {
                var bezeichnung = config.StartByte + "." + config.StartBit;

                if (config.LaufendeNr != LaufenderZaehler)
                {
                    throw new InvalidDataException($"{nameof(FensterDi)} invalid {config.LaufendeNr} ");
                }
                //   < Button Grid.Column = "1" Grid.Row = "3" Content = "0.0" Background = "{Binding Visu.ColorTasten[1]}"  ClickMode = "{Binding Visu.ClickModeTasten[1]}"  Command = "{Binding BtnTasten}"  CommandParameter = "1" />

                var buttonTasten = new Button
                {
                    Content = bezeichnung,
                    CommandParameter = config,
                    Command = new RelayCommand(manualViewModel.ManualVisuAnzeigen.TastenDa),
                    ClickMode = manualViewModel.ManualVisuAnzeigen.ClickModeTasten[config.LaufendeNr],
                    Background = manualViewModel.ManualVisuAnzeigen.FarbeTastenToggelnDa[config.LaufendeNr],
                    Width = 50,
                    Height = 20,
                    FontFamily = new FontFamily("Arial"),
                    FontSize = 12
                };

                Canvas.SetLeft(buttonTasten, posTastenX);
                Canvas.SetTop(buttonTasten, posY);
                Canvas.Children.Add(buttonTasten);

                //  < Button Grid.Column = "3" Grid.Row = "3" Content = "0.0" Background = "{Binding Visu.ColorToggeln[1]}"                                                Command = "{Binding BtnToggeln}" CommandParameter = "1" />


                var buttonToggeln = new Button
                {
                    Content = bezeichnung,
                    CommandParameter = config,
                    Command = new RelayCommand(manualViewModel.ManualVisuAnzeigen.ToggelnDa),
                    Background = manualViewModel.ManualVisuAnzeigen.FarbeTastenToggelnDa[config.LaufendeNr],
                    Width = 50,
                    Height = 20,
                    FontFamily = new FontFamily("Arial"),
                    FontSize = 12
                };

                Canvas.SetLeft(buttonToggeln, posToggelnX);
                Canvas.SetTop(buttonToggeln, posY);
                Canvas.Children.Add(buttonToggeln);


                var label = new Label
                {
                    Content = config.Bezeichnung,
                    FontFamily = new FontFamily("Arial"),
                    FontSize = 12
                };

                Canvas.SetLeft(label, posLabelX);
                Canvas.SetTop(label, posY);
                Canvas.Children.Add(label);



                var kommentar = new Label
                {
                    Content = config.Kommentar,
                    FontFamily = new FontFamily("Arial"),
                    FontSize = 12
                };

                Canvas.SetLeft(kommentar, posKommentarX);
                Canvas.SetTop(kommentar, posY);
                Canvas.Children.Add(kommentar);


                posY += abstand;
                LaufenderZaehler++; // für ClickModeTasten
            }
        }
    }
}
