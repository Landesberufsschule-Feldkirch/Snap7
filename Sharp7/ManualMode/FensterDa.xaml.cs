using ManualMode.Commands;
using ManualMode.ViewModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManualMode
{
    public partial class FensterDa
    {
        public FensterDa(Model.ConfigDa configDa, ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            //DataContext = manViewModel.ManVisuAnzeigen;
            DataContext = manViewModel;

            const int posTastenX = 10;
            const int posToggelnX = 100;
            const int posLabelX = 200;
            const int posKommentarX = 300;

            const int posUeberschriftY = 10;
            const int abstand = 30;

            var posY = 40;

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

            var laufenderZaehler = 0;

            foreach (var config in configDa.DigitaleAusgaenge)
            {
                var buttonBeschriftung = config.StartByte + "." + config.StartBit;

                if (config.LaufendeNr == laufenderZaehler)
                {
                    //   < Button Grid.Column = "1" Grid.Row = "3" Content = "0.0" BackgroundButton = "{Binding Visu.ColorTasten[1]}"  ClickMode = "{Binding Visu.ClickModeTasten[1]}"  Command = "{Binding BtnTasten}"  CommandParameter = "1" />
                 
                    /*
                    var buttonTasten = new Button
                    {
                        Content = buttonBeschriftung,
                        CommandParameter = config,
                        Command = new RelayCommand(manViewModel.ManVisuAnzeigen.TastenDa),
                        ClickMode = manViewModel.ManVisuAnzeigen.ClickModeTasten[laufenderZaehler],
                      //  Background = manViewModel.ManVisuAnzeigen.FarbeTastenToggelnDa[laufenderZaehler],
                        Width = 50,
                        Height = 20,
                        FontFamily = new FontFamily("Arial"),
                        FontSize = 12
                    };
                    if (!used)
                    {
                        var buttonTasten2 = new Button
                        {
                            Name = "BackgroundButton",
                            Content = buttonBeschriftung,
                            CommandParameter = config,
                            Command = new RelayCommand(manViewModel.ManVisuAnzeigen.TastenDa),
                            ClickMode = manViewModel.ManVisuAnzeigen.ClickModeTasten[laufenderZaehler],
                            //  Background = manViewModel.ManVisuAnzeigen.FarbeTastenToggelnDa[laufenderZaehler],
                            Width = 50,
                            Height = 20,
                            FontFamily = new FontFamily("Arial"),
                            FontSize = 12
                        };
                        buttonTasten2.SetBinding(BackgroundProperty, "BackgroundButton");
                        mvm.ManVisuAnzeigen.BackgroundButton = new SolidColorBrush(Colors.OrangeRed);

                        Canvas.SetLeft(buttonTasten, posTastenX);
                        Canvas.SetTop(buttonTasten, posY);
                        Canvas.Children.Add(buttonTasten2);
                    mvm.ManVisuAnzeigen.OnPropertyChanged("BackgroundButton");
                    }
                    used = true;
                    
                    var binding = new Binding
                    {
                        Source = mvm,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        Path = new PropertyPath("BackgroundButton"),
                        Mode = BindingMode.Default
                    };
                    
                    BindingOperations.SetBinding(buttonTasten, BackgroundProperty, binding);
                    Canvas.SetLeft(buttonTasten, posTastenX);
                    Canvas.SetTop(buttonTasten, posY);
                    Canvas.Children.Add(buttonTasten);
                    */




                    //  < Button Grid.Column = "3" Grid.Row = "3" Content = "0.0" BackgroundButton = "{Binding Visu.ColorToggeln[1]}"  
                    var buttonToggeln = new Button
                    {
                        Content = buttonBeschriftung,
                        CommandParameter = config,
                        Command = new RelayCommand(manViewModel.ManVisuAnzeigen.ToggelnDa),
                        Background =  manViewModel.ManVisuAnzeigen.FarbeTastenToggelnDa[laufenderZaehler],
                        Width = 50,
                        Height = 20,
                        FontFamily = new FontFamily("Arial"),
                        FontSize = 12
                    };

                    Canvas.SetLeft(buttonToggeln, posToggelnX);
                    Canvas.SetTop(buttonToggeln, posY);
                    Canvas.Children.Add(buttonToggeln);



                    var labelBezeichnung = new Label
                    {
                        Content = config.Bezeichnung,
                        FontFamily = new FontFamily("Arial"),
                        FontSize = 12
                    };

                    Canvas.SetLeft(labelBezeichnung, posLabelX);
                    Canvas.SetTop(labelBezeichnung, posY);
                    Canvas.Children.Add(labelBezeichnung);


                    var labelKommentar = new Label
                    {
                        Content = config.Kommentar,
                        FontFamily = new FontFamily("Arial"),
                        FontSize = 12
                    };

                    Canvas.SetLeft(labelKommentar, posKommentarX);
                    Canvas.SetTop(labelKommentar, posY);
                    Canvas.Children.Add(labelKommentar);


                    posY += abstand;
                    laufenderZaehler++; // für ClickModeTasten
                }
                else
                {
                    throw new InvalidDataException($"{nameof(FensterDi)} invalid {config.LaufendeNr} ");
                }
            }
        }
    }
}