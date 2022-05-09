using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PeopleEditor.Tools.Managers
{
    internal static class FileFolderManager
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath =
            Path.Combine(AppDataPath, "PeopleEditor");

        internal static readonly string StorageFilePath =
            Path.Combine(AppFolderPath, "People.txt");

        internal static bool CreateFolderAndCheckFileExistence(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistence();
        }

        internal static bool CreateFolderAndCheckFileExistence(this FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            return file.Exists;
        }
    }
}
