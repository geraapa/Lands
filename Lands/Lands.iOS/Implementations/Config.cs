using Xamarin.Forms;

[assembly: Dependency(typeof(Lands.iOS.Implementations.Config))]

namespace Lands.iOS.Implementations
{
    using Lands.Interfaces;
    using SQLite.Net.Interop;
    using System;

    public class Config : IConfig
    {
        #region Attributes
        private string directoryDB;
        private ISQLitePlatform platform;
        #endregion

        #region Propierties
        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(directory, "..", "Library");
                }

                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }

                return platform;
            }
        }
        #endregion
    }
}