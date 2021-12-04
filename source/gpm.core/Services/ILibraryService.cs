using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gpm.core.Models;
using Octokit;

namespace gpm.core.Services
{
    public interface ILibraryService : IDictionary<string, PackageModel>
    {
        void Load();
        void Save();

        PackageModel GetOrAdd(Package package);

        bool IsInstalled(string key);
        bool IsInstalled(Package package);

        bool IsInstalledInSlot(Package package, int slot);

        /// <summary>
        /// Uninstalls a package from the system by slot
        /// </summary>
        /// <param name="package"></param>
        /// <param name="slotIdx"></param>
        bool UninstallPackage(Package package, int slotIdx = 0);

        /// <summary>
        /// Uninstalls a package from the system by slot
        /// </summary>
        /// <param name="key"></param>
        /// <param name="slotIdx"></param>
        /// <returns></returns>
        bool UninstallPackage(string key, int slotIdx = 0);

        /// <summary>
        /// Uninstalls a package from the system by slot
        /// </summary>
        /// <param name="model"></param>
        /// <param name="slotIdx"></param>
        bool UninstallPackage(PackageModel model, int slotIdx = 0);

        bool IsInstalledInSlot(string key, int slot);
    }
}
