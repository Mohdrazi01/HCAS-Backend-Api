using System;

namespace APSystem.Configuration.Constants
{
    public static class DefaultConstants
    {
        /// <summary>
        /// Name of the Connection String which you want to connect
        /// </summary>
        /// <returns></returns>
         public const string DefaultConnection = nameof(DefaultConstants.DefaultConnection);
         public const string ConnectionStrings = nameof(DefaultConstants.ConnectionStrings);
         public const string AppSettings = nameof(DefaultConstants.AppSettings);
         public const string EmailSettings = nameof(DefaultConstants.EmailSettings);
         public const string JwtSettings = nameof(DefaultConstants.JwtSettings);
         
    }
}