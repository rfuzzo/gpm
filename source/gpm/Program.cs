using System;
using Serilog;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using System.IO;
using gpm;
using gpm.Commands;
using gpm.core.Services;

var rootCommand = new RootCommand
{
    new ListCommand(),
    new InstallCommand(),
    new UpdateCommand(),
    new RemoveCommand(),
    new InstalledCommand(),
    new UpgradeCommand()
};

Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#else
    .MinimumLevel.Information()
#endif
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(IAppSettings.GetLogsFolder(), "gpm-log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

var parser = new CommandLineBuilder(rootCommand)
    .UseDefaults()
    .UseHost(GenericHost.CreateHostBuilder)
    .Build();

#if DEBUG
Environment.CurrentDirectory = Path.GetTempPath();
#endif

// hack to get DI in system.commandline
parser.Invoke(new UpgradeCommand().Name);

parser.Invoke(args);