using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using gpm.Core.Installer;
using gpm.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace WpfAppTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Register services
            Ioc.Default.ConfigureServices(
                new ServiceCollection()

                .AddSingleton<MySink>()
                .AddSingleton<AutoInstallerService>()

                .AddSingleton<IGitHubService, GitHubService>()
                .AddSingleton<IDeploymentService, DeploymentService>()

                .AddSingleton<ILibraryService, LibraryService>()
                .AddSingleton<IDataBaseService, DataBaseService>()

                .BuildServiceProvider());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.MySink()
               .CreateLogger();
            Log.Information("Started");
        }



    }
}