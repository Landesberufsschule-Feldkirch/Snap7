using ManualMode.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using static System.Windows.Controls.Grid;

namespace ManualMode
{
    public static partial class FensterFunktionen
    {
        internal static void ButtonZeichnen(int x, int y, int bezByte, int bezBit, int par, string bez, DependencyProperty backgroundProperty, DependencyProperty visibilityProperty, Grid grid)
        {
            var buttonTasten = new Button
            {
                Content = bezByte + "." + bezBit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonTasten.SetBinding(backgroundProperty, new Binding($"ManVisuAnzeigen.Farbe{bez} [{ par }]"));
            buttonTasten.SetBinding(visibilityProperty, new Binding($"ManVisuAnzeigen.Visibility{bez} [{ par }]"));

            SetColumn(buttonTasten, x);
            SetRow(buttonTasten, y);

            grid.Children.Add(buttonTasten);
        }
        internal static void ButtonTastenZeichnen(int x, int y, int bezByte, int bezBit, int par, string bez, DependencyProperty backgroundProperty, DependencyProperty visibilityProperty, Grid grid, ManualViewModel manualViewModel)
        {
            var buttonTasten = new Button
            {
                Content = bezByte + "." + bezBit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnTasten,
                CommandParameter = par.ToString(),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonTasten.SetBinding(backgroundProperty, new Binding($"ManVisuAnzeigen.FarbeTastenToggeln{bez} [{ par }]"));
            buttonTasten.SetBinding(ButtonBase.ClickModeProperty, new Binding($"ManVisuAnzeigen.ClickModeTasten [{ par }]"));
            buttonTasten.SetBinding(visibilityProperty, new Binding($"ManVisuAnzeigen.Visibility{bez} [{ par }]"));

            SetColumn(buttonTasten, x);
            SetRow(buttonTasten, y);

            grid.Children.Add(buttonTasten);
        }
        internal static void ButtonToggelnZeichnen(int x, int y, int bezByte, int bezBit, int par, string bez, DependencyProperty backgroundProperty, DependencyProperty visibilityProperty, Grid grid, ManualViewModel manualViewModel)
        {

            var buttonToggeln = new Button
            {
                Content = bezByte + "." + bezBit,
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Command = manualViewModel.BtnToggeln,
                CommandParameter = par.ToString(),
                Margin = new Thickness(3, 3, 3, 3)
            };

            buttonToggeln.SetBinding(backgroundProperty, new Binding($"ManVisuAnzeigen.FarbeTastenToggeln{bez} [{ par }]"));
            buttonToggeln.SetBinding(visibilityProperty, new Binding($"ManVisuAnzeigen.Visibility{bez} [{ par }]"));

            SetColumn(buttonToggeln, x);
            SetRow(buttonToggeln, y);

            grid.Children.Add(buttonToggeln);
        }
    }
}