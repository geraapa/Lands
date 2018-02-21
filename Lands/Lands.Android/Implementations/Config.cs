using Xamarin.Forms;

[assembly: Dependency(typeof(Lands.Droid.Implementations.Config))]

namespace Lands.Droid.Implementations
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
                    directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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
                    platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return platform;
            }
        }
        #endregion
    }
}