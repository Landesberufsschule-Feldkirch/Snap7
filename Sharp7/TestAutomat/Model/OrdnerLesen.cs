using System.Collections.ObjectModel;
using System.IO;

namespace TestAutomat.Model
{
    public class OrdnerLesen
    {
        public ObservableCollection<DirectoryInfo> AlleTestOrdner { get; set; } = new();

        public OrdnerLesen(string autoTestConfig) => OrdnerEinlesen(autoTestConfig);


        private void OrdnerEinlesen(string autoTestConfig)
        {
            var parentDirectory = new DirectoryInfo(autoTestConfig);

            foreach (var ordnerInfo in parentDirectory.GetDirectories())
            {
                if ((ordnerInfo.Attributes & FileAttributes.Directory) == 0 || ordnerInfo.Name == ".git") continue;
                AlleTestOrdner.Add(ordnerInfo);
            }
        }
    }
}