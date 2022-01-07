using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using gpm.Core.Models;
using gpm.Core.Services;

namespace gpm.WinUI.ViewModels;

/// <summary>
/// A ViewModel for an installed package
/// </summary>
public class PackageModelViewModel : ObservableObject
{
    private PackageModel _model;

    private readonly ILibraryService _libraryService = Ioc.Default.GetRequiredService<ILibraryService>();
    private readonly ITaskService _taskService = Ioc.Default.GetRequiredService<ITaskService>();

    //public PackageViewModel(Package model)
    //{
    //    _model = model;

    //    InstallCommand = new AsyncRelayCommand(InstallAsync);
    //    CheckCommand = new AsyncRelayCommand(CheckForUpdatesAsync);
    //    LaunchCommand = new AsyncRelayCommand(LaunchAsync);
    //}

    public PackageModelViewModel(PackageModel model)
    {
        _model = model;

        InstallCommand = new AsyncRelayCommand(InstallAsync);
        CheckCommand = new AsyncRelayCommand(CheckForUpdatesAsync);
        LaunchCommand = new AsyncRelayCommand(LaunchAsync);
        RemoveCommand = new AsyncRelayCommand(RemoveAsync);
    }
    public string? Version
    {
        get
        {
            // get default version unless a slot is selected
            if (IsInstalled && _libraryService.TryGetDefaultSlot(Id, out var slot))
            {
                return slot.Version;
            }

            // TODO slot selection in UI

            return "-";
        }
    }

    public string Id => _model.Key;

    //public string? Url => _model.;

    public bool IsInstalled => _libraryService.IsInstalled(_model.Key);

    public bool IsNotInstalled => !IsInstalled;

    public IAsyncRelayCommand InstallCommand { get; }

    private async Task InstallAsync()
    {
        await _taskService.UpdateAndInstall(Id, "", "", true);
    }

    public IAsyncRelayCommand CheckCommand { get; }
    private async Task CheckForUpdatesAsync()
    {
        await Task.Delay(1);
    }

    public IAsyncRelayCommand LaunchCommand { get; }
    private async Task LaunchAsync()
    {
        await Task.Delay(1);
    }

    public IAsyncRelayCommand RemoveCommand { get; }
    private async Task RemoveAsync()
    {
        await _taskService.Remove(Id, true, "", null);
    }
}

