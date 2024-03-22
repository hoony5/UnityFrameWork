using System.Collections.Generic;

namespace Share
{
    public static class IOEx
    {
        public static List<string> GetChildrenAllFiles(string path)
        {
            List<string> children = new List<string>();
            string[] innerFiles = System.IO.Directory.GetFiles(path);
            if (innerFiles.Length == 0) return default;
            foreach (string file in innerFiles)
            {
                children.Add(file);
            }
            string[] innerFolders = System.IO.Directory.GetDirectories(path);
            if (innerFolders.Length == 0) return children;
            foreach (string folder in innerFolders)
            {
                children.AddRange(GetChildrenAllFiles(folder));       
            }
            return children;
        }
    }   
}
