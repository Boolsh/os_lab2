using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//18.Заданы два каталога. Для каждого из них подсчитать количество 
//каталогов, содержащих более чем m файлов. Сравнить их количество. 

namespace os_lab2
{
    public class CatalogProcessor
    {
        private readonly string rootPath;
        private readonly int m;
        private int count;

        public int ResultCount => count;

        public CatalogProcessor(string path, int minFiles)
        {
            rootPath = path ?? throw new ArgumentNullException(nameof(path));
            m = minFiles;
        }

        public void Process()
        {
            if (!Directory.Exists(rootPath)) return;
            SearchDirectory(rootPath);
        }

        private void SearchDirectory(string path)
        {
            WIN32_FIND_DATA data;


            IntPtr handle = FindFirstFile(Path.Combine(path, "*"), out data);

            if (handle == INVALID_HANDLE_VALUE)
                return;


            int fileCount = 0;

            do
            {
                string current = data.cFileName;
                if (current == "." || current == "..")
                    continue;

                bool isDir = (data.dwFileAttributes & FileAttributes.Directory) != 0;
                string fullPath = Path.Combine(path, current);

                if (isDir)
                {
                    SearchDirectory(fullPath);
                }
                else
                {
                    fileCount++;
                }

            } while (FindNextFile(handle, out data));

            if (fileCount > m)
                Interlocked.Increment(ref count);
            FindClose(handle);
        }



        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct WIN32_FIND_DATA
        {
            public FileAttributes dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FindClose(IntPtr hFindFile);
    }
}










//    try
//    {
//        int fileCount = 0;

//        do
//        {
//            string current = data.cFileName;
//            if (current == "." || current == "..")
//                continue;

//            bool isDir = (data.dwFileAttributes & FileAttributes.Directory) != 0;
//            string fullPath = Path.Combine(path, current);

//            if (isDir)
//            {
//                SearchDirectory(fullPath);
//            }
//            else
//            {
//                fileCount++;
//            }

//        } while (FindNextFile(handle, out data));

//        if (fileCount > m)
//            Interlocked.Increment(ref count);
//    }
//    finally
//    {   
//        FindClose(handle);
//    }
//}