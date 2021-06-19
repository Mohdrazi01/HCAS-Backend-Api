using System;

namespace APSystem.Configuration.Settings
{
    /// <summary>
    /// This Class to read the Application settings from AppSettings.json file
    /// </summary>
    public class AppSettings
    {
       /// <summary>
       /// This property is used for specifying the time out for DbTimeOut
       /// </summary>
       /// <value></value>
        public int DbTimeOut { get; set; }
    }
}