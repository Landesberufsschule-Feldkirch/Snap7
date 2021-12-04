using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WpfApp1;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost _host;
    public App()
    {
        _host = new HostBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<Dependency1>();
                services.AddSingleton<Foo>();
                services.AddLogging();
            })
            .Build();
    }

    private async void Application_Startup(object sender, StartupEventArgs e)
    {
        await _host.StartAsync();

        var mainWindow = _host.Services.GetService<MainWindow>();
        mainWindow.Show();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
    }
}

public class Foo
{

}

public class Dependency1
{
    public Dependency1(Foo foo, ILogger<Dependency1> logger)
    {
        logger.LogDebug("Startup");
    }
}