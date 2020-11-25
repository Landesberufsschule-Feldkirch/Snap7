using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public partial class FensterDa
    {
        public bool DatenByteweiseEingeben { get; set; }

        public FensterDa(ConfigDa configDa, ManualViewModel mvm)
        {
            DatenByteweiseEingeben = false;
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var anzahlBit = DigitaleAusgaengeDatenLesen(configDa, manViewModel);
            CreateGridDa(anzahlBit, manViewModel);
        }

        private int DigitaleAusgaengeDatenLesen(ConfigDa configDa, ManualViewModel manualViewModel)
        {
            var anzahlBit = 0;

            foreach (var config in configDa.DigitaleAusgaenge)
            {
                if (config.LaufendeNr == anzahlBit)
                {
                    switch (config.Type)
                    {
                        case PlcEinUndAusgaengeTypen.BitmusterByte:
                            DatenByteweiseEingeben = true;
                            manualViewModel.ManVisuAnzeigen.VisibilityDa[config.LaufendeNr] = Visibility.Visible;
                            manualViewModel.ManVisuAnzeigen.BezeichnungDa[config.LaufendeNr] = config.Bezeichnung;
                            manualViewModel.ManVisuAnzeigen.KommentarDa[config.LaufendeNr] = config.Kommentar;
                            break;
                        case PlcEinUndAusgaengeTypen.Default:
                            manualViewModel.ManVisuAnzeigen.VisibilityDa[config.LaufendeNr] = Visibility.Visible;
                            manualViewModel.ManVisuAnzeigen.BezeichnungDa[config.LaufendeNr] = config.Bezeichnung;
                            manualViewModel.ManVisuAnzeigen.KommentarDa[config.LaufendeNr] = config.Kommentar;
                            break;
                        case PlcEinUndAusgaengeTypen.Ascii:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    anzahlBit++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(FensterDi)} invalid {config.LaufendeNr} ");
                }
            }

            return anzahlBit + 1;
        }
        private void CreateGridDa(int anzahlBit, ManualViewModel manualViewModel)
        {
            var gridDa = new Grid
            {
                Name = "GridDa",
                Width = 600
            };

            Content = gridDa;

            gridDa.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            gridDa.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            gridDa.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            gridDa.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            var rowDefCollection = new ObservableCollection<RowDefinition>();
            for (var i = 0; i < anzahlBit; i++)
            {
                rowDefCollection.Add(new RowDefinition { Height = new GridLength(45) });
                gridDa.RowDefinitions.Add(rowDefCollection[i]);
            }

            if (DatenByteweiseEingeben)
            {
                TextZeichnen("Wert", HorizontalAlignment.Center, 0, 0, gridDa);
                TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 2, 0, gridDa);
                TextZeichnen("Kommentar", HorizontalAlignment.Left, 3, 0, gridDa);

                for (var vbyte = 0; vbyte < anzahlBit; vbyte++)
                {
                    TextboxByteZeichnen(vbyte, 0, 1 + vbyte, gridDa);
                    BezeichnungByteZeichnen(vbyte, 2, 1 + vbyte, gridDa);
                    KommentarByteZeichnen(vbyte, 3, 1 + vbyte, gridDa);
                }
            }
            else
            {
                TextZeichnen("Tasten", HorizontalAlignment.Center, 0, 0, gridDa);
                TextZeichnen("Toggeln", HorizontalAlignment.Center, 1, 0, gridDa);
                TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 2, 0, gridDa);
                TextZeichnen("Kommentar", HorizontalAlignment.Left, 3, 0, gridDa);

                for (var vbyte = 0; vbyte < 10; vbyte++)
                {
                    for (var vBit = 0; vBit < 8; vBit++)
                    {
                        if (8 * vbyte + vBit >= anzahlBit) continue;

                        ButtonTastenZeichnen(vbyte, vBit, 0, 1 + vbyte * 8 + vBit, gridDa, manualViewModel);
                        ButtonToggelnZeichnen(vbyte, vBit, 1, 1 + vbyte * 8 + vBit, gridDa, manualViewModel);
                        BezeichnungBitZeichnen(vbyte, vBit, 2, 1 + vbyte * 8 + vBit, gridDa);
                        KommentarBitZeichnen(vbyte, vBit, 3, 1 + vbyte * 8 + vBit, gridDa);
                    }
                }
            }
        }


        private static void ButtonTastenZeichnen(int vbyte, int vbit, int x, int y, Panel panel, ManualViewModel manualViewModel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var buttonTasten = new Button
            {
                Content = vbyte + "." + vbit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnTasten,
                CommandParameter = parameterNummer.ToString()
            };

            buttonTasten.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeTastenToggelnDa[" + parameterNummer + "]"));
            buttonTasten.SetBinding(ButtonBase.ClickModeProperty, new Binding("ManVisuAnzeigen.ClickModeTasten[" + parameterNummer + "]"));
            buttonTasten.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(buttonTasten, x);
            Grid.SetRow(buttonTasten, y);

            panel.Children.Add(buttonTasten);
        }
        private static void ButtonToggelnZeichnen(int vbyte, int vbit, int x, int y, Panel panel, ManualViewModel manualViewModel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var buttonToggeln = new Button
            {
                Content = vbyte + "." + vbit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnToggeln,
                CommandParameter = parameterNummer.ToString()
            };

            buttonToggeln.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeTastenToggelnDa[" + parameterNummer + "]"));
            buttonToggeln.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(buttonToggeln, x);
            Grid.SetRow(buttonToggeln, y);

            panel.Children.Add(buttonToggeln);
        }
        private static void BezeichnungBitZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var bezeichnung = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.BezeichnungDa[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            panel.Children.Add(bezeichnung);
        }
        private static void KommentarBitZeichnen(int vbyte, int vbit, int x, int y, Panel panel)
        {
            var parameterNummer = 8 * vbyte + vbit;

            var kommentar = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                Padding = new Thickness(10, 5, 5, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarDa[" + parameterNummer + "]"));
            kommentar.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            panel.Children.Add(kommentar);
        }

        private static void TextboxByteZeichnen(int vbyte, int x, int y, Panel panel)
        {
            var parameterNummer = vbyte;

            var textBox = new TextBox
            {
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            };



            //textBox.SetBinding(TextBox.TextProperty, new Binding( "ManVisuAnzeigen.WertDa[" + parameterNummer + "], Mode=TwoWay, UpdateSourceTrigger=PropertyChanged"  ) );
            //textBox.SetBinding(TextBox.TextProperty, new Binding("ManVisuAnzeigen.WertDa[" + parameterNummer + "], UpdateSourceTrigger=PropertyChanged"));
            //textBox.SetBinding(TextBox.TextProperty, new Binding("ManVisuAnzeigen.WertDa[" + parameterNummer + "]"));


            var textBinding = new Binding
            {
                Path = new PropertyPath("ManVisuAnzeigen.WertDa[" + parameterNummer + "]"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            textBox.SetBinding(TextBox.TextProperty, textBinding);
            textBox.IsEnabled = true;


            textBox.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(textBox, x);
            Grid.SetRow(textBox, y);

            panel.Children.Add(textBox);
        }

        private static void BezeichnungByteZeichnen(int vbyte, int x, int y, Panel panel)
        {
            var parameterNummer = vbyte;

            var bezeichnung = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            bezeichnung.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.BezeichnungDa[" + parameterNummer + "]"));
            bezeichnung.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(bezeichnung, x);
            Grid.SetRow(bezeichnung, y);
            panel.Children.Add(bezeichnung);
        }
        private static void KommentarByteZeichnen(int vbyte, int x, int y, Panel panel)
        {
            var parameterNummer = vbyte;

            var kommentar = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                Padding = new Thickness(10, 5, 5, 5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            kommentar.SetBinding(TextBlock.TextProperty, new Binding("ManVisuAnzeigen.KommentarDa[" + parameterNummer + "]"));
            kommentar.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            panel.Children.Add(kommentar);
        }

        private static void TextZeichnen(string beschriftung, HorizontalAlignment alignment, int x, int y, Panel panel)
        {
            var text = new TextBlock
            {
                Text = beschriftung,
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = alignment
            };

            Grid.SetColumn(text, x);
            Grid.SetRow(text, y);
            panel.Children.Add(text);
        }
    }
}