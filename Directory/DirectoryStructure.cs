using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTreeView
{
    public class DirectoryStructure
    {
        private static string file;

        public static List<DirectoryItem> GetLogicalDrives()
        {
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        public static List<DirectoryItem> GetDirectoryContents(string FullPath)
        {
            var items = new List<DirectoryItem>();

            #region Get Folders

            try
            {
                var dirs = Directory.GetDirectories(FullPath);
                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(dirs => new DirectoryItem { FullPath = dirs, Type = DirectoryItemType.Folder }));
                }

            }
            catch { }

            #endregion

            #region Get Files
            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(FullPath);
                if (fs.Length > 0)
                {
                    items.AddRange(fs.Select(fs => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
                }

            }
            catch { }

            #endregion

            return items;

        }

    #region Helpers
    public static string GetFileFolderName(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;

        // Make all slashes backslahes
        var normalizedPath = path.Replace('/', '\\');

        // Find the last backslash
        var lastIndex = normalizedPath.LastIndexOf('\\');

        //if we don't find a backslash, return the path itself
        if (lastIndex <= 0)
        { return path; }

        //Return name after last backslash
        return path.Substring(lastIndex + 1);
    }

    #endregion
    
    }
}
