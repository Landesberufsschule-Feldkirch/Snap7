using System.Collections.ObjectModel;
using System.IO;

namespace TestAutomat.Model;

public class OrdnerLesen
{
    public ObservableCollection<DirectoryInfo> AlleTestOrdner { get; set; } = new();
    public OrdnerLesen(string configTests) => OrdnerEinlesen(configTests);
    private void OrdnerEinlesen(string configTests)
    {
        var parentDirectory = new DirectoryInfo(configTests);

        foreach (var ordnerInfo in parentDirectory.GetDirectories())
        {
            if ((ordnerInfo.Attributes & FileAttributes.Directory) == 0 || ordnerInfo.Name == ".git") continue;
            AlleTestOrdner.Add(ordnerInfo);
        }
    }
}