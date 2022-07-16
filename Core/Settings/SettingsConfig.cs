using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Settings
{
    public class SettingsConfig
    {
        public string? MONGO_URI { get; set; }
        public string? DATABASE_NAME { get; set; }

        public SettingsConfig Properties;

        public SettingsConfig() => Properties = this;
    }
}
