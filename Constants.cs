using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui
{
    public static class Constants
    {
        public const string Person = "Contacts";
        public const string ClickFreeFolderName = "ClickFree";
        public const string WindowsBackupFolderName = "Photos And Videos";
        public const string FacebookFolderName = "Photos from Facebook";
        public const string InstagramFolderName = "Photos from Instagram";
        public static string[] AppleFileExtensions = new[] { ".heic" };
        public static string[] IgnoreFileExtensions = new string[0];
        public static List<string> DefaultBackUpFolders = new List<string>(new[]
        {
            Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
        });
    }
}
