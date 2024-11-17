using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E621Maui.Lib.SettingsManager
{
    public class AppSettingsManager : SettingsManager
    {
        public string E621UserName
        {
            get => GetString();
            set => SetValue(value);
        }

        public string E621ApiKey
        {
            get => GetString();
            set => SetValue(value);
        }
    }
}
