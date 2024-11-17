using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E621Maui.Lib.SettingsManager
{
    public abstract partial class SettingsManager
    {
        protected string GetString([CallerMemberName] string key = "", string defaultValue = default)
        {
            _logger?.LogDebug("Get config string {key}", key);
            return GetValue(key, defaultValue);
        }

        protected int GetInt([CallerMemberName] string key = "", int defaultValue = default)
        {
            _logger?.LogDebug("Get config i32 {key}", key);
            string valueString = GetValue(key);
            return int.TryParse(valueString, out int ret) ? ret : defaultValue;
        }

        protected bool GetBool([CallerMemberName] string key = "", bool defaultValue = default)
        {
            _logger?.LogDebug("Get config bool {key}", key);
            string valueString = GetValue(key);
            return bool.TryParse(valueString, out bool ret) ? ret : defaultValue;
        }

        protected byte GetByte([CallerMemberName] string key = "", byte defaultValue = default)
        {
            _logger?.LogDebug("Get config byte {key}", key);
            string valueString = GetValue(key);
            return byte.TryParse(valueString, out byte ret) ? ret : defaultValue;
        }

        protected sbyte GetSbyte([CallerMemberName] string key = "", sbyte defaultValue = default)
        {
            _logger?.LogDebug("Get config sbyte {key}", key);
            string valueString = GetValue(key);
            return sbyte.TryParse(valueString, out sbyte ret) ? ret : defaultValue;
        }

        protected char GetChar([CallerMemberName] string key = "", char defaultValue = default)
        {
            _logger?.LogDebug("Get config char {key}", key);
            string valueString = GetValue(key);
            return char.TryParse(valueString, out char ret) ? ret : defaultValue;
        }

        protected decimal GetDecimal([CallerMemberName] string key = "", decimal defaultValue = default)
        {
            _logger?.LogDebug("Get config decimal {key}", key);
            string valueString = GetValue(key);
            return decimal.TryParse(valueString, out decimal ret) ? ret : defaultValue;
        }

        protected double GetDouble([CallerMemberName] string key = "", double defaultValue = default)
        {
            _logger?.LogDebug("Get config double {key}", key);
            string valueString = GetValue(key);
            return double.TryParse(valueString, out double ret) ? ret : defaultValue;
        }

        protected float GetFloat([CallerMemberName] string key = "", float defaultValue = default)
        {
            _logger?.LogDebug("Get config float {key}", key);
            string valueString = GetValue(key);
            return float.TryParse(valueString, out float ret) ? ret : defaultValue;
        }

        protected uint GetUint([CallerMemberName] string key = "", uint defaultValue = default)
        {
            _logger?.LogDebug("Get config uint {key}", key);
            string valueString = GetValue(key);
            return uint.TryParse(valueString, out uint ret) ? ret : defaultValue;
        }

        protected nint GetNint([CallerMemberName] string key = "", nint defaultValue = default)
        {
            _logger?.LogDebug("Get config nint {key}", key);
            string valueString = GetValue(key);
            return nint.TryParse(valueString, out nint ret) ? ret : defaultValue;
        }

        protected nuint GetNuint([CallerMemberName] string key = "", nuint defaultValue = default)
        {
            _logger?.LogDebug("Get config nuint {key}", key);
            string valueString = GetValue(key);
            return nuint.TryParse(valueString, out nuint ret) ? ret : defaultValue;
        }

        protected long GetLong([CallerMemberName] string key = "", long defaultValue = default)
        {
            _logger?.LogDebug("Get config long {key}", key);
            string valueString = GetValue(key);
            return long.TryParse(valueString, out long ret) ? ret : defaultValue;
        }

        protected ulong GetUlong([CallerMemberName] string key = "", ulong defaultValue = default)
        {
            _logger?.LogDebug("Get config ulong {key}", key);
            string valueString = GetValue(key);
            return ulong.TryParse(valueString, out ulong ret) ? ret : defaultValue;
        }

        protected short GetShort([CallerMemberName] string key = "", short defaultValue = default)
        {
            _logger?.LogDebug("Get config short {key}", key);
            string valueString = GetValue(key);
            return short.TryParse(valueString, out short ret) ? ret : defaultValue;
        }

        protected ushort GetUshort([CallerMemberName] string key = "", ushort defaultValue = default)
        {
            _logger?.LogDebug("Get config ushort {key}", key);
            string valueString = GetValue(key);
            return ushort.TryParse(valueString, out ushort ret) ? ret : defaultValue;
        }
        private string GetValue(string key, string defaultValue = "") =>
            _values.GetValueOrDefault(GetJsonPropertyName(key), defaultValue);
    }
}
