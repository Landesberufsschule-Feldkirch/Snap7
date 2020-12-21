using System.Collections.ObjectModel;
using System.IO;

namespace TestAutomat.Model
{
    public class OrdnerLesen
    {
        public ObservableCollection<DirectoryInfo> AlleTestOrdner { get; set; } = new();

        public OrdnerLesen() => OrdnerEinlesen();


        private void OrdnerEinlesen()
        {
            var parentDirectory = new DirectoryInfo(".");

            foreach (var ordnerInfo in parentDirectory.GetDirectories())
            {
                if ((ordnerInfo.Attributes & FileAttributes.Directory) == 0 || ordnerInfo.Name == ".git") continue;
                if (ordnerInfo.Name.Contains("Tst_"))
                {
                    AlleTestOrdner.Add(ordnerInfo);
                }
            }
        }
    }
}