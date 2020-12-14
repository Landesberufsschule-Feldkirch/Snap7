using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace ManualMode
{
    public partial class DaFenster
    {
        private void DaCreateGridBit(int anzahlZeilenConfig, DaConfig config, ManualViewModel manualViewModel)
        {
            var daGrid = new Grid { Name = "DaGrid" };
            Content = daGrid;

            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenWert) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenBezeichnung) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenAbstand) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(SpaltenKommentar) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenHoehe) });
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ZeilenAbstand) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, daGrid);

            _fensterFunktionen.TextZeichnen("Tasten", HorizontalAlignment.Center, 0, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Toggeln", HorizontalAlignment.Center, 2, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Center, 4, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 7, Brushes.YellowGreen, daGrid);

                DaButtonTastenZeichnen(0, 2 + 2 * i, i, config, daGrid, manualViewModel);
                DaButtonToggelnZeichnen(2, 2 + 2 * i, i, config, daGrid, manualViewModel);
                _fensterFunktionen.BezeichnungZeichnen(4, 2 + 2 * i, i, "Da", HorizontalAlignment.Center, VisibilityProperty, daGrid);
                _fensterFunktionen.KommentarZeichnen(6, 2 + 2 * i, i, "Da", VisibilityProperty, daGrid);

            }
        }
        private void DaCreateGridByte(int anzahlZeilenConfig)
        {
            var daGrid = new Grid { Name = "DaGrid" };
            Content = daGrid;

            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            daGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (var i = 0; i <= anzahlZeilenConfig; i++)
            {
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(45) });
                daGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
            }

            _fensterFunktionen.HintergrundRechteckZeichnen(0, 0, 7, Brushes.Yellow, daGrid);

            _fensterFunktionen.TextZeichnen("Eingabe", HorizontalAlignment.Center, 0, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Aktuell", HorizontalAlignment.Center, 2, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Bezeichnung", HorizontalAlignment.Left, 4, 0, daGrid);
            _fensterFunktionen.TextZeichnen("Kommentar", HorizontalAlignment.Left, 6, 0, daGrid);

            for (var i = 0; i < anzahlZeilenConfig; i++)
            {
                _fensterFunktionen.HintergrundRechteckZeichnen(0, 2 + 2 * i, 7, Brushes.YellowGreen, daGrid);
                DaTextboxByteZeichnen(i, 0, 2 + 2 * i, daGrid);
                _fensterFunktionen.BezeichnungZeichnen(2, 2 + 2 * i, i, "Da",HorizontalAlignment.Center, VisibilityProperty, daGrid);
                _fensterFunktionen.KommentarZeichnen(6, 2 + 2 * i, i, "Da", VisibilityProperty, daGrid);
            }
        }
        private static void DaCreateGridWord() => throw new NotImplementedException();
        private static void DaCreateGridLong() => throw new NotImplementedException();
        private static void DaButtonTastenZeichnen(int x, int y, int par, DaConfig config, Panel panel, ManualViewModel manualViewModel)
        {
            var buttonTasten = new Button
            {
                Content = config.DigitaleAusgaenge[par].StartByte + "." + config.DigitaleAusgaenge[par].StartBit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnTasten,
                CommandParameter = par.ToString(),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonTasten.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeTastenToggelnDa[" + par + "]"));
            buttonTasten.SetBinding(ButtonBase.ClickModeProperty, new Binding("ManVisuAnzeigen.ClickModeTasten[" + par + "]"));
            buttonTasten.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + par + "]"));

            Grid.SetColumn(buttonTasten, x);
            Grid.SetRow(buttonTasten, y);

            panel.Children.Add(buttonTasten);
        }
        private static void DaButtonToggelnZeichnen(int x, int y, int par, DaConfig config, Panel panel, ManualViewModel manualViewModel)
        {

            var buttonToggeln = new Button
            {
                Content = config.DigitaleAusgaenge[par].StartByte + "." + config.DigitaleAusgaenge[par].StartBit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnToggeln,
                CommandParameter = par.ToString(),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonToggeln.SetBinding(BackgroundProperty, new Binding("ManVisuAnzeigen.FarbeTastenToggelnDa[" + par + "]"));
            buttonToggeln.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + par + "]"));

            Grid.SetColumn(buttonToggeln, x);
            Grid.SetRow(buttonToggeln, y);

            panel.Children.Add(buttonToggeln);
        }
        private static void DaTextboxByteZeichnen(int vbyte, int x, int y, Panel panel)
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
                UpdateSourceTrigger = UpdateSourceTrigger.LostFocus
            };

            textBox.SetBinding(TextBox.TextProperty, textBinding);
            textBox.IsEnabled = true;


            textBox.SetBinding(VisibilityProperty, new Binding("ManVisuAnzeigen.VisibilityDa[" + parameterNummer + "]"));

            Grid.SetColumn(textBox, x);
            Grid.SetRow(textBox, y);

            panel.Children.Add(textBox);
        }
    }
}