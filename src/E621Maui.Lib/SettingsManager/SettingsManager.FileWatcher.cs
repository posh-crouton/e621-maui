using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E621Maui.Lib.SettingsManager
{
    public abstract partial class SettingsManager
    {
        /// <summary>
        /// <para>
        ///     Registers a file watcher for the given configuration path.
        ///     If the contents of the file changes or the file is renamed,
        ///     the watcher will notice and invalidate the app's cache, triggering
        ///     PropertyChanging and PropertyChanged for all properties. 
        /// </para>
        /// <para>
        ///     If the config path changes while a watcher is registered, the existing
        ///     watcher will be discarded, and a new watcher will take its place watching
        ///     the new file. 
        /// </para>
        /// </summary>
        public void RegisterFileWatcher()
        {
            FileSystemWatcher watcher = new()
            {
                Path = Path.GetDirectoryName(ConfigPath)!,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = Path.GetFileName(ConfigPath)
            };

            bool watching = true;
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName != nameof(ConfigPath)) return;
                watching = false;
                RegisterFileWatcher();
            };

            watcher.Changed += (_, _) =>
            {
                InvalidateCache();
            };
            watcher.EnableRaisingEvents = true;

            Task.Run(() =>
            {
                while (watching)
                {
                    watcher.WaitForChanged(WatcherChangeTypes.Changed);
                }
            });
        }
    }
}
