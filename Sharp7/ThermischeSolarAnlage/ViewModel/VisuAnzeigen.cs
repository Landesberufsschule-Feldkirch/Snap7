using System.ComponentModel;
using System.Threading;

namespace ThermischeSolarAnlage.ViewModel;

public class VisuAnzeigen : INotifyPropertyChanged
{
    public VisuAnzeigen()
    {
        System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
    }

    private static void VisuAnzeigenTask()
    {
        while (true)
        {


            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }


    private string _textFeld;

    public string TextFeld
    {
        get => _textFeld;
        set
        {
            _textFeld = value;
            OnPropertyChanged(nameof(TextFeld));
        }
    }

    #region iNotifyPeropertyChanged Members
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion iNotifyPeropertyChanged Members
}