using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestAutomat
{
    public partial class TestAutomat
    {
        public void TestProjekteEinfuellen(Button btnStart, StackPanel stackPanel, WebBrowser webBrowser)
        {
            foreach (var projekt in ConfigOrdner.AlleTestOrdner)
            {
                var rdo = new RadioButton
                {
                    GroupName = "TestProjekte",
                    Name = projekt.Name,
                    FontSize = 14,
                    Content = projekt.Name,
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = projekt
                };

                rdo.Checked += (sender, _) =>
                {
                    if (!(sender is RadioButton { Tag: DirectoryInfo } rb)) return;

                    btnStart.IsEnabled = true;
                    btnStart.Background = new SolidColorBrush(Colors.LawnGreen);

                    AktuellesProjekt = rb.Tag as DirectoryInfo;

                    if (AktuellesProjekt == null) return;

                    BeschriftungenPlc.UpdateBeschriftungen(AktuellesProjekt.FullName);
                    
                    UpdateBelegung();

                    _datenstruktur.TestProjektOrdner = AktuellesProjekt.FullName;
                    _callbackPlcWindow?.Invoke(_datenstruktur);

                    var dateiName = $@"{AktuellesProjekt.FullName}\index.html";

                    var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

                    var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
                    var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

                    webBrowser.NavigateToStream(stmHtmlSeite);
                };

                stackPanel.Children.Add(rdo);
            }
        }
    }
}