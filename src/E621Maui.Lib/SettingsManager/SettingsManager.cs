using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E621Maui.Lib.SettingsManager
{
    /// <summary>
    /// <para>
    ///     Abstract class that can be inherited to create a settings manager,
    ///     a safe and fluent way of storing configuration values in JSON format on disk. 
    /// </para>
    /// <para>
    ///     The base class contains type-specific methods such as <see cref="GetInt"/> to
    ///     retrieve data as a specific type so that implementations need not concern themselves
    ///     with casting. These methods are non-exhaustive, but their pattern may easily be followed
    ///     in implementation to create getters for any desired type. 
    /// </para>
    /// <para>
    ///     The recommended way of creating a settings property is rather similar to implementing the
    ///     <see cref="INotifyPropertyChanged"/> pattern. The use of the <see cref="CallerMemberNameAttribute"/>
    ///     ensures that implementations can remain clean and concise, while also allowing the default behaviour
    ///     to be overwritten if necessary. 
    ///     <code>
    ///         internal class MySettingsManager : SettingsManager
    ///         {
    ///             public int MyValue
    ///             {
    ///                 get => GetInt();
    ///                 set => SetValue(value);
    ///             }
    ///
    ///             public bool IsTapOn
    ///             {
    ///                 get => GetBool("IsFaucetOn");
    ///                 set => SetValue(value, "IsFaucetOn");
    ///             }
    ///         }
    ///     </code>
    /// </para>
    /// </summary>
    /// <remarks>
    ///     Requires Microsoft.Extensions.Logging.Abstractions and Newtonsoft.Json NuGet package.
    /// </remarks>
    public abstract partial class SettingsManager : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private string _configPath;
        private Dictionary<string, string> _values = new();

        /// <summary>
        ///     The path to the configuration file to read from and write to.
        ///     Can be set via <see cref="SetConfigPath"/>.
        /// </summary>
        public string ConfigPath
        {
            get => _configPath;
            private set
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(ConfigPath)));
                _configPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConfigPath)));
            }
        }

        /// <summary>
        ///     Whether to "pretty-print" the configuration on save.
        ///     If set to true, each property and bracket/brace will be given its own line.
        ///     If false, the content written to the file will be minified. 
        /// </summary>
        public bool PrettyPrint { get; set; }

        protected SettingsManager()
        {
            _configPath ??= Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "e621maui.json");
            InvalidateCache();
        }

        /// <summary>
        ///     Change the path of the settings file.
        ///     This will invalidate the cached values and cause all properties to notify changing and changed. 
        /// </summary>
        public void SetConfigPath(string newPath)
        {
            ConfigPath = newPath;
            InvalidateCache();
        }

        /// <summary>
        ///     Sets a value and saves to the configuration file.
        /// </summary>
        /// <param name="value">
        ///     The new value. Values are serialised as strings, even if their type is supported in JSON.
        ///     This includes Enums. Calling this function will trigger a PropertyChanging and
        ///     PropertyChanged event on the given key. If the value's <see cref="object.ToString"/> returns
        ///     null, events will not be triggered and the property will not be set. 
        /// </param>
        /// <param name="key">
        ///     The key to set. Usually, this should be the <see langword="nameof"/>()
        ///     a property on an implementation of this class. If left blank, the caller
        ///     member name will be used, allowing for such constructions as
        ///     <code>
        ///         public string MyValue
        ///         {
        ///             get => GetString();
        ///             set => SetValue(value);
        ///         }
        ///     </code>
        /// </param>
        protected void SetValue(object value, [CallerMemberName] string key = "")
        {
            key = GetJsonPropertyName(key);

            string? stringValue = value.ToString();
            if (stringValue is null)
            {
                return;
            }

            _logger?.LogDebug("Changing config value {key} to {value}", key, value);
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(key));

            _values[key] = stringValue;
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(_values, PrettyPrint ? Formatting.Indented : Formatting.None));

            _logger?.LogDebug("Changed config value {key} to {value}", key, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
        }

        private void InvalidateCache()
        {
            _logger?.LogDebug("Refresh config cache from {path}", ConfigPath);
            var newValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(ConfigPath));
            if (newValues is null) throw new Exception($"The contents of {ConfigPath} is corrupted and unreadable.");

            foreach (string prop in GetType().GetProperties().Select(x => x.Name))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(prop));
            }

            _values = newValues;

            foreach (string prop in GetType().GetProperties().Select(x => x.Name))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
